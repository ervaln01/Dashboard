namespace Dashboard
{
	using Microsoft.Extensions.Configuration;

	public static class Settings
	{
		public static string ConnectionString { get; private set; }
		public static void Initialize(IConfiguration config) => ConnectionString = config.GetConnectionString("DashboardCS").Replace("%CONTENTROOTPATH%", System.IO.Directory.GetCurrentDirectory());
	}
}