
using System.Text.Json.Serialization;

namespace Common.Models
{

    public class Res
    {
        [JsonPropertyName("Status")]
        public bool Status { get; set; } = true;

        [JsonPropertyName("Message")]
        public string Message { get; set; } = ActionStatus.Success;

        [JsonPropertyName("Data")]
        public object Data { get; set; }
    }
}