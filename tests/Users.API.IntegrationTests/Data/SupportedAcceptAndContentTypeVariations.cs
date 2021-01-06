using System.Collections.Generic;

namespace Users.API.IntegrationTests.Data
{
    public class SupportedAcceptAndContentTypeVariations
    {
        public static IEnumerable<object[]> DataAll()
        {
            foreach (var acceptType in SupportedAcceptTypes.All)
            {
                foreach (var contentType in SupportedContentTypes.All)
                {
                    yield return new object[] { acceptType, contentType };
                }
            }
        }
    }
}
