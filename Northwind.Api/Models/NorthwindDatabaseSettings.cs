namespace Northwind.Api.Models
{
    public class NorthwindDatabaseSettings : INorthwindDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface INorthwindDatabaseSettings
    { 
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}