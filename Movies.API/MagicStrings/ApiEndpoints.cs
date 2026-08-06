namespace Movies.API.MagicStrings
{
    public static class ApiEndpoints
    {
        private const string ApiBase = "api/v{version:apiVersion}";
        public static class Movies
        {

            public const string Base = $"{ApiBase}/movies";
            public const string Create = "";
            public const string Get = $"{{idOrSlug}}";
            public const string GetAll = "";
            public const string Update = $"{{id:guid}}";
            public const string Delete = $"{{id:guid}}";

            public const string Rate = $"{{id:guid}}/ratings";
            public const string DeleteRating = $"{{id:guid}}/ratings";

        }
        public static class Ratings
        {
            public const string Base = $"ratings";
            public const string GetUserRatings = $"me";
        }
    }
}
