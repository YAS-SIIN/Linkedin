using System;
using System.ComponentModel.DataAnnotations;

namespace Linkedin.Models
{
    public class Request : BaseEntity<int>
    {

        [Required]
        public DateTime ExpireDateTime { get; set; }

        [Required]
        public DateTime UpdateDateTime { get; set; }

        [Required]
        public int UserId { get; set; }

        public virtual User User { get; set; }

    }
}
