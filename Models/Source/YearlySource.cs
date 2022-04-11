namespace Dashboard.Models.Source
{
	using Dashboard.Models.Enums;
	using Dashboard.Models.Production;
	using System;

	/// <summary>
	/// Класс, предоставляющий интерфейс получения годовых данных по холодильникам.
	/// </summary>
	public class YearlySource : SourceShared<YearProduction>
	{
		/// <summary>
		/// Инициализация годового отчета.
		/// </summary>
		public static void Initialize()
		{
			RF = new YearProduction();
			RF = DataFilling(RF, Line.RF, Location.RF);

			WM = new YearProduction();
			WM = DataFilling(WM, Line.WM, Location.WM);
		}

		/// <summary>
		/// Обновление годового отчета.
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
		private static YearProduction DataFilling(YearProduction yearProd, Line line, Location location)
		{
			var now = DateTime.Now;
			var start = new DateTime(now.Year, 1, 1);
			var end = start.AddYears(1).AddDays(-1);
			for (var month = start; month < end; month = month.AddMonths(1))
			{
				yearProd.SetData(month, line, location, _context);
			}
			return yearProd;
		}

		/// <summary>
		/// Обновление данных отчета.
		/// </summary>
		/// <param name="dayProd">Текущее содержание отчёта о продукции.</param>
		/// <param name="location">Локация линии.</param>
		/// <returns>Отчет, заполненный обновленными данными.</returns>
		private static YearProduction DataUpdate(YearProduction yearProd, Line line, Location location)
		{
			var date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
			yearProd.SetData(date, line, location, _context);
			yearProd.SetAverageData();
			return yearProd;
		}
	}
}