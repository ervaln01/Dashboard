namespace Dashboard.Models.TimePeriod
{
	using System;
	using System.Collections.Generic;

	/// <summary>
	/// Класс, представляющий собой данные о производстве за неделю.
	/// </summary>
	public class Week
	{
		/// <summary>
		/// Суммарное количество продукции за неделю.
		/// </summary>
		public int TotalSum;

		/// <summary>
		/// Дни, входящие в текущую неделю.
		/// </summary>
		public Dictionary<DayOfWeek, Day> Days;

		/// <summary>
		/// Дата/время начала недели.
		/// </summary>
		public DateTime Start;

		/// <summary>
		/// Дата/время конца недели.
		/// </summary>
		public DateTime End;

		/// <summary>
		/// Конструктор класса <see cref="Week"/>.
		/// </summary>
		/// <param name="start">Дата/время начала недели.</param>
		/// <param name="end">Дата/время конца недели.</param>
		public Week(DateTime start, DateTime end)
		{
			Start = start;
			End = end;
			Days = new Dictionary<DayOfWeek, Day>();// new Day[(end - start).Days];
			for (var index = start; index < end; index = index.AddDays(1)) 
				Days.Add(index.DayOfWeek, new Day());
		}
	}
}