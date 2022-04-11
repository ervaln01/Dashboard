namespace Dashboard.Controllers
{
	using Dashboard.Models.WeatherChecker;

	using Microsoft.AspNetCore.Mvc;

	using System;

	/// <summary>
	/// Контроллер управления демонстрационным отчетом с погодой и эмблемами предприятия.
	/// </summary>
	public class LabelReportController : Controller
	{
		/// <summary>
		/// Названия месяцев на русском языке.
		/// </summary>
		private static readonly string[] months_ru = { "Января", "Февраля", "Марта", "Апреля", "Мая", "Июня", "Июля", "Августа", "Сентября", "Октября", "Ноября", "Декабря" };

		/// <summary>
		/// Названия месяцев на английском языке.
		/// </summary>
		private static readonly string[] months_en = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

		/// <summary>
		/// Названия дней недели на русском языке.
		/// </summary>
		private static readonly string[] dayOfWeek_ru = { "Воскресенье", "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота" };

		/// <summary>
		/// Названия дней недели на английском языке.
		/// </summary>
		private static readonly string[] dayOfWeek_en = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };

		/// <summary>
		/// Отрисовка представления.
		/// </summary>
		/// <returns>Представление.</returns>
		public IActionResult Index() => View();

		/// <summary>
		/// Получение данных о дате и времени на одном из языков.
		/// </summary>
		/// <returns>Данные о дате и времени.</returns>
		public dynamic GetData()
		{
			var now = DateTime.Now;
			var en = now.Second >= 30;
			return new
			{
				day = now.Day,
				month = en ? months_en[now.Month - 1] : months_ru[now.Month - 1],
				weekday = en ? dayOfWeek_en[(int)now.DayOfWeek] : dayOfWeek_ru[(int)now.DayOfWeek],
				time = now.ToString("HH:mm")
			};
		}

		/// <summary>
		/// Получение данных о погоде.
		/// </summary>
		/// <returns>Html-разметка с данными о погоде.</returns>
		public string GetWeather()
		{
			var parser = new XmlWeatherParser("img\\weather");

			try
			{
				var result = parser.Parse();

				var src = result.Value;
				return string.IsNullOrEmpty(src) ?
					throw new Exception() :
					$"<div>" +
					$"<img src=\"{src}\" style=\"margin:auto;height:75%;display:block;margin-left:auto;margin-right:auto;\"/>" +
					$"</div>" +
					$"<div style=\"height:25%;font-size:4em;text-align:center\">{result.Key}°C</div>";
			}
			catch
			{
				return
					"<div style=\"font-size:2em\">" +
					"<p>Сервис погоды</p>" +
					"<p>недоступен</p>" +
					"<p>-------------------------</p>" +
					"<p>Weather service</p>" +
					"<p>is disabled</p>" +
					"</div>";
			}
		}
	}
}