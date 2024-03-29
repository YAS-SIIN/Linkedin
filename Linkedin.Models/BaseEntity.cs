﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Linkedin.Models
{
    public interface IEntity
    {
        public Int16 Status { get; set; }
        public DateTime CreateDateTime { get; set; }
    }

    public abstract class BaseEntity<TKey> : IEntity
    {
        [Key]
        public TKey Id { get; set; }

        [Required]
        public short Status { get; set; }

        [Required]
        public DateTime CreateDateTime { get; set; }
    }

    public abstract class BaseEntity : BaseEntity<int>
    {
    }
}