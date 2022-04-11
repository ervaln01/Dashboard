namespace Dashboard.Controllers
{
	using Dashboard.Models.TimePeriod;
	using System.Collections.Generic;

	/// <summary>
	/// Интерфейс дневного контроллера.
	/// </summary>
	public interface IDailyController
	{
		/// <summary>
		/// Обновление данных отчета по смене.
		/// </summary>
		/// <param name="index">Номер смены.</param>
		/// <returns>Отчет по смене.</returns>
		public dynamic UpdateShift(int index);

		/// <summary>
		/// Получение списка лейблов.
		/// </summary>
		/// <param name="shifts">Список смен.</param>
		/// <returns>Список лейблов часов.</returns>
		public static IEnumerable<string> GetLabels(List<Shift> shifts)
		{
			foreach (var shift in shifts)
			{
				if (shift.End < shift.Start) shift.End = shift.End.AddDays(1);
				for (var hour = shift.Start; hour < shift.End; hour = hour.AddHours(1))
				{
					yield return $" {hour.Hour:D2}-{hour.AddHours(1).Hour:D2}";
				}
			}
		}
	}
}