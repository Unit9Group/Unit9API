namespace AutoApi.Models
{
    public class VehiclesDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string TrucksCollectionName { get; set; } = null!;
        public string CarsCollectionName { get; set; } = null!;
        public string MotorcyclesCollectionName { get; set; } = null!;
    }
}
