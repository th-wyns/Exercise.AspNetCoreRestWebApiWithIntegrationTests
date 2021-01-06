using System.Collections.Generic;
using Users.API.IntegrationTests.Models;

namespace Users.API.IntegrationTests.Data
{
    public static class SupportedContentTypes
    {
        public static ContentType Json => ContentType.Json;
        public static ContentType Xml => ContentType.Xml;
        public static ContentType[] All => new[] { Json, Xml };

        public static IEnumerable<object[]> DataAll()
        {
            foreach (var contentType in All)
            {
                yield return new object[] { contentType };
            }
        }
    }
}
