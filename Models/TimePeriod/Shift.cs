namespace Dashboard.Models.TimePeriod
{
	using System;

	/// <summary>
	/// Класс, представляющий собой данные о производстве за смену.
	/// </summary>
	public class Shift
	{
		/// <summary>
		/// Дата/время начала смены.
		/// </summary>
		public DateTime Start;

		/// <summary>
		/// Дата/время конца смены.
		/// </summary>
		public DateTime End;

		/// <summary>
		/// План на смену.
		/// </summary>
		public int Plan;

		/// <summary>
		/// Актуальное производство за смену.
		/// </summary>
		public int Actual;

		/// <summary>
		/// Номер смены.
		/// </summary>
		public int Number;
	}
}