namespace Dashboard.Models.TimePeriod
{
	/// <summary>
	/// Класс, представляющий собой данные о производстве за день.
	/// </summary>
	public class Day
	{
		/// <summary>
		/// Суммарное количество продукции за день.
		/// </summary>
		public int TotalSum;

		/// <summary>
		/// Смены, входящие в день.
		/// </summary>
		public int[] Shift;

		/// <summary>
		/// Конструктор класса <see cref="Day"/>.
		/// </summary>
		public Day()
		{
			Shift = new int[3];
			for (var index = 0; index < Shift.Length; Shift[index++] = 0) ;
		}
	}
}