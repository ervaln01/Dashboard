namespace Dashboard.Models.Source
{
	using Dashboard.Models.Enums;
	using Dashboard.Models.Production;

	using System;

	/// <summary>
	/// Класс, предоставляющий интерфейс получения месячных данных по холодильникам.
	/// </summary>
	public class MonthlySource : SourceShared<MonthProduction>
	{
		/// <summary>
		/// Инициализация месячного отчета.
		/// </summary>
		public static void Initialize()
		{
			RF = new MonthProduction(DateTime.Now);
			RF = DataFilling(RF, Line.RF, Location.RF);

			WM = new MonthProduction(DateTime.Now);
			WM = DataFilling(WM, Line.WM, Location.WM);
		}

		/// <summary>
		/// Обновление месячного отчета.
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
		private static MonthProduction DataFilling(MonthProduction monthProd, Line line, Location location)
		{
			var now = DateTime.Now;
			var start = new DateTime(now.Year, now.Month, 1);
			var end = start.AddMonths(1);
			for (var month = start; month < end; month = month.AddDays(1))
			{
				monthProd.SetData(month, line, location, _context);
			}
			return monthProd;
		}

		/// <summary>
		/// Обновление данных отчета.
		/// </summary>
		/// <param name="dayProd">Текущее содержание отчёта о продукции.</param>
		/// <param name="location">Локация линии.</param>
		/// <returns>Отчет, заполненный обновленными данными.</returns>
		private static MonthProduction DataUpdate(MonthProduction monthProd, Line line, Location location)
		{
			monthProd.SetData(DateTime.Now, line, location, _context);
			monthProd.SetAverageData();
			return monthProd;
		}
	}
}