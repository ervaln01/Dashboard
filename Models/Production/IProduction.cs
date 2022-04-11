namespace Dashboard.Models.Production
{
	using Dashboard.Models.Enums;

	using System;

	/// <summary>
	/// Интерфейс производственной модели.
	/// </summary>
	public interface IProduction
	{
		/// <summary>
		/// Заполнение производственной модели усредненными данными.
		/// </summary>
		public void SetAverageData();

		/// <summary>
		/// Заполнение производственной модели данными.
		/// </summary>
		/// <param name="date">Дата.</param>
		/// <param name="line">Номер линии.</param>
		/// <param name="location">Локация линии.</param>
		/// <param name="context">Контекст базы данных.</param>
		public void SetData(DateTime date, Line line, Location location, ApplicationContext context);
	}
}