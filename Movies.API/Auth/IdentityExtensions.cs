namespace Movies.API.Auth
{
    public static class IdentityExtensions
    {
        public static string? GetUserId(this HttpContext context)
        {
            var userId = context.User.Claims.FirstOrDefault(x => x.Type == "userid");

            if(string.IsNullOrEmpty(userId?.Value)) return null; 

            return userId.Value;
        }
    }
}
