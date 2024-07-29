using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Project_2023.Data;
using Project_2023.ViewModels;

namespace Project_2023.Pages.CVs
{
    public class DetailsModel : PageModel
    {
        private readonly CVContext _context;

        public DetailsModel(CVContext context)
        {
            _context = context;
        }

        public Models.CV CV { get; set; } = default!;
        public CVVM CVVM { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.CV == null)
            {
                return NotFound();
            }

            var cv = await _context.CV.Include(s => s.HasSkills).ThenInclude(s => s.Skill).AsNoTracking().FirstOrDefaultAsync(m => m.CVID == id);
            if (cv == null)
            {
                return NotFound();
            }
            else
            {
                CV = cv;
                CVVM = new CVVM();
                CVVM.FirstName = cv.FirstName;
                CVVM.LastName = cv.LastName;
                CVVM.Email = cv.Email;
                CVVM.Birthday = cv.Birthday;
                CVVM.Nationality = cv.Nationality;
                CVVM.ConfirmEmail = cv.Email;
                CVVM.Grade = cv.Grade;

                if (cv.Gender.Equals(Gender.Female)) CVVM.Gender = Gender.Female;
                else CVVM.Gender = Gender.Male;

            }
            return Page();
        }
    }
}
