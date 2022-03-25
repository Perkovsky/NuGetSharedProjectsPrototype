using System.Linq;
using System.Collections.Generic;

namespace GSMP.Tools
{
	public static class EnumerableExtensions
	{
		public static bool HasAny<T>(this IEnumerable<T> collection)
		{
			return collection is not null && collection.Any();
		}
	}
}
