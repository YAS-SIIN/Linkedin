using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkedin.Models
{
    public class Request : BaseEntity<int>
    {    
        [Required]
        public DateTime RequestDateTime { get; set; }

        [Required]
        public DateTime ExpireDateTime { get; set; }

        [Required]
        public DateTime UpdateDateTime { get; set; }
          
        [Required] 
        public int UserId { get; set; }

        public virtual User User { get; set; }
         
    }
}
