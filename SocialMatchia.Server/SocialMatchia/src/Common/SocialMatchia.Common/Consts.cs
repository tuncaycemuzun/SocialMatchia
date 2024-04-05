namespace SocialMatchia.Common
{
    public class Consts
    {
        public static readonly Guid Admin = new Guid("5e74cbbf-0ad2-4ecf-bb7c-c801e1b68ab9");
        public static readonly Guid Basic = new Guid("b74c7082-267d-4521-9643-9035eed02a38");

        public static readonly string AdminUser = Guid.NewGuid().ToString();
        public static readonly string BasicUser = Guid.NewGuid().ToString();

        public static Dictionary<string, string> MimeTypesToExtensions = new Dictionary<string, string>
        {
            {"image/jpeg", ".jpg"},
            {"image/png", ".png"},
            {"image/gif", ".gif"},
            {"image/bmp", ".bmp"},
            {"image/svg+xml", ".svg"},
            {"image/webp", ".webp"},
            {"image/heic", ".heic"},
        };
    }
}
