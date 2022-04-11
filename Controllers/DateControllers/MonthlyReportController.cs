namespace Dashboard.Controllers
{
	using Dashboard.Models.Source;

	using Microsoft.AspNetCore.Mvc;

	using System;
	using System.Linq;

	/// <summary>
	/// Контроллер управления месячным отчетом.
	/// </summary>
	public class MonthlyReportController : Controller, IDateController
	{
		/// <inheritdoc/>
		public IActionResult Index()
		{
			try
			{
				SetSourceContext();
				MonthlySource.Initialize();

				var length = MonthlySource.RF.Production.Length;

				ViewBag.Labels_en = new string[] { "Week 1", "Week 2", "Week 3", "Week 4", "Week 5", "Week 6" }.Take(length).ToArray();
				ViewBag.Labels_ru = new string[] { "Нед. 1", "Нед. 2", "Нед. 3", "Нед. 4", "Нед. 5", "Нед. 6" }.Take(length).ToArray();
				ViewBag.Error = "none";
			}
			catch
			{
				ViewBag.Error = "error";
			}

			return View();
		}

		[HttpGet]
		/// <inheritdoc/>
		public dynamic ChangeLang()
		{
			var now = DateTime.Now.Second;

			try
			{
				if (now % 15 >= 3 && now % 15 < 6)
				{
					SetSourceContext();
					MonthlySource.Update();
				}
			}
			catch { }

			var en = now >= 30;
			return new
			{
				wmMonthQtyName = en ? "This month line WM" : "Произведено стиральных машин за месяц",
				rfMonthQtyName = en ? "This month line RF" : "Произведено холодильников за месяц",
				chartName = en ? "Weekly production" : "Производство по неделям",
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
		public dynamic Update() => new
		{
			rfproduction = MonthlySource.RF?.Production.Select(x => x.TotalSum == 0 ? null : x.TotalSum.ToString()).ToArray(),
			rf1shift = MonthlySource.RF?.AverageOn1Shift,
			rf2shift = MonthlySource.RF?.AverageOn2Shift,
			rf3shift = MonthlySource.RF?.AverageOn3Shift,
			rfday = MonthlySource.RF?.AverageOnDay,

			wmproduction = MonthlySource.WM?.Production.Select(x => x.TotalSum == 0 ? null : x.TotalSum.ToString()).ToArray(),
			wm1shift = MonthlySource.WM?.AverageOn1Shift,
			wm2shift = MonthlySource.WM?.AverageOn2Shift,
			wm3shift = MonthlySource.WM?.AverageOn3Shift,
			wmday = MonthlySource.WM?.AverageOnDay,
		};

		[HttpGet]
		/// <inheritdoc/>
		public bool Refresh() => DateTime.Now.Hour == 7 && DateTime.Now.Minute == 0 && DateTime.Now.Second > 0 && DateTime.Now.Second < 3 && DateTime.Now.Day == 1;

		[HttpGet]
		public bool WaitCorrectData()
		{
			try
			{
				MonthlySource.Initialize();
				return true;
			}
			catch
			{
				return false;
			}
		}

		/// <inheritdoc/>
		public void SetSourceContext() => MonthlySource.SetContext(new());
	}
}