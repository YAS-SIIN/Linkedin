using System;
using System.ComponentModel.DataAnnotations;

namespace Linkedin.Models
{
    public class Activity : BaseEntity<int>
    {

        [Required]
        [StringLength(100)]
        public string ActivityId { get; set; }

        [Required]
        public DateTime UpdateDateTime { get; set; }

        [Required]
        public int UserId { get; set; }

        public virtual User User { get; set; }
    }


}
