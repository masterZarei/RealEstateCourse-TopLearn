using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RealEstateCourse_TopLearn.Data;
using RealEstateCourse_TopLearn.Models;
using RealEstateCourse_TopLearn.Models.ViewModels.EstatesViewModels;

namespace RealEstateCourse_TopLearn.Pages.Admin.Estates
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public DetailsModel(ApplicationDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public EstateModel ViewModel { get; set; }
        public async Task<IActionResult> OnGet(int Id)
        {
            if (Id <= 0)
            {
                return NotFound();
            }
            ViewModel = await _db.Estate
                .Include(c=>c.Category)
                .FirstOrDefaultAsync(e => e.Id == Id);
            if (ViewModel is null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
