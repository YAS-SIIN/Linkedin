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
        public DateTime UpdateDateTime { get; set; }
         
        [Required]
        public int UserId { get; set; }

        public virtual User User { get; set; } 
    }
}
