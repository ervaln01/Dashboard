namespace Dashboard.Models.WeatherChecker
{
	using System.Xml;

	/// <summary>
	/// Информационный блок прогноза.
	/// </summary>
	public static class Forecast
	{
		public class Attributes
		{
			/// <summary>
			/// День, на который составлен прогноз в данном блоке.
			/// </summary>
			public static int Day { get; private set; }

			/// <summary>
			/// Месяц, на который составлен прогноз в данном блоке.
			/// </summary>
			public static int Month { get; private set; }

			/// <summary>
			/// Год, на который составлен прогноз в данном блоке.
			/// </summary>
			public static int Year { get; private set; }

			/// <summary>
			/// Местное время, на которое составлен прогноз.
			/// </summary>
			public static int Hour { get; private set; }

			/// <summary>
			/// Время суток, для которого составлен прогноз: 0 - ночь 1 - утро, 2 - день, 3 - вечер
			/// </summary>
			public static int Tod { get; private set; }

			/// <summary>
			/// Заблаговременность прогноза в часах.
			/// </summary>
			public static int Predict { get; private set; }

			/// <summary>
			/// День недели, 1 - воскресенье, 2 - понедельник, и т.д.
			/// </summary>
			public static int Weekday { get; private set; }

			/// <summary>
			/// Инициализирует аттрибуты Forecast.
			/// </summary>
			/// <param name="node">Значимый узел xml-документа.</param>
			public Attributes(XmlNode forecast)
			{
				Day = forecast.GetValue("day");
				Month = forecast.GetValue("month");
				Year = forecast.GetValue("year");
				Hour = forecast.GetValue("hour");
				Tod = forecast.GetValue("tod");
				Predict = forecast.GetValue("predict");
				Weekday = forecast.GetValue("weekday");
			}
		}

		/// <summary>
		/// Атмосферные явления.
		/// </summary>
		public class Phenomena
		{
			/// <summary>
			/// Облачность по градациям: (-1) - туман, 0 - ясно, 1 - малооблачно, 2 - облачно, 3 - пасмурно.
			/// </summary>
			public static int Cloudiness { get; private set; }

			/// <summary>
			/// Тип осадков: 3 - смешанные, 4 - дождь, 5 - ливень, 6,7 – снег, 8 - гроза, 9 - нет данных, 10 - без осадков.
			/// </summary>
			public static int Precipitation { get; private set; }

			/// <summary>
			/// Интенсивность осадков, если они есть. 0 - возможен дождь/снег, 1 - дождь/снег.
			/// </summary>
			public static int Rpower { get; private set; }

			/// <summary>
			/// Вероятность грозы, если прогнозируется: 0 - возможна гроза, 1 - гроза.
			/// </summary>
			public static int Spower { get; private set; }

			/// <summary>
			/// Инициализирует аттрибуты Forecast/Phenomena.
			/// </summary>
			/// <param name="node">Значимый узел xml-документа.</param>
			public Phenomena(XmlNode phenomena)
			{
				Cloudiness = phenomena.GetValue("cloudiness");
				Precipitation = phenomena.GetValue("precipitation");
				Rpower = phenomena.GetValue("rpower");
				Spower = phenomena.GetValue("spower");
			}
		}

		/// <summary>
		/// Атмосферное давление, в мм.рт.ст.
		/// </summary>
		public class Pressure
		{
			/// <summary>
			/// Максимальное значение.
			/// </summary>
			public static int Max { get; private set; }

			/// <summary>
			/// Минимальное значение.
			/// </summary>
			public static int Min { get; private set; }

			/// <summary>
			/// Инициализирует аттрибуты Forecast/Pressure.
			/// </summary>
			/// <param name="node">Значимый узел xml-документа.</param>
			public Pressure(XmlNode pressure)
			{
				Max = pressure.GetValue("max");
				Min = pressure.GetValue("min");
			}
		}

		/// <summary>
		/// Температура воздуха (°C).
		/// </summary>
		public class Temperature
		{
			/// <summary>
			/// Максимальное значение.
			/// </summary>
			public static int Max { get; private set; }

			/// <summary>
			/// Минимальное значение.
			/// </summary>
			public static int Min { get; private set; }

			/// <summary>
			/// Инициализирует аттрибуты Forecast/Temperature.
			/// </summary>
			/// <param name="node">Значимый узел xml-документа.</param>
			public Temperature(XmlNode temperature)
			{
				Max = temperature.GetValue("max");
				Min = temperature.GetValue("min");
			}
		}

		/// <summary>
		/// Приземный ветер.
		/// </summary>
		public class Wind
		{
			/// <summary>
			/// Максимальное значения средней скорости ветра, без порывов (м/с).
			/// </summary>
			public static int Max { get; private set; }

			/// <summary>
			/// Минимальное значения средней скорости ветра, без порывов (м/с).
			/// </summary>
			public static int Min { get; private set; }

			/// <summary>
			/// Направление ветра в румбах от 1 до 8: 1 - северный, 2 - северо-восточный, и т.д.
			/// </summary>
			public static int Direction { get; private set; }

			/// <summary>
			/// Инициализирует аттрибуты Forecast/Wind.
			/// </summary>
			/// <param name="node">Значимый узел xml-документа.</param>
			public Wind(XmlNode wind)
			{
				Max = wind.GetValue("max");
				Min = wind.GetValue("min");
				Direction = wind.GetValue("direction");
			}
		}

		/// <summary>
		/// Относительная влажность воздуха (%).
		/// </summary>
		public class Relwet
		{
			/// <summary>
			/// Максимальное значение.
			/// </summary>
			public static int Max { get; private set; }

			/// <summary>
			/// Минимальное значение.
			/// </summary>
			public static int Min { get; private set; }

			/// <summary>
			/// Инициализирует аттрибуты Forecast/Relwet.
			/// </summary>
			/// <param name="node">Значимый узел xml-документа.</param>
			public Relwet(XmlNode relwet)
			{
				Max = relwet.GetValue("max");
				Min = relwet.GetValue("min");
			}
		}

		/// <summary>
		/// Температура воздуха по ощущению одетого в сезонную одежду человека.
		/// </summary>
		public class Heat
		{
			/// <summary>
			/// Максимальное значение.
			/// </summary>
			public static int Max { get; private set; }

			/// <summary>
			/// Минимальное значение.
			/// </summary>
			public static int Min { get; private set; }

			/// <summary>
			/// Инициализирует аттрибуты Forecast/Heat.
			/// </summary>
			/// <param name="node">Значимый узел xml-документа.</param>
			public Heat(XmlNode heat)
			{
				Max = heat.GetValue("max");
				Min = heat.GetValue("min");
			}
		}

		/// <summary>
		/// Парсит узел FORECAST xml-документа.
		/// </summary>
		/// <param name="node">Значимый узел xml-документа.</param>
		public static void Parse(XmlNode forecast)
		{
			new Attributes(forecast);
			new Phenomena(forecast["PHENOMENA"]);
			new Pressure(forecast["PRESSURE"]);
			new Temperature(forecast["TEMPERATURE"]);
			new Wind(forecast["WIND"]);
			new Relwet(forecast["RELWET"]);
			new Heat(forecast["HEAT"]);
		}

		/// <summary>
		/// Получает значение атрибута Xml-элемента.
		/// </summary>
		/// <param name="element">Xml-элемент.</param>
		/// <param name="attribute">Имя атрибута.</param>
		/// <returns>Значение атрибута.</returns>
		private static int GetValue(this XmlNode node, string attribute) => int.Parse(node.Attributes[attribute].Value);
	}
}