using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ROS.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter the Name of the Category")]
        [RegularExpression(@"[A-Za-z]{4,}")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter the Description of the Category")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}