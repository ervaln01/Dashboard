namespace Dashboard.Models
{
	using Dashboard.Models.Entity;
	using Microsoft.EntityFrameworkCore;

	/// <summary>
	/// Контекст подклчения к базе данных.
	/// </summary>
	public partial class ApplicationContext : DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
				optionsBuilder.UseSqlServer(Settings.ConnectionString);
		}

		/// <summary>
		/// Таблица FIXEDBARCODE.
		/// </summary>
		public virtual DbSet<Fixedbarcode> Fixedbarcodes { get; set; }

		/// <summary>
		/// Таблица PRODUCTION_SHEDULE.
		/// </summary>
		public virtual DbSet<ProductionShedule> ProductionShedules { get; set; }

		/// <summary>
		/// Таблица SHIFT_TIMELINES.
		/// </summary>
		public virtual DbSet<ShiftTimeline> ShiftTimelines { get; set; }

		/// <summary>
		/// Настраивает взаимосвязи сущностей.
		/// </summary>
		/// <param name="modelBuilder">Построитель модели.</param>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasAnnotation("Relational:Collation", "Turkish_CI_AS");

			modelBuilder.Entity<Fixedbarcode>(entity => entity.HasKey(e => new { e.Product, e.Serial, e.Location, e.Time }).HasName("aaaaaFIXEDBARCODE_PK").IsClustered(false));

			modelBuilder.Entity<ProductionShedule>(entity =>
			{
				entity.HasKey(e => new { e.Versus, e.Product, e.Model, e.Definition, e.Date, e.Count, e.Created });

				entity.Property(e => e.Versus).HasComment("Версия  файла с которой взяты данные, необходимо для учета изменений");
				entity.Property(e => e.Product).IsUnicode(false).HasComment("Сток номер продукта");
				entity.Property(e => e.Model).IsUnicode(false);
				entity.Property(e => e.Definition).IsUnicode(false);
				entity.Property(e => e.Date).HasComment("Планируемая дата производства");
				entity.Property(e => e.Notes).IsUnicode(false);
			});

			base.OnModelCreating(modelBuilder);
		}
	}
}