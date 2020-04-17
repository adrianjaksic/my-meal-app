using System.Collections.Generic;
using System.Text;

namespace Common.Extensions
{
    public static class EnumerableExtensions
    {
        public static string ConvertToCsvString<T>(this IEnumerable<T> data)
        {
            var sb = new StringBuilder();
            foreach (var item in data)
            {
                sb.Append(item.ToString());
                sb.Append(',');
            }
            return sb.ToString();
        }
    }
}
