using System;
using System.Collections.Generic;

#nullable disable

namespace MyMentalHealth.Data
{
    public partial class Admin
    {
        public int AdminId { get; set; }
        public string AName { get; set; }
        public string AEmail { get; set; }
        public byte[] APassword { get; set; }
    }
}
