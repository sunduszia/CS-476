using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace MyMentalHealth.Models
{
    public class Exercise: Contents
    {
        

        [Display(Name = "Video Link")]
        public string VideoLink { get; set; }


        
    }
}

