using System.Text.RegularExpressions;

namespace Movies.Applications.Utilities
{
    public static partial class SlugGenerator
    {
         public static string Generate(string title, int year)
         {
                var sluggedTitle = SlugRegex()
                    .Replace(title, "")
                    .ToLower()
                    .Replace(" ", "-");

                return $"{sluggedTitle}-{year}";
         }

        [GeneratedRegex("[^0-9A-Za-z_-]", RegexOptions.NonBacktracking, 5)]
        private static partial Regex SlugRegex();


    }
}
