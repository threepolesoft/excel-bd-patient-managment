using System.Text.Json.Serialization;

namespace Common.Models
{
    public class LoginModelReq
    {
        [JsonPropertyName("UserName")]
        public string UserName { get; set; }

        [JsonPropertyName("Password")]
        public string Password { get; set; }
    }

    public class LoginModelRes
    {
        [JsonPropertyName("Token")]
        public string Token { get; set; }

        [JsonPropertyName("Expiry")]
        public DateTime Expiry { get; set; }
    }

}