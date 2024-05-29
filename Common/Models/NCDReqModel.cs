using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class NCDReqModel
    {
        [Required(ErrorMessage = "Id is required")]
        public long ID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }
    }
}
