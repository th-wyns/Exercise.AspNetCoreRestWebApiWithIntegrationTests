using System;

namespace Users.API.IntegrationTests.Models
{
    [Flags]
    public enum ContentType
    {
        Unset = 0,
        Json = 1,
        Xml = 2,
        Unsupported = 4,
    }
}
