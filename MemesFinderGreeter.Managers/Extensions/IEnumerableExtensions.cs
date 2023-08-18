using System;
namespace MemesFinderGreeter.Managers.Extensions
{
	public static class IEnumerableExtensions
	{
		public static string GetFormattedString(this IEnumerable<string> strings, Func<string, string> formatter)
		{
			if (strings is null || !strings.Any())
				return string.Empty;

			if (strings.Count() == 1)
				return formatter(strings.First());

			return strings.Select(s => $"{formatter(s)} ").Aggregate((f, s) => f + s);
        }
	}
}

