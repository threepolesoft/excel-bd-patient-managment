using System.Text.Json.Serialization;

namespace Common.Models
{
    public class User
    {
        [JsonPropertyName("Id")]
        public string Id { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Mob")]
        public string Mob { get; set; }

        [JsonPropertyName("Email")]
        public string Email { get; set; }
    }
}