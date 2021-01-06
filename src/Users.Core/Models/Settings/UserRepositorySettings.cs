namespace Users.Models.Settings
{
    public class UserRepositorySettings : IUserRepositorySettings
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

        public const string SectionName = "UserRepository";
    }
}
