using Users.API.IntegrationTests.Models;

namespace Users.API.IntegrationTests.Helpers
{
    public class AcceptTypeRepresentations
    {
        public const string Json = "application/json";
        public const string Xml = "application/xml";
        public const string Unsupported = "application/not-supported-type";

        public static AcceptType ToAcceptType(string mediaType)
        {
            switch (mediaType)
            {
                case Json:
                    return AcceptType.Json;
                case Xml:
                    return AcceptType.Xml;
                default:
                    return AcceptType.Unsupported;
            }
        }

        public static string ToMediaType(AcceptType acceptType)
        {
            switch (acceptType)
            {
                case AcceptType.Json:
                    return Json;
                case AcceptType.Xml:
                    return Xml;
                case AcceptType.Unsupported:
                    return Unsupported;
                case AcceptType.Unset:
                default:
                    return null;

            }
        }
    }
}
