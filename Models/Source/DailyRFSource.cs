namespace Dashboard.Models.Source
{
	using Dashboard.Models.Enums;
	using Dashboard.Models.Production;

	/// <summary>
	/// Класс, предоставляющий интерфейс получения дневных данных по холодильникам.
	/// </summary>
	public class DailyRFSource : SourceShared<DayProduction>
	{
		/// <summary>
		/// Инициализация суточного отчета.
		/// </summary>
		public static void Initialize()
		{
			RF = new DayProduction(Line.RF, _context);
			RF = RF.DataFilling(_context, Line.RF, Location.RF);
		}

		/// <summary>
		/// Обновление суточного отчета.
		/// </summary>
		public static void Update() => RF = RF.DataUpdate(_context, Location.RF);
	}
}