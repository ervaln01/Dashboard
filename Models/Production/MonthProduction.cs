namespace Dashboard.Models.Production
{
	using Dashboard.Models.Enums;
	using Dashboard.Models.TimePeriod;

	using System;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// Класс, предоставляющий данные о производстве за месяц.
	/// </summary>
	public class MonthProduction : ProductionShared<Week>, IProduction
	{
		/// <summary>
		/// Конструктор класса <see cref="MonthProduction"/>.
		/// </summary>
		/// <param name="date">Дата.</param>
		public MonthProduction(DateTime date)
		{
			TotalProduction = 0;
			var weeks = GetWeekDates(date);
			Production = new Week[weeks.Count];
			for (var index = 0; index < Production.Length; index++)
				Production[index] = new Week(weeks[index].Start, weeks[index].End);

			AverageOn1Shift = 0;
			AverageOn2Shift = 0;
			AverageOn3Shift = 0;
			AverageOnDay = 0;
		}

		/// <inheritdoc/>
		public void SetAverageData()
		{
			for (var index = 0; index < Production.Length; index++)
			{
				foreach (var day in Production[index].Days.Keys)
					Production[index].Days[day].TotalSum = Production[index].Days[day].Shift.Sum();

				Production[index].TotalSum = Production[index].Days.Sum(day => day.Value.TotalSum);
			}

			TotalProduction = Production.Sum(week => week.TotalSum);

			var positiveShift1Count = Production.Sum(week => week.Days.Count(day => day.Value.Shift[0] > 0));
			var positiveShift2Count = Production.Sum(week => week.Days.Count(day => day.Value.Shift[1] > 0));
			var positiveShift3Count = Production.Sum(week => week.Days.Count(day => day.Value.Shift[2] > 0));
			var positiveDayCount = Production.Sum(week => week.Days.Count(day => day.Value.TotalSum > 0));

			AverageOn1Shift = positiveShift1Count == 0 ? 0 : Production.Sum(week => week.Days.Sum(day => day.Value.Shift[0])) / positiveShift1Count;
			AverageOn2Shift = positiveShift2Count == 0 ? 0 : Production.Sum(week => week.Days.Sum(day => day.Value.Shift[1])) / positiveShift2Count;
			AverageOn3Shift = positiveShift3Count == 0 ? 0 : Production.Sum(week => week.Days.Sum(day => day.Value.Shift[2])) / positiveShift3Count;
			AverageOnDay = positiveDayCount == 0 ? 0 : TotalProduction / positiveDayCount;
		}

		/// <inheritdoc/>
		public void SetData(DateTime date, Line line, Location location, ApplicationContext context) => context.GetTimelines(date, line).ForEach(timeline =>
		{
			var period = new Period(timeline.ShiftBegin, timeline.ShiftEnd > timeline.ShiftBegin ? timeline.ShiftEnd : timeline.ShiftEnd.AddDays(1));
			var shift = context.GetBarcodesCount(period, location);

			var index = Production.ToList().IndexOf(Production.FirstOrDefault(x => x.Start <= date && x.End > date));
			Production[index].Days[date.DayOfWeek].Shift[timeline.ShiftNumber - 1] = shift;
		});

		/// <summary>
		/// Получение дат начала и конца недель в месяце.
		/// </summary>
		/// <param name="targetMonth">Целевой месяц.</param>
		/// <returns>Список из дат начала и конца недель в месяце.</returns>
		private static List<Period> GetWeekDates(DateTime targetMonth)
		{
			var startMonth = new DateTime(targetMonth.Year, targetMonth.Month, 1);
			var endMonth = startMonth.AddMonths(1);
			var weeks = new List<Period>();

			for (var day = startMonth; day < endMonth; day = day.AddDays(7))
			{
				while (day.DayOfWeek != DayOfWeek.Monday) day = day.AddDays(-1);

				var startWeek = day < startMonth ? startMonth : day;
				var endWeek = day.AddDays(7) > endMonth ? endMonth : day.AddDays(7);

				weeks.Add(new Period(startWeek, endWeek));
			}
			return weeks;
		}
	}
}