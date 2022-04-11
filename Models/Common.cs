namespace Dashboard.Models
{
	using Dashboard.Models.Entity;
	using Dashboard.Models.Enums;
	using Dashboard.Models.TimePeriod;

	using System;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// Общие методы расширения.
	/// </summary>
	public static class Common
	{
		/// <summary>
		/// Получение количества баркодов.
		/// </summary>
		/// <param name="context">Контекст базы данных.</param>
		/// <param name="period">Период запроса.</param>
		/// <param name="location">Локация запроса.</param>
		/// <returns>Количество баркодов.</returns>
		public static int GetBarcodesCount(this ApplicationContext context, Period period, Location location) => context.Fixedbarcodes.Count(fixedbarcode =>
			!fixedbarcode.Backup.Equals("1") &&
			!fixedbarcode.Backup.Equals("2") &&
			fixedbarcode.Time >= period.Start &&
			fixedbarcode.Time < period.End &&
			fixedbarcode.Location == (int)location);

		/// <summary>
		/// Получение списка активных смен.
		/// </summary>
		/// <param name="context">Контекст базы данных.</param>
		/// <param name="date">Дата.</param>
		/// <param name="line">Линия.</param>
		/// <returns>Список активных смен на целевую дату.</returns>
		public static List<ShiftTimeline> GetTimelines(this ApplicationContext context, DateTime date, Line line) => context.ShiftTimelines.Where(shiftTimeline =>
			shiftTimeline.IsActive &&
			shiftTimeline.TargetDate.Year == date.Year &&
			shiftTimeline.TargetDate.Month == date.Month &&
			shiftTimeline.TargetDate.Day == date.Day &&
			shiftTimeline.Line == (int)line).ToList();
	}
}