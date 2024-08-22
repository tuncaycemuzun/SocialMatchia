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

            byte[] imageBytes;
            try
            {
                imageBytes = Convert.FromBase64String(base64Data);
            }
            catch (FormatException)
            {
                return null;
            }

            var dataUriHeader = image.Substring(0, image.IndexOf(","));
            var contentType = dataUriHeader.Split(';')[0].Split(':')[1];

            if (!Consts.MimeTypesToExtensions.TryGetValue(contentType, out var extension))
            {
                return null;
            }

            var name = fileNamePrefix != null ? string.Join(" - ", fileNamePrefix, Guid.NewGuid().ToString()) : Guid.NewGuid().ToString();

            string fileName = string.Join("", name, extension);

            var filePath = Path.Combine(path, fileName);

            File.WriteAllBytes(filePath, imageBytes);

            return fileName;
        }

        public static string? ConvertImageToBase64(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return null;
            }

            try
            {
                byte[] imageBytes = File.ReadAllBytes(filePath);
                string base64String = Convert.ToBase64String(imageBytes);
                string contentType = GetMimeType(filePath); // MIME türünü almak için bir yardımcı metot kullanıyoruz
                return $"data:{contentType};base64,{base64String}";
            }
            catch (Exception)
            {
                // Hata durumunda null döndürürüz
                return null;
            }
        }

        private static string GetMimeType(string filePath)
        {
            // Dosya uzantısına göre MIME türünü döndürürüz
            string extension = Path.GetExtension(filePath).ToLowerInvariant();

            switch (extension)
            {
                case ".jpg":
                case ".jpeg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                case ".gif":
                    return "image/gif";
                case ".bmp":
                    return "image/bmp";
                case ".webp":
                    return "image/webp";
                // Daha fazla dosya uzantısı ekleyebilirsiniz
                default:
                    return "application/octet-stream"; // Varsayılan MIME türü
            }
        }

    }
}
