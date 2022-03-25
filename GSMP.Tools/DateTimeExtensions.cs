using System;

namespace GSMP.Tools
{
	public static class DateTimeExtensions
	{
		public static bool IsSameDay(this DateTime target, DateTime date) => target.Date == date.Date;

        public static bool IsStartOfDay(this DateTime date) => date == date.Date;

		public static DateTime StartOfDay(this DateTime date) => date.Date;

        public static DateTime EndOfDay(this DateTime date)
		{
			return date.Date
				.AddDays(1)
				.AddTicks(-1);
		}

		public static DateTime SetKind(this DateTime date, DateTimeKind kind) => DateTime.SpecifyKind(date, kind);

        public static DateTime SetKindUtc(this DateTime date) => SetKind(date, DateTimeKind.Utc);

        public static string ToAmericanDateFormat(this DateTime date) => date.ToString("MM/dd/yyyy");

		public static string ToAmericanDateFormat(this DateTime? date) => date.HasValue ? date.Value.ToAmericanDateFormat() : string.Empty;

		public static string ToAmericanTimeFormat(this DateTime date) => date.ToString("hh:mm tt");

		public static string ToAmericanTimeFormat(this DateTime? date) => date.HasValue ? date.Value.ToAmericanTimeFormat() : string.Empty;

		public static DateTime MonthStart(this DateTime date) => date.AddDays(1 - date.Day).Date;

        public static DateTime PrevMonthStart(this DateTime date) => date.MonthStart().AddMonths(-1);

        public static string ToStringOrEmpty(this DateTime date) => date == default ? string.Empty : date.ToString();

		public static DateTime Normalize(this DateTime date)
		{
			return date.Kind == DateTimeKind.Unspecified
				? DateTime.SpecifyKind(date, DateTimeKind.Utc)
				: date;
		}

		public static DateTime AddTime(this DateTime date, string time, IFormatProvider formatProvider = null)
		{
			if (string.IsNullOrWhiteSpace(time))
				throw new ArgumentNullException(nameof(time));

			var timeOfDay = DateTime.Parse(time, formatProvider);

			return date.AddTime(timeOfDay);
		}

		public static DateTime AddTime(this DateTime date, DateTime? time)
		{
			return time.HasValue
				? new DateTime(date.Year, date.Month, date.Day, time.Value.Hour, time.Value.Minute, 0)
				: date;
		}

		public static int GetTimeStampGuid(this DateTime pmcTime)
		{
			var val = (pmcTime.Minute * 60000)
				+ (pmcTime.Second * 1000)
				+ pmcTime.Millisecond
				+ 100000;

			return int.MaxValue - val;
		}
	}
}
