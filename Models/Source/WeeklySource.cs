namespace Dashboard.Models.Source
{
	using Dashboard.Models.Enums;
	using Dashboard.Models.Production;
	using System;

	/// <summary>
	/// Класс, предоставляющий интерфейс получения недельных данных.
	/// </summary>
	public class WeeklySource : SourceShared<WeekProduction>
	{
		/// <summary>
		/// Инициализация недельного отчета.
		/// </summary>
		public static void Initialize()
		{
			RF = new WeekProduction();
			RF = DataFilling(RF, Line.RF, Location.RF);

			WM = new WeekProduction();
			WM = DataFilling(WM, Line.WM, Location.WM);
		}

		/// <summary>
		/// Обновление недельного отчета.
		/// </summary>
		public static void Update()
		{
			RF = DataUpdate(RF, Line.RF, Location.RF);
			WM = DataUpdate(WM, Line.WM, Location.WM);
		}

		/// <summary>
		/// Установка первоначального значения отчета. 
		/// </summary>
		/// <param name="dayProd">Текущее содержание отчета о продукции.</param>
		/// <param name="lineNumber">Номер линии.</param>
		/// <param name="location">Локация линии.</param>
		/// <returns>Отчет, заполненный первоначальными данными.</returns>
		private static WeekProduction DataFilling(WeekProduction weekProd, Line line, Location location)
		{
			var now = DateTime.Now;
			var differenceDay = now.DayOfWeek - DayOfWeek.Monday;
			if (differenceDay == -1) differenceDay = 6;

			var nowDay = now.AddDays(-differenceDay);
			for (var day = nowDay; day < nowDay.AddDays(7); day = day.AddDays(1))
			{
				weekProd.SetData(day, line, location, _context);
			}
			return weekProd;
		}

		/// <summary>
		/// Обновление данных отчета.
		/// </summary>
		/// <param name="dayProd">Текущее содержание отчёта о продукции.</param>
		/// <param name="location">Локация линии.</param>
		/// <returns>Отчет, заполненный обновленными данными.</returns>
		private static WeekProduction DataUpdate(WeekProduction weekProd, Line line, Location location)
		{
			weekProd.SetData(DateTime.Now, line, location, _context);
			weekProd.SetAverageData();
			return weekProd;
		}
	}
}