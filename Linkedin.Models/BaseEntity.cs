using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Linkedin.Models
{
    public interface IEntity
    {  
        public Int16 Status { get; set; }
    }

    public abstract class BaseEntity<TKey> : IEntity
    { 
        [Required]
        public TKey Id { get; set; }

        [Required]
        public short Status { get; set; }
    }

    public abstract class BaseEntity : BaseEntity<int>
    {
    }
}