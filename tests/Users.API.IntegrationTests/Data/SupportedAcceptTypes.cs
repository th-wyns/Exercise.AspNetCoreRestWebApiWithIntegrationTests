using System.Collections.Generic;
using Users.API.IntegrationTests.Models;

namespace Users.API.IntegrationTests.Data
{
    public static class SupportedAcceptTypes
    {
        public static AcceptType Unset => AcceptType.Unset;
        public static AcceptType Json => AcceptType.Json;
        public static AcceptType Xml => AcceptType.Xml;
        public static AcceptType[] All => new[] { Unset, Json, Xml };

        public static IEnumerable<object[]> DataAll()
        {
            foreach (var acceptType in All)
            {
                yield return new object[] { acceptType };
            }
        }
    }
}
