namespace Dashboard.Models.TimePeriod
{
	/// <summary>
	/// Класс, представляющий собой данные о производстве за месяц.
	/// </summary>
	public class Month
	{
		/// <summary>
		/// Суммарное количество продукции за месяц.
		/// </summary>
		public int TotalSum;

		/// <summary>
		/// Конструктор класса <see cref="Month"/>.
		/// </summary>
		public Month() => TotalSum = 0;
	}
}