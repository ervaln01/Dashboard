namespace Dashboard.Models.TimePeriod
{
	using System;

	/// <summary>
	/// Временной период.
	/// </summary>
	public class Period
	{
		/// <summary>
		/// Начало периода.
		/// </summary>
		public DateTime Start { get; private set; }

		/// <summary>
		/// Конец периода.
		/// </summary>
		public DateTime End { get; private set; }

		/// <summary>
		/// Конструктор класса <see cref="Period"/>.
		/// </summary>
		/// <param name="start">Начало периода.</param>
		/// <param name="end">Конец периода.</param>
		public Period(DateTime start, DateTime end)
		{
			Start = start;
			End = end;
		}
	}
}