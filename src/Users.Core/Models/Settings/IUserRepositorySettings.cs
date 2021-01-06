namespace Users.Models.Settings
{
    public interface IUserRepositorySettings
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
