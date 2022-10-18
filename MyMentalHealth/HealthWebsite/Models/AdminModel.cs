﻿using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace HealthWebsite.Models
{
    
    public class AdminModel
    {
        [Key]
        public int ID { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }
        
    }
}
