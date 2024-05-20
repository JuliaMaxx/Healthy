using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Healthy.Models
{
    public class Food
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Reference { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Calories must be a positive value.")]
        public double Calories { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Fat must be a non-negative value.")]
        public double Fat { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Carbs must be a non-negative value.")]
        public double Carbs { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Protein must be a non-negative value.")]
        public double Protein { get; set; }

        [Required]
        public string? IdentityUserId { get; set; }

        [ForeignKey("IdentityUserId")]
        public IdentityUser? User { get; set; }

        public Food()
        {

        }
    }
}
