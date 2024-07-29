using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_2023.Data;
using Project_2023.Models;
using Project_2023.ViewModels;

namespace Project_2023.Pages.CVs
{
    public class SendCVModel : PageModel
    {
        [BindProperty]
        public CVVM CVVM { get; set; }

        public static int i = 1;

        public Array GenderItems = Enum.GetValues(typeof(Gender));
        public List<Skill> SkillItems { get; set; }
        public CVContext _context { get; set; }
        public IEnumerable<SelectListItem> NationalityItems { get; set; } = new List<SelectListItem>
        {
                new SelectListItem("American","American"),
                new SelectListItem("Europian","Europian"),
                new SelectListItem("Middle Eastern","Middle Eastern"),
                new SelectListItem("Asian","Asian")
        };

        public SendCVModel(CVContext context)
        {
            _context = context;
            SkillItems = _context.Skills.ToList();
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            else if((CVVM.x + CVVM.y) != CVVM.sum) return Page();

            Models.CV newCV = new Models.CV();
            newCV.FirstName = CVVM.FirstName;
            newCV.LastName = CVVM.LastName;
            newCV.Birthday = CVVM.Birthday;
            newCV.Nationality = CVVM.Nationality;
            newCV.Gender = CVVM.Gender.ToString();
            newCV.Email = CVVM.Email;

            //SKills
            newCV.HasSkills = new List<HasSkills>();
            foreach (int skillId in CVVM.Skills)
            {
                var hasSkill = new HasSkills { SkillId = skillId, CVId = newCV.CVID };
                newCV.HasSkills.Add(hasSkill);
            }

            //Grades
            int grade = 10 * newCV.HasSkills.Count();
            if (CVVM.Gender.CompareTo(Gender.Female) == 0) grade += 10;
            else grade += 5;
            newCV.Grade = grade;

            //Image
            if (CVVM.photo == null || CVVM.photo.Length == 0)
            {
                return Content("File not selected");
            }
            string name = CVVM.FirstName + "_" + CVVM.LastName + "_" + i + ".jpg";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", name);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await CVVM.photo.CopyToAsync(stream);
                stream.Close();
                i++;
            }

            newCV.Image = "\\Images\\" + name;

            _context.Add(newCV);
            _context.SaveChanges();

            return RedirectToPage("/CVs/BrowseCV");
        }
    }
}
