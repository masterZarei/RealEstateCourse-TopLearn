using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RealEstateCourse_TopLearn.Data;
using RealEstateCourse_TopLearn.Models;
using RealEstateCourse_TopLearn.Models.ViewModels.EstatesViewModels;

namespace RealEstateCourse_TopLearn.Pages
{
    public class EstateDetailsModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public EstateDetailsModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public EstateDetailsViewModel EstateViewModel { get; set; }
        public async Task<IActionResult> OnGet(int Id)
        {
            if (Id <= 0)
            {
                return NotFound();
            }
           var Estate = await _db.Estate
                .Include(c => c.Category)
                .FirstOrDefaultAsync(e => e.Id == Id);
            if (Estate is null)
            {
                return NotFound();
            }
            EstateViewModel = new()
            {
                Estate = Estate,
                SuggestedProducts = _db.Estate
                .Include(c=>c.Category)
                .Where(e=>e.Category.Title == Estate.Category.Title && e.Id != Estate.Id)
                .Take(4)
                .ToList()
            };
            return Page();
        }
    }
}
