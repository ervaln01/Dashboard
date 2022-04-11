namespace Dashboard.Models.Source
{
	using Dashboard.Models;
	using Dashboard.Models.Enums;
	using Dashboard.Models.Production;
	using Dashboard.Models.TimePeriod;

	using System;
	using System.Linq;

	/// <summary>
	/// Класс, предоставляющий интерфейс получения дневных данных.
	/// </summary>
	public static class DailyCommon
	{
		/// <summary>
		/// Установка первоначального значения отчета. 
		/// </summary>
		/// <param name="dayProd">Текущее содержание отчета о продукции.</param>
		/// <param name="line">Номер линии.</param>
		/// <param name="location">Локация линии.</param>
		/// <returns>Отчет, заполненный первоначальными данными.</returns>
		public static DayProduction DataFilling(this DayProduction dayProd, ApplicationContext context, Line line, Location location)
		{
			var maxVersus = context.ProductionShedules
				.Where(productionSchedule => productionSchedule.Date == dayProd.Date)
				.Max(x => x.Versus);

			dayProd.Plan = context.ProductionShedules
				.Where(productionSchedule => productionSchedule.Date == dayProd.Date && productionSchedule.Line == (int)line && productionSchedule.Versus == maxVersus)
				.Sum(productionSchedule => productionSchedule.Count);
			try
			{
				var temp = dayProd.Shifts[0].Start;
			}
			catch
			{
				throw new Exception(nameof(Shift));
			}

			var startDay = dayProd.Shifts[0].Start;
			for (var shift = 0; shift < dayProd.Shifts.Count; shift++)
			{
				var start = dayProd.Shifts[shift].Start;
				var end = dayProd.Shifts[shift].End;
				if (end < start) end = end.AddDays(1);

				dayProd.Shifts[shift].Plan = dayProd.Plan / dayProd.Shifts.Count;
				dayProd.Shifts[shift].Actual = context.GetBarcodesCount(new Period(start, end), location);

				for (var date = start; date < end; date = date.AddMinutes(30))
				{
					var actual = context.GetBarcodesCount(new Period(date, date.AddMinutes(30)), location);
					if ((date - startDay).Hours >= dayProd.Actual.Count)
					{
						dayProd.Actual.Add(actual);
						continue;
					}

					dayProd.Actual[(date - startDay).Hours] += actual;
				}
			}
			return dayProd;
		}

		/// <summary>
		/// Обновление данных отчета.
		/// </summary>
		/// <param name="dayProd">Текущее содержание отчёта о продукции.</param>
		/// <param name="location">Локация линии.</param>
		/// <returns>Отчет, заполненный обновленными данными.</returns>
		public static DayProduction DataUpdate(this DayProduction dayProd, ApplicationContext context, Location location)
		{
			var now = DateTime.Now;
			var firstShift = dayProd.Shifts.Where(shift => shift.Number == dayProd.Shifts.Min(shift => shift.Number)).FirstOrDefault();
			if (firstShift == null) return null;

			var index = now - firstShift.Start;
			var actualPeriod = new Period(dayProd.Date.AddHours(now.Hour), dayProd.Date.AddHours(now.Hour + 1));
			if (index.Hours < dayProd.Actual.Count) dayProd.Actual[index.Hours] = context.GetBarcodesCount(actualPeriod, location);

			var shift = dayProd.Shifts.FirstOrDefault(shift => shift.Start < now && (shift.End > shift.Start ? shift.End : shift.End.AddDays(1)) > now);
			var shiftIndex = dayProd.Shifts.IndexOf(shift);
			try
			{
				var shiftPeriod = new Period(dayProd.Shifts[shiftIndex].Start, dayProd.Shifts[shiftIndex].End);
				dayProd.Shifts[shiftIndex].Actual = context.GetBarcodesCount(shiftPeriod, location);
			}
			catch { }

			return dayProd;
		}
	}
}