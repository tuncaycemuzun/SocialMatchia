namespace SocialMatchia.Common.Helpers
{
    public static class ImageHelper
    {
        public static string? CreateImage(string path, string image, string? fileNamePrefix = null)
        {
            if (string.IsNullOrEmpty(image) || !image.Contains(',')) return null;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var base64Data = image.Substring(image.IndexOf(",") + 1);
            base64Data = base64Data.Trim('\0');

            var dataUri = new DataUri(base64Data);
            var contentType = dataUri.MimeType;

            if (!Consts.MimeTypesToExtensions.TryGetValue(contentType, out var extension))
            {
                return null;
            }

            byte[] imageBytes = Convert.FromBase64String(image);

            var name = fileNamePrefix != null ? string.Join(" - ", fileNamePrefix, Guid.NewGuid().ToString()) : Guid.NewGuid().ToString();

            string fileName = string.Join(".", name, extension);

            var filePath = Path.Combine(path, fileName);

            File.WriteAllBytes(filePath, imageBytes);

            return fileName;
        }
    }
}
