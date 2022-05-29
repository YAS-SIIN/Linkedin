using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkedin.Models
{
    public class Activity : BaseEntity<int>
    {


        [Required]
        [StringLength(100)]
        public string ActivityId { get; set; }

        [Required]
        public DateTime CreateDateTime { get; set; }

        [Required]
        public Int16 Status { get; set; }

        [Required]
        public DateTime LikeDateTime { get; set; }


        [Required]
        [StringLength(100)]
        public string UserId { get; set; }

        public virtual User User { get; set; }


    }
}
