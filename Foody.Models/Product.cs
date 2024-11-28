using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Foody.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }


        public string? Description { get; set; }
        [Required]
        [Display(Name = "Price")]
        [Range(1001, int.MaxValue, ErrorMessage = "The total must be greater than 1000 VND.")]
        public double Price { get; set; }

        [Required]
        [Display(Name = "Origin Price")]
        [Range(1001, int.MaxValue, ErrorMessage = "The total must be greater than 1000 VND.")]
        public double ListPrice { get; set; }
     
        [ForeignKey(nameof(Category))]
        [Required]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }
        [ValidateNever]
        public string? imageUrl { get; set; }



    }
}
