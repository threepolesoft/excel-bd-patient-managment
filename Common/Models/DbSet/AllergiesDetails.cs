using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.DbSet
{
    public class AllergiesDetails
    {
        [Key]
        public long ID { get; set; }
        public long? PatientID { get; set; }
        public long? AllergiesID { get; set; }

        // common field
        public DateTime? EntryDate { get; set; }
        public string? EntryUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? UpdateUser { get; set; }
    }
}
