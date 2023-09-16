namespace BookStoreApi.Models
{
    public class DatabaseSettingModel
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string BookCollectionName { get; set; } = null!;

    }
}
