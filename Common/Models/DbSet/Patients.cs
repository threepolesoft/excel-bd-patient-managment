﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Common.Models.DbSet
{
    public class Patients
    {
        [Key]
        [JsonPropertyName("ID")]
        public long ID { get; set; }

        [JsonPropertyName("PatientName")]
        public string PatientName { get; set; }

        [JsonPropertyName("DiseaseInformationID")]
        public long DiseaseInformationID { get; set; }

        // common field
        [JsonPropertyName("EntryDate")]
        public DateTime? EntryDate { get; set; }

        [JsonPropertyName("EntryUser")]
        public long? EntryUser { get; set; }

        [JsonPropertyName("UpdateDate")]
        public DateTime? UpdateDate { get; set; }

        [JsonPropertyName("UpdateUser")]
        public long? UpdateUser { get; set; }
    }
}
