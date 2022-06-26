using System;
using System.ComponentModel.DataAnnotations;

namespace Linkedin.Models
{
    public class Schedule : BaseEntity<int>
    {

        [Required]
        public DateTime UpdateDateTime { get; set; }

        [Required]
        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
