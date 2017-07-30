using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ROS.Models
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage="Please enter the Name of the Recipe")]
        [RegularExpression(@"[A-Za-z]{4,}")]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Required]
        [Range(50,80000)]
        [DataType(DataType.Currency)]
        public float Price { get; set; }
        [ForeignKey("Category")]
        [Display(Name="Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; } 

        public virtual List<Restaurant> Restaurents { get; set; }
    }
}