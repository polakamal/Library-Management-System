﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Management_System.Data.Model
{
    public class Customer
    {
        public int CustomerId { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public String Name { get; set; }

    }
}
