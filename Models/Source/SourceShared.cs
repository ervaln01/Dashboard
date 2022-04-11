namespace Dashboard.Models.Source
{
	public class SourceShared<T>
	{
		/// <summary>
		/// Контекст БД.
		/// </summary>
		protected static ApplicationContext _context;

		/// <summary>
		/// Установка контекста.
		/// </summary>
		/// <param name="context">Контекст базы данных.</param>
		public static void SetContext(ApplicationContext context) => _context = context;

		/// <summary>
		/// Отчет годового производства холодильников.
		/// </summary>
		public static T RF { get; protected set; }

		/// <summary>
		/// Отчет годового производства холодильников.
		/// </summary>
		public static T WM { get; protected set; }
	}
}