namespace Dashboard.Controllers
{
	using Dashboard.Models;

	using Microsoft.AspNetCore.Mvc;
	using Microsoft.EntityFrameworkCore;

	using System;
	using System.Diagnostics;

	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			try
			{
				using var context = new ApplicationContext();
				context.Database.ExecuteSqlRaw("SELECT 1");
			}
			catch (Exception e)
			{
				return View($"{e.Message}\n{e.InnerException}\n{e.StackTrace}");
			}

			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}