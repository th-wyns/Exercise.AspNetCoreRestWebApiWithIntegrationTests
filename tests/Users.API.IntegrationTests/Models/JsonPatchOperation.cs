namespace Users.API.IntegrationTests.Models
{
    public class JsonPatchOperation
    {
        public string Op { get; set; }
        public string Path { get; set; }
        public string Value { get; set; }
    }
}
