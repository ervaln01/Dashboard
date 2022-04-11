namespace Dashboard.Models.Entity
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	using Microsoft.EntityFrameworkCore;

	[Table("SHIFT_TIMELINES")]
	[Index(nameof(TargetDate), nameof(Line), nameof(IsActive), Name = "IX_SHIFT_TIMELINES_TARGET_DATE_LINE_IS_ACTIVE")]
	public partial class ShiftTimeline
	{
		[Key]
		public int Id { get; set; }

		[Column("TARGET_DATE")]
		public DateTime TargetDate { get; set; }

		[Column("LINE")]
		public int Line { get; set; }

		[Column("SHIFT_NUMBER")]
		public int ShiftNumber { get; set; }

		[Column("SHIFT_ID")]
		public int ShiftId { get; set; }

		[Column("SHIFT_BEGIN")]
		public DateTime ShiftBegin { get; set; }

		[Column("SHIFT_END")]
		public DateTime ShiftEnd { get; set; }

		[Column("IS_ACTIVE")]
		public bool IsActive { get; set; }

		[Column("LUNCH_BEGIN")]
		public DateTime? LunchBegin { get; set; }

		[Column("LUNCH_END")]
		public DateTime? LunchEnd { get; set; }

		[Column("BREAK1_BEGIN")]
		public DateTime? Break1Begin { get; set; }

		[Column("BREAK1_END")]
		public DateTime? Break1End { get; set; }

		[Column("BREAK2_BEGIN")]
		public DateTime? Break2Begin { get; set; }

		[Column("BREAK2_END")]
		public DateTime? Break2End { get; set; }

		[Column("BREAK3_BEGIN")]
		public DateTime? Break3Begin { get; set; }

		[Column("BREAK3_END")]
		public DateTime? Break3End { get; set; }

		[Column("INSERT_TIME")]
		public DateTime InsertTime { get; set; }

		[Required]
		[Column("INSERT_USER")]
		[StringLength(12)]
		public string InsertUser { get; set; }
	}
}