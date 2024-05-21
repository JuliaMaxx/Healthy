using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Healthy.Models
{
    public class Entry
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Required]
        [Display(Name = "Meal Type")]
        public MealType? MealType { get; set; }
        public bool Ate { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Quantity must be a non-negative value.")]
        public double Quantity { get; set; }

        [Required]
        [Display(Name = "Food")]
        public int FoodId { get; set; }

        [ForeignKey("FoodId")]
        public Food Food { get; set; }

        [Display(Name = "Intake Time")]
        public DateTime IntakeTime { get; set; }

        [Required]
        public string? IdentityUserId { get; set; }

        [ForeignKey("IdentityUserId")]
        public IdentityUser? User { get; set; }

        public Entry()
        {

        }
    }

    // Enum for meal types
    public enum MealType
    {
        Breakfast,
        Lunch,
        Dinner,
        Drink,
        Snack
    }
}



