using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace GSMP.Tools
{
	public static class StringExtensions
	{
		public static string Capitalize(this string str)
		{
			return string.IsNullOrEmpty(str)
				? str
				: CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
		}

		public static bool IsDigitsOnly(this string str)
		{
			foreach (char c in str)
			{
				if (c < '0' || c > '9')
					return false;
			}

			return true;
		}

		public static string ReplaceSpaceChars(this string text)
		{
			return text
				.Replace('\u00A0', ' ')
				.Replace('\u0009', ' ');
		}

		public static string FirstCharToUpper(this string str)
		{
			if (string.IsNullOrWhiteSpace(str))
				return str;

			return str.First()
				.ToString()
				.ToUpper() + str.Substring(1);
		}

		public static string FirstCharToLower(this string @string)
		{
			return string.IsNullOrEmpty(@string)
				? @string
				: @string.First().ToString().ToLower() + @string.Substring(1);
		}

		public static string ToTitle(this string str)
		{
			var separator = " ";
			var pattern = new Regex("(?<=[a-z0-9])[A-Z]", RegexOptions.Compiled);

			return pattern.Replace(str, m => separator + m.Value)
				.ToLowerInvariant()
				.FirstCharToUpper();
		}

		public static string[] Split(this string str, string separator, StringSplitOptions options = StringSplitOptions.None)
		{
			return str.Split(new[] { separator }, options);
		}

		public static TEnum ToEnum<TEnum>(this string value)
			where TEnum : struct, Enum
		{
			if (Enum.TryParse<TEnum>(value, out TEnum result))
				return result;

			return default;
		}

		public static bool Has(this string str, string test, StringComparison comparisonType = StringComparison.Ordinal)
		{
			if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(test))
				return false;

			return str.IndexOf(test, comparisonType) > -1;
		}
	}
}
