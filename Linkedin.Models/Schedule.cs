using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkedin.Models
{
    public class  Schedule : BaseEntity<int>
    {
     
        [Required]
        public DateTime CreateDateTime { get; set; }

        [Required]
        public DateTime NextVisitStartDateTime { get; set; }
         
        [Required]
        [StringLength(100)]
        public string Priority { get; set; }

        [Required]
        public Int16 Status { get; set; }

        [Required]
        [StringLength(100)]
        public string UserId { get; set; }

        public virtual User User { get; set; } 
    }
}
