namespace Dashboard.Models.WeatherChecker
{
	using System;
	using System.Collections.Generic;
	using System.Xml;

	/// <summary>
	/// Класс парсинга XML-документа погоды.
	/// </summary>
	public class XmlWeatherParser
	{
		/// <summary>
		/// Ссылка на xml погоду Киржача. Проверить ссылку можно 2 способами:
		/// 1. На ресурсе https://www.meteoservice.ru/content/export получить ссылку
		/// 2. Сравнить аттрибуты "TOWN latitude=56 longitude=39"
		/// </summary>
		private const string url = "https://xml.meteoservice.ru/export/gismeteo/point/7318.xml";

		/// <summary>
		/// Путь к папке с картинками .../img
		/// </summary>
		private readonly string _path;

		/// <summary>
		/// Конструктор класса <see cref="XmlWeatherParser"/>.
		/// </summary>
		/// <param name="path">Путь к папке с картинками погоды .../img</param>
		public XmlWeatherParser(string path) => _path = path;

		/// <summary>
		/// Парсит Xml-документ.
		/// </summary>
		/// <returns>Пара значений температуры и пути к нужной картинке.</returns>
		public KeyValuePair<int, string> Parse()
		{
			var node = GetNode();
			if (node == null) throw new Exception("Не удалось получить Xml-узел.");

			try
			{
				Forecast.Parse(node);

				var storm = Forecast.Phenomena.Spower == 0 ? string.Empty : "\\storm";
				var filePath = Forecast.Phenomena.Precipitation switch
				{
					3 => $"complexWeather\\mix{storm}",
					4 => $"complexWeather\\rain{storm}",
					5 => $"complexWeather\\heavyRain{storm}",
					6 or 7 => $"complexWeather\\snow{storm}",
					8 => $"stormWeather",
					9 or 10 => $"cloudWeather",
					_ => string.Empty
				};

				var source = $"{_path}\\{filePath}\\{Forecast.Phenomena.Cloudiness}.png";
				return new KeyValuePair<int, string>(Forecast.Temperature.Max, source);
			}
			catch
			{
				return new KeyValuePair<int, string>(0, string.Empty);
			}
		}

		/// <summary>
		/// Получение необходимого узла Xml-документа.
		/// </summary>
		/// <returns>Узел Xml-документа.</returns>
		private static XmlNode GetNode()
		{
			try
			{
				var xmlDocument = new XmlDocument();
				xmlDocument.Load(new XmlTextReader(url));
				return xmlDocument.DocumentElement.SelectNodes("//FORECAST")[0];
			}
			catch
			{
				return null;
			}
		}
	}
}