namespace Movies.API.Auth
{
    public static class AuthConstants
    {
        //برو policy به نام "Admin" را اجرا کن
        //و آن policy داخلش می‌گوید:

        //بررسی کن آیا توکن claim به نام "admin" دارد یا نه

        public const string AdminUserPolicyName = "Admin";
        public  const string AdminUserClaimName = "admin";

        public const string TrustedMemberPolicyName = "trusted";
        public const string TrustedMemberClaimName = "trusted_member";
    }
}
