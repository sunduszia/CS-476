﻿using System;
using System.ComponentModel.DataAnnotations;

namespace MyMentalHealth.Models
{
    public class UserModel
    {
        [Key]
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
        public UserModel()
        {
        }
    }
}

