using System.ComponentModel.DataAnnotations;

namespace Linkedin.Models
{
    public class Visit : BaseEntity<int>
    {
        [Required]
        public int UserId { get; set; }

        public virtual User User { get; set; }


    }
}
