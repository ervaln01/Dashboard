namespace Dashboard.Models.Production
{
	using Dashboard.Models.Enums;
	using Dashboard.Models.TimePeriod;

	using System;
	using System.Linq;

	/// <summary>
	/// Класс, предоставляющий данные о производстве за неделю.
	/// </summary>
	public class WeekProduction : ProductionShared<Day>, IProduction
	{
		/// <summary>
		/// Конструктор класса <see cref="WeekProduction"/>.
		/// </summary>
		public WeekProduction()
		{
			TotalProduction = 0;
			Production = new Day[7];
			for (var index = 0; index < Production.Length; Production[index++] = new Day()) ;
			AverageOn1Shift = 0;
			AverageOn2Shift = 0;
			AverageOn3Shift = 0;
			AverageOnDay = 0;
		}

		/// <inheritdoc/>
		public void SetAverageData()
		{
			for (var index = 0; index < Production.Length; index++)
				Production[index].TotalSum = Production[index].Shift.Sum();

			TotalProduction = Production.Sum(day => day.TotalSum);

			var positiveShift1Count = Production.Count(day => day.Shift[0] > 0);
			var positiveShift2Count = Production.Count(day => day.Shift[1] > 0);
			var positiveShift3Count = Production.Count(day => day.Shift[2] > 0);
			var positiveDayCount = Production.Count(day => day.Shift.Any(shiftNumber => shiftNumber > 0));

			AverageOn1Shift = positiveShift1Count == 0 ? 0 : Production.Sum(day => day.Shift[0]) / positiveShift1Count;
			AverageOn2Shift = positiveShift2Count == 0 ? 0 : Production.Sum(day => day.Shift[1]) / positiveShift2Count;
			AverageOn3Shift = positiveShift3Count == 0 ? 0 : Production.Sum(day => day.Shift[2]) / positiveShift3Count;
			AverageOnDay = positiveDayCount == 0 ? 0 : TotalProduction / positiveDayCount;
		}

		/// <inheritdoc/>
		public void SetData(DateTime date, Line line, Location location, ApplicationContext context) => context.GetTimelines(date, line).ForEach(timeline =>
		{
			var period = new Period(timeline.ShiftBegin, timeline.ShiftEnd > timeline.ShiftBegin ? timeline.ShiftEnd : timeline.ShiftEnd.AddDays(1));
			var shift = context.GetBarcodesCount(period, location);

			var dayOfWeek = (date.DayOfWeek == 0 ? 7 : (int)date.DayOfWeek) - 1;

			Production[dayOfWeek].Shift[timeline.ShiftNumber - 1] = shift;
		});
	}
}