﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.DbSet
{
    public class DiseaseInformation
    {
        [Key]
        public long ID { get; set; }
        public string Name { get; set; }

        // common field
        public DateTime? EntryDate { get; set; }
        public long? EntryUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long? UpdateUser { get; set; }
    }
}
