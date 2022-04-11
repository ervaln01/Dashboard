namespace Dashboard.Controllers
{
	using Dashboard.Models.Source;

	using Microsoft.AspNetCore.Mvc;

	/// <summary>
	/// Контроллер управления десятимиллионным отчетом.
	/// </summary>
	public class Record10MController : Controller
	{
		/// <summary>
		/// Количество произведенных товаров за период до конца 2020 года.
		/// </summary>
		private const int totalCount = 9_315_671;
		private const int totalCount2021 = 1_003_140;

		/// <summary>
		/// Количество произведенных товаров за текущий год.
		/// </summary>
		private static int totalCountCurrentYear = 0;

		/// <summary>
		/// Отрисовка представления.
		/// </summary>
		/// <returns>Представление.</returns>
		public IActionResult Index() => View();

		/// <summary>
		/// Получение текущего количества произведенной продукции за весь период производства.
		/// </summary>
		/// <returns>Текущее количество произведенной продукции разделенное на цифры.</returns>
		public dynamic GetDigits()
		{
			var rf = YearlySource.RF == null ? 0 : YearlySource.RF.TotalProduction;
			var wm = YearlySource.WM == null ? 0 : YearlySource.WM.TotalProduction;
			if (totalCountCurrentYear < rf + wm)
				totalCountCurrentYear = rf + wm;

			var total = totalCount + totalCount2021 + totalCountCurrentYear;

			return new 
			{
				flag = total >= 10_000_000,
				d1 = $"{total % 10}", 
				d10 = $"{(total /= 10) % 10}", 
				d100 = $"{(total /= 10) % 10}", 
				t1 = $"{(total /= 10) % 10}", 
				t10 = $"{(total /= 10) % 10}", 
				t100 = $"{(total /= 10) % 10}", 
				m1 = $"{(total /= 10) % 10}", 
				m10 = (total /= 10) % 10 == 0 ? "empty" : $"{total % 10}",
			};
		}
	}
}