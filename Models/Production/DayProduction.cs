namespace Dashboard.Models.Production
{
	using Dashboard.Models.Enums;
	using Dashboard.Models.TimePeriod;

	using System;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// Класс, предоставляющий данные о производстве за день.
	/// </summary>
	public class DayProduction
	{
		/// <summary>
		/// План линии на сутки.
		/// </summary>			
		public int Plan;

		/// <summary>
		/// Список значений производства по часам.
		/// </summary>
		public List<int> Actual;

		/// <summary>
		/// Список смен на текущую дату.
		/// </summary>
		public List<Shift> Shifts;

		/// <summary>
		/// Текущая дата.
		/// </summary>		
		public DateTime Date;

		/// <summary>
		/// Конструктор класса <see cref="DayProduction"/>.
		/// </summary>
		/// <param name="line">Линия.</param>
		/// <param name="context">Контекст БД.</param>
		public DayProduction(Line line, ApplicationContext context)
		{
			Plan = 0;
			Actual = new List<int>();
			var now = DateTime.Now;
			Date = new DateTime(now.Year, now.Month, now.Day);
			Shifts = SetShifts(line, context).Distinct().ToList();
			if (Shifts.Count > 0 && Shifts.Last().Start < Shifts.First().Start)
			{
				var index = Shifts.Count - 1;
				Shifts[index].Start = Shifts[index].Start.AddDays(1);
				Shifts[index].End = Shifts[index].End.AddDays(1);
			}
		}

		/// <summary>
		/// Установка смен на текущую дату.
		/// </summary>
		/// <param name="line">Линия.</param>
		/// <param name="context">Контекст БД.</param>
		private IEnumerable<Shift> SetShifts(Line line, ApplicationContext context) => context
			.GetTimelines(Date, line)
			.OrderBy(timeline => timeline.ShiftNumber)
			.Select(timeline => new Shift { Plan = 0, Actual = 0, Start = timeline.ShiftBegin, End = timeline.ShiftEnd, Number = timeline.ShiftNumber - 1 });
	}
}