using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RealEstateCourse_TopLearn.Data;
using RealEstateCourse_TopLearn.Models;

namespace RealEstateCourse_TopLearn.Pages.Panel.Admin.Estates
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public DeleteModel(ApplicationDbContext db)
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
                .FirstOrDefaultAsync(e => e.Id == Id);
            if (ViewModel is null)
            {
                return NotFound();
            }

            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            if (ViewModel.Id <= 0)
            {
                return NotFound();
            }
            if (ViewModel.Image is not null)
            {
                string saveDir = "wwwroot/image/Estates";
                string deletePath = Path.Combine(Directory.GetCurrentDirectory(), saveDir, ViewModel.Image);
                if (System.IO.File.Exists(deletePath))
                    System.IO.File.Delete(deletePath);
            }
            _db.Remove(ViewModel);
            await _db.SaveChangesAsync();


            return RedirectToPage("Index");
        }
    }
}
