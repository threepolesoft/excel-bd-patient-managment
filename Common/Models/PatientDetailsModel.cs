using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Common.Models
{
    public class PatientDetailsModel
    {
        [JsonPropertyName("Disease")]
        public string Disease { get; set; }

        [JsonPropertyName("Epilepsy")]
        public bool Epilepsy { get; set; }

        [JsonPropertyName("OthersNCDs")]
        public List<NCDModel> OthersNCDs { get; set; }

        [JsonPropertyName("Allergies")]
        public List<AllergiesModel> Allergies { get; set; }
    }
}
