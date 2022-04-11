namespace Dashboard.Controllers
{
	using Dashboard.Models.Source;

	using Microsoft.AspNetCore.Mvc;

	using System;
	using System.Linq;

	/// <summary>
	/// Контроллер управления недельным отчетом.
	/// </summary>
	public class WeeklyReportController : Controller, IDateController
	{
		/// <inheritdoc/>
		public IActionResult Index()
		{
			try
			{
				SetSourceContext();
				WeeklySource.Initialize();
				ViewBag.Error = "none";
			}
			catch
			{
				ViewBag.Error = "error";
			}
			ViewBag.Labels_en = new string[] { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
			ViewBag.Labels_ru = new string[] { "ПН", "ВТ", "СР", "ЧТ", "ПТ", "СБ", "ВС" };
			return View();
		}

		[HttpGet]
		/// <inheritdoc/>
		public dynamic ChangeLang()
		{
			var now = DateTime.Now.Second;

			try
			{
				if (now % 15 >= 6 && now % 15 < 9)
				{
					SetSourceContext();
					WeeklySource.Update();
				}
			}
			catch { }

			var en = now >= 30;
			return new
			{
				wmWeekQtyName = en ? "This week line WM" : "Произведено стиральных машин за неделю",
				rfWeekQtyName = en ? "This week line RF" : "Произведено холодильников за неделю",
				chartName = en ? "Daily production" : "Производство по дням",
				productionName = en ? "Average production" : "Средние показатели",
				shift1Name = en ? "1st shift" : "За 1 смену",
				shift2Name = en ? "2nd shift" : "За 2 смену",
				shift3Name = en ? "3rd shift" : "За 3 смену",
				dayName = en ? "Day" : "За день",
				en
			};
		}

		[HttpGet]
		/// <inheritdoc/>
		public dynamic Update()
		{
			return new
			{
				rfproduction = WeeklySource.RF?.Production.Select(x => x.TotalSum == 0 ? null : x.TotalSum.ToString()).ToArray(),
				rf1shift = WeeklySource.RF?.AverageOn1Shift,
				rf2shift = WeeklySource.RF?.AverageOn2Shift,
				rf3shift = WeeklySource.RF?.AverageOn3Shift,
				rfday = WeeklySource.RF?.AverageOnDay,

				wmproduction = WeeklySource.WM?.Production.Select(x => x.TotalSum == 0 ? null : x.TotalSum.ToString()).ToArray(),
				wm1shift = WeeklySource.WM?.AverageOn1Shift,
				wm2shift = WeeklySource.WM?.AverageOn2Shift,
				wm3shift = WeeklySource.WM?.AverageOn3Shift,
				wmday = WeeklySource.WM?.AverageOnDay,
			};
		}

		[HttpGet]
		/// <inheritdoc/>
		public bool Refresh() => DateTime.Now.Hour == 7 && DateTime.Now.Minute == 0 && DateTime.Now.Second > 0 && DateTime.Now.Second < 3 && DateTime.Now.DayOfWeek == DayOfWeek.Monday;

		[HttpGet]
		public bool WaitCorrectData()
		{
			try
			{
				WeeklySource.Initialize();
				return true;
			}
			catch
			{
				return false;
			}
		}

		/// <inheritdoc/>
		public void SetSourceContext() => WeeklySource.SetContext(new());
	}
}