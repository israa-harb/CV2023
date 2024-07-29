using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Project_2023.Pages.CVs
{
    public class BrowseCVModel : PageModel
    {
        private readonly Project_2023.Data.CVContext _context;

        public BrowseCVModel(Project_2023.Data.CVContext context)
        {
            _context = context;
        }

        public IList<Models.CV> CV { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.CV != null)
            {
                CV = await _context.CV.ToListAsync();
            }
        }
    }
}
