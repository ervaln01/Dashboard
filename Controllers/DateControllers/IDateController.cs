namespace Dashboard.Controllers
{
	using Microsoft.AspNetCore.Mvc;

	/// <summary>
	/// Интерфейс контроллера универсального периода дат.
	/// </summary>
	public interface IDateController
	{
		/// <summary>
		/// Задание параметров и отрисовка представления.
		/// </summary>
		/// <returns>Представление.</returns>
		public IActionResult Index();

		/// <summary>
		/// Обновление языка лейблов.
		/// </summary>
		/// <returns>Текущий набор лейблов.</returns>
		public dynamic ChangeLang();

		/// <summary>
		/// Обновление данных отчета.
		/// </summary>
		/// <returns>Данные отчета.</returns>
		public dynamic Update();

		/// <summary>
		/// Проверка на обновление страницы.
		/// </summary>
		/// <returns>Страницу требуется обновить?</returns>
		public bool Refresh();

		/// <summary>
		/// Установка контекста.
		/// </summary>
		public void SetSourceContext();
	}
}