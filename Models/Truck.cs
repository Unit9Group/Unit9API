using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace AutoApi.Models
{
    public class Truck
    {
        [BsonId]
        [JsonPropertyName("id")]
        [BsonElement("id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Name")]
        [JsonPropertyName("Name")]
        public string? Name { get; set; }
        public string? Make { get; set; }
        public int Year { get; set; }
        public string? Color { get; set; }
        public string? Horsepower { get; set; }
    }
}
