using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkedin.Models
{
    public class User : BaseEntity<int>
    {
         
        [Required] 
        [StringLength(100)]
        public string ExternalUserId { get; set; }
                            
        [Required] 
        public long VisitCount { get; set; }

        public virtual ICollection<Activity> Activity { get; set; }
        public virtual ICollection<Visit> Visit { get; set; }
        public virtual ICollection<Schedule> Schedule { get; set; }
        public virtual Request Request { get; set; }
    }
}
