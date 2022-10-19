using System;
using System.Collections.Generic;

#nullable disable

namespace MyMentalHealth.Data
{
    public partial class User
    {
        public int UserId { get; set; }
        public string UName { get; set; }
        public byte[] UPassword { get; set; }
        public string UEmail { get; set; }
    }
}
