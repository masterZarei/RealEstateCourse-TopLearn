using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RealEstateCourse_TopLearn.Data;
using RealEstateCourse_TopLearn.Models;

namespace RealEstateCourse_TopLearn.Pages.Panel.Admin.Categories
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public CategoryModel CategoryModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CategoryModel = await _context.Category.FirstOrDefaultAsync(m => m.Id == id);

            if (CategoryModel == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
