namespace Dashboard.Controllers
{
	using Dashboard.Models.Source;

	using Microsoft.AspNetCore.Mvc;

	using System;
	using System.Linq;

	/// <summary>
	/// Контроллер управления годовым отчетом.
	/// </summary>
	public class YearlyReportController : Controller, IDateController
	{
		/// <inheritdoc/>
		public IActionResult Index()
		{
			try
			{
				SetSourceContext();
				YearlySource.Initialize();
				ViewBag.Error = "none";
			}
			catch
			{
				ViewBag.Error = "error";
			}

			ViewBag.Labels_en = new string[] { "JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC" };
			ViewBag.Labels_ru = new string[] { "ЯНВ", "ФЕВ", "МАР", "АПР", "МАЙ", "ИЮН", "ИЮЛ", "АВГ", "СЕН", "ОКТ", "НОЯ", "ДЕК" };

			return View();
		}

		[HttpGet]
		/// <inheritdoc/>
		public dynamic ChangeLang()
		{
			var now = DateTime.Now.Second;

			try
			{
				if (now % 15 >= 0 && now % 15 < 3)
				{
					SetSourceContext();
					YearlySource.Update();
				}
			}
			catch { }

			var en = now >= 30;
			return new
			{
				wmYearQtyName = en ? "This year line WM" : "Произведено стиральных машин за год",
				rfYearQtyName = en ? "This year line RF" : "Произведено холодильников за год",
				wmchartName = en ? "Monthly production WM" : "Производство стиральных машин по месяцам",
				rfchartName = en ? "Monthly production RF" : "Производство холодильников по месяцам",
				productionName = en ? "Average production" : "Средние показатели",
				averageName = en ? "Month" : "За месяц",
				totalName = en ? "Total production" : "Суммарные показатели",
				yearName = en ? "Year" : "За год",
				en
			};
		}

		[HttpGet]
		/// <inheritdoc/>
		public dynamic Update() => new
		{
			rfproduction = YearlySource.RF?.Production.Select(x => x.TotalSum == 0 ? null : x.TotalSum.ToString()).ToArray(),
			rfaverage = YearlySource.RF?.AverageOnMonth,

			wmproduction = YearlySource.WM?.Production.Select(x => x.TotalSum == 0 ? null : x.TotalSum.ToString()).ToArray(),
			wmaverage = YearlySource.WM?.AverageOnMonth,
		};

		[HttpGet]
		/// <inheritdoc/>
		public bool Refresh() => DateTime.Now.Hour == 7 && DateTime.Now.Minute == 0 && DateTime.Now.Second > 0 && DateTime.Now.Second < 3 && DateTime.Now.Month == 1 && DateTime.Now.Day == 1;

		[HttpGet]
		public bool WaitCorrectData()
		{
			try
			{
				YearlySource.Initialize();
				return true;
			}
			catch
			{
				return false;
			}
		}

		/// <inheritdoc/>
		public void SetSourceContext() => YearlySource.SetContext(new());
	}
}