namespace Dashboard.Models.Production
{
	using Dashboard.Models.Enums;
	using Dashboard.Models.TimePeriod;

	using System;
	using System.Linq;

	/// <summary>
	/// Класс, предоставляющий данные о производстве за год.
	/// </summary>
	public class YearProduction : ProductionShared<Month>, IProduction
	{
		/// <summary>
		/// Среднее значение за месяц.
		/// </summary>
		public int AverageOnMonth;

		/// <summary>
		/// Конструктор класса <see cref="YearProduction"/>.
		/// </summary>
		public YearProduction()
		{
			AverageOnMonth = 0;
			Production = new Month[12];
			for (var index = 0; index < Production.Length; Production[index++] = new Month()) ;
		}

		/// <inheritdoc/>
		public void SetData(DateTime date, Line line, Location location, ApplicationContext context)
		{
			var period = new Period(date, date.AddMonths(1));
			var sumOfMonth = context.GetBarcodesCount(period, location);
			Production[date.Month - 1].TotalSum = sumOfMonth;
		}

		/// <inheritdoc/>
		public void SetAverageData()
		{
			var positiveMonthCount = Production.Count(month => month.TotalSum > 0);

			TotalProduction = Production.Sum(month => month.TotalSum);

			AverageOnMonth = positiveMonthCount == 0 ? 0 : TotalProduction / positiveMonthCount;
		}
	}
}