namespace Dashboard.Models.Source
{
	using Dashboard.Models.Enums;
	using Dashboard.Models.Production;

	/// <summary>
	/// Класс, предоставляющий интерфейс получения дневных данных по стиральным машинам.
	/// </summary>
	public class DailyWMSource : SourceShared<DayProduction>
	{
		/// <summary>
		/// Инициализация суточного отчета.
		/// </summary>
		public static void Initialize()
		{
			WM = new DayProduction(Line.WM, _context);
			WM = WM.DataFilling(_context, Line.WM, Location.WM);
		}

		/// <summary>
		/// Обновление суточного отчета.
		/// </summary>
		public static void Update() => WM = WM.DataUpdate(_context, Location.WM);
	}
}