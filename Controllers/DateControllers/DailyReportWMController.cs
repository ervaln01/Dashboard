namespace Dashboard.Controllers
{
	using Dashboard.Models.Source;
	using Dashboard.Models.TimePeriod;
	using Microsoft.AspNetCore.Mvc;

	using System;
	using System.Linq;

	/// <summary>
	/// Контроллер управления дневным отчетом стиральных машин.
	/// </summary>
	public class DailyReportWMController : Controller, IDateController, IDailyController
	{
		/// <inheritdoc/>
		public IActionResult Index()
		{
			try
			{
				SetSourceContext();
				DailyWMSource.Initialize();
			}
			catch(Exception e)
			{
				if (e.Message.Equals(nameof(Shift)))
				{
					ViewBag.Error = "line";
					return View();
				}

				ViewBag.Error = "plan";
				return View();
			}

			var wm = DailyWMSource.WM;
			if (wm != null)
			{
				ViewBag.ShiftCount = wm.Shifts.Count;
				ViewBag.Plan = wm.Shifts.Select(x => x.Plan).ToArray();
				ViewBag.ShiftProduction = wm.Shifts.Select(x => x.Actual).ToArray();
				ViewBag.Labels = IDailyController.GetLabels(DailyWMSource.WM.Shifts).Distinct().ToArray();
				ViewBag.Error = "none";
				return View();
			}

			ViewBag.Error = "line";
			return View();
		}

		[HttpGet]
		/// <inheritdoc/>
		public dynamic ChangeLang()
		{
			var now = DateTime.Now.Second;

			try
			{
				if (now % 15 >= 9 && now % 15 < 12)
				{
					SetSourceContext();
					DailyWMSource.Update();
				}
			}
			catch { }

			var en = now >= 30;
			return new
			{
				lineName = en ? "Today line WM" : "Произведено стиральных машин за сегодня",
				shift = en ? "Shift" : "Смена",
				hName = en ? "Hourly production" : "Производство по часам",
			};
		}

		[HttpGet]
		/// <inheritdoc/>
		public dynamic Update() => new { production = DailyWMSource.WM?.Actual.Select(x => x == 0 ? null : x.ToString()).ToArray() };

		[HttpPost]
		/// <inheritdoc/>
		public dynamic UpdateShift(int index) => new
		{
			shift = DailyWMSource.WM?.Shifts[index].Actual,
			plan = DailyWMSource.WM?.Shifts[index].Plan
		};

		[HttpGet]
		/// <inheritdoc/>
		public bool Refresh() => DateTime.Now.Hour == 7 && DateTime.Now.Minute == 0 && DateTime.Now.Second > 0 && DateTime.Now.Second < 3;

		[HttpGet]
		public bool WaitCorrectData()
		{
			try
			{
				DailyWMSource.Initialize();
				return true;
			}
			catch
			{
				return false;
			}
		}

		/// <inheritdoc/>
		public void SetSourceContext() => DailyWMSource.SetContext(new());
	}
}