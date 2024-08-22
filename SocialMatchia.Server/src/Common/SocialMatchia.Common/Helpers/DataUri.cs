namespace SocialMatchia.Common.Helpers
{
    public class DataUri
    {
        public string MimeType { get; }
        public string Base64Content { get; }

        public DataUri(string base64Image)
        {
            var commaIndex = base64Image.IndexOf(',');
            if (commaIndex == -1 || !base64Image.StartsWith("data:"))
            {
                throw new ArgumentException("Geçersiz Base64 veri URI'si.");
            }

            MimeType = base64Image.Substring(5, commaIndex - 5).Split(';')[0];
            Base64Content = base64Image.Substring(commaIndex + 1);
        }
    }
}
