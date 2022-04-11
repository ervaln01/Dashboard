namespace Dashboard.Models.Production
{
	/// <summary>
	/// Класс, общий для моделей производства.
	/// </summary>
	/// <typeparam name="Range">Тип коллекции элементов (период).</typeparam>
	public class ProductionShared<Range>
	{
		/// <summary>
		/// Произведено за период <see cref="Range"/>. 
		/// </summary>
		public int TotalProduction;

		/// <summary>
		/// Коллекция элементов типа <see cref="Range"/>.
		/// </summary>
		public Range[] Production;

		/// <summary>
		/// Среднее значение за первую смену.
		/// </summary>
		public int AverageOn1Shift;

		/// <summary>
		/// Среднее значение за вторую смену.
		/// </summary>
		public int AverageOn2Shift;

		/// <summary>
		/// Среднее значение за третью смену.
		/// </summary>
		public int AverageOn3Shift;

		/// <summary>
		/// Среднее значение за день.
		/// </summary>
		public int AverageOnDay;
	}
}