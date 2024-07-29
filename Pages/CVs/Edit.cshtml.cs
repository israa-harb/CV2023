using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_2023.Data;
using Project_2023.Models;
using Project_2023.ViewModels;

namespace Project_2023.Pages.CVs
{
    public class EditModel : PageModel
    {
        private readonly CVContext _context;
        private readonly IWebHostEnvironment _environment;

        public Array GenderItems = Enum.GetValues(typeof(Gender));
        public List<Skill> SkillItems { get; set; }
        public IEnumerable<SelectListItem> NationalityItems { get; set; } = new List<SelectListItem>
        {
                new SelectListItem("American","american"),
                new SelectListItem("Europian","europrian"),
                new SelectListItem("Middle Eastern","me"),
                new SelectListItem("Asian","asian")
        };

        public EditModel(CVContext context, IWebHostEnvironment environment)
        {
            _context = context;
            SkillItems = _context.Skills.ToList();
            _environment = environment;
        }


        [BindProperty]
        public CVVM CVVM { get; set; }

        public string imagePath { get; set; }
        public int? id { get; set; }

        public List<Skill> checkedSkills { get; set; } = new List<Skill>();
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.CV == null)
            {
                return NotFound();
            }

            this.id = id;
            var CV = _context.CV.Include(s => s.HasSkills).ThenInclude(h => h.Skill).FirstOrDefault(m => m.CVID == id);

            CVVM = new CVVM()
            {
                FirstName = CV.FirstName,
                LastName = CV.LastName,
                Email = CV.Email,
                Birthday = CV.Birthday,
                Nationality = CV.Nationality,
                ConfirmEmail = CV.Email,
                Grade = CV.Grade
            };

            if (CV.Gender.Equals(Gender.Female)) CVVM.Gender = Gender.Female;
            else CVVM.Gender = Gender.Male;

            var allHasSkills = await _context.HasSkills.Where(s => s.CVId == CV.CVID).Include(s => s.Skill).ToListAsync();
            foreach (var hasSkill in allHasSkills)
            {
                checkedSkills.Add(hasSkill.Skill);
            }

            imagePath = CV.Image;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var CV = await _context.CV.Include(s=>s.HasSkills).ThenInclude(s=>s.Skill).FirstOrDefaultAsync(m => m.CVID == id);
            
            ChangeToCV(CVVM, CV);

            _context.Attach(CV).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CVExists(CV.CVID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./BrowseCV");
        }

        private bool CVExists(int id)
        {
            return (_context.CV?.Any(e => e.CVID == id)).GetValueOrDefault();
        }

        private async void ChangeToCV(CVVM CVVM, CV CV)
        {
            CV.FirstName = CVVM.FirstName;
            CV.LastName = CVVM.LastName;
            CV.Email = CVVM.Email;
            CV.Birthday = CVVM.Birthday;
            CV.Nationality = CVVM.Nationality;
            CV.Email = CVVM.Email;
            CV.Grade = CVVM.Grade;
            CV.Gender = CVVM.Gender.ToString();

            //SKills
            CV.HasSkills.Clear();
            foreach (int skillId in CVVM.Skills)
            {
                var hasSkill = new HasSkills { SkillId = skillId, CVId = CV.CVID };
                CV.HasSkills.Add(hasSkill);
            }

            //Grades
            int grade = 10 * CV.HasSkills.Count();
            if (CVVM.Gender.CompareTo(Gender.Female) == 0) grade += 10;
            else grade += 5;
            CV.Grade = grade;

            //Image

            //delete the old one
            var path = _environment.WebRootPath + CV.Image;
            System.IO.File.Delete(path);
            //upload the new one
            string name = CVVM.FirstName + "_" + CVVM.LastName + "_" + SendCVModel.i + ".jpg";
            var Newpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", name);
            using (FileStream stream = new FileStream(Newpath, FileMode.Create))
            {
                await CVVM.photo.CopyToAsync(stream);
                stream.Close();
                SendCVModel.i++;
            }

            CV.Image = "\\Images\\" + name;
        }
    }
}
