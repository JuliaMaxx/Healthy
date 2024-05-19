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
        public double Calories { get; set; }

        [Required]
        public double Fat { get; set; }

        [Required]
        public double Carbs { get; set; }

        [Required]
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
