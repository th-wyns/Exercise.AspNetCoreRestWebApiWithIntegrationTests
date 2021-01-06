using Users.API.IntegrationTests.Models;

namespace Users.API.IntegrationTests.Helpers
{
    public class ContentTypeRepresentations
    {
        public const string Json = "application/json";
        public const string Xml = "application/xml";
        public const string Unsupported = "application/not-supported-type";

        public static ContentType ToContentType(string mediaType)
        {
            switch (mediaType)
            {
                case Json:
                    return ContentType.Json;
                case Xml:
                    return ContentType.Xml;
                default:
                    return ContentType.Unsupported;
            }
        }

        public static string ToMediaType(ContentType ContentType)
        {
            switch (ContentType)
            {
                case ContentType.Json:
                    return Json;
                case ContentType.Xml:
                    return Xml;
                case ContentType.Unsupported:
                    return Unsupported;
                case ContentType.Unset:
                default:
                    return null;

            }
        }
    }
}
