using System.ComponentModel.DataAnnotations;

namespace Project_2023.Models
{
    

    public class CV
    {
        public int CVID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Nationality { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public int Grade { get; set; }
        public string Image { get; set; }

        [Display(Name ="Skills")]
        public ICollection<HasSkills> HasSkills { get; set; }
    }
}
