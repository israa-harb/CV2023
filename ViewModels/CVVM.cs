using Microsoft.AspNetCore.Mvc.Rendering;
using Project_2023.Models;
using System.ComponentModel.DataAnnotations;

namespace Project_2023.ViewModels
{
    public enum Gender
    {
        Male, Female
    }

    public class CVVM
    {
        [Required(ErrorMessage ="First Name Required")]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage ="Last Name Required")]
        [Display(Name ="Last Name")]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage ="Birthday Required")]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "Nationality Required")]
        public string Nationality { get; set; }

        [Required(ErrorMessage ="Required")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage ="Pick at least one skill")]
        public List<int> Skills { get; set; }
        
        [EmailAddress]
        [Required(ErrorMessage ="Email Address Required")]
        public string Email { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Confirm Email Address Required")]
        [Compare("Email",ErrorMessage ="Emails don't match")]
        [Display(Name ="Confirm Email")]
        public string ConfirmEmail { get; set; }

        [Required]
        [Display(Name = "Photo")]
        public IFormFile photo { get; set; }

        public int Grade { get; set; }

        [Required(ErrorMessage ="First value is required")]
        [Range(1,20,ErrorMessage ="First value should be between 1 and 20")]
        public int x { get; set; }

        [Required(ErrorMessage ="Second value is required")]
        [Range(20, 50,ErrorMessage="Second value should be between 20 and 50")]
        public int y { get; set; }

        [Required(ErrorMessage ="Third value is required")]
        public int sum { get; set; }

        
    }
}
