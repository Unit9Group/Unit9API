namespace AutoApi.Models
{
    public class AutoDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string AutoCollectionName { get; set; } = null!;
    }
}
