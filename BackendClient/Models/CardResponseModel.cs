using System.Text.Json.Serialization;

namespace BackendClientLib.Models
{
    public class CardResponseModel
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("description")]
        public required string Description { get; set; }

        public string ImageUrl { get; set; } = string.Empty;
    }
}
