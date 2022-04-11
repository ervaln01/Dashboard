namespace Dashboard.Controllers
{
	using Dashboard.Models.Source;
	using Dashboard.Models.TimePeriod;
	using Microsoft.AspNetCore.Mvc;

	using System;
	using System.Linq;

	/// <summary>
	/// Контроллер управления дневным отчетом холодильников.
	/// </summary>
	public class DailyReportRFController : Controller, IDateController, IDailyController
	{
		/// <inheritdoc/>
		public IActionResult Index()
		{
			try
			{
				SetSourceContext();
				DailyRFSource.Initialize();
			}
			catch (Exception e)
			{
				if (e.Message.Equals(nameof(Shift)))
				{
					ViewBag.Error = "line";
					return View();
				}

				ViewBag.Error = "plan";
				return View();
			}

			var rf = DailyRFSource.RF;
			if (rf != null)
			{
				ViewBag.ShiftCount = rf.Shifts.Count;
				ViewBag.Plan = rf.Shifts.Select(x => x.Plan).ToArray();
				ViewBag.ShiftProduction = rf.Shifts.Select(x => x.Actual).ToArray();
				ViewBag.Labels = IDailyController.GetLabels(DailyRFSource.RF.Shifts).Distinct().ToArray();
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
				if (now % 15 >= 12 && now % 15 <= 14)
				{
					SetSourceContext();
					DailyRFSource.Update();
				}
			}
			catch { }

			var en = now >= 30;
			return new
			{
				lineName = en ? "Today line RF" : "Произведено холодильников за сегодня",
				shift = en ? "Shift" : "Смена",
				hName = en ? "Hourly production" : "Производство по часам",
			};
		}

		[HttpGet]
		/// <inheritdoc/>
		public dynamic Update() => new { production = DailyRFSource.RF?.Actual.Select(x => x == 0 ? null : x.ToString()).ToArray() };

		[HttpPost]
		/// <inheritdoc/>
		public dynamic UpdateShift(int index) => new
		{
			shift = DailyRFSource.RF?.Shifts[index].Actual,
			plan = DailyRFSource.RF?.Shifts[index].Plan
		};

		[HttpGet]
		/// <inheritdoc/>
		public bool Refresh() => DateTime.Now.Hour == 7 && DateTime.Now.Minute == 0 && DateTime.Now.Second > 0 && DateTime.Now.Second < 3;

		[HttpGet]
		public bool WaitCorrectData()
		{
			try
			{
				DailyRFSource.Initialize();
				return true;
			}
			catch
			{
				return false;
			}
		}

		/// <inheritdoc/>
		public void SetSourceContext() => DailyRFSource.SetContext(new());
	}
}