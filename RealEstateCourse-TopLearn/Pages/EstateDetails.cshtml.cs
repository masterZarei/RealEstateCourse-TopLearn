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
                .Include(c => c.Category)
                .Where(e => e.Id != Estate.Id)
                .Take(4)
                .ToList()
            };
            return Page();
        }
        public async Task<IActionResult> OnPostAddToFavourites(int Id)
        {
            if (User is null || !User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login?returnUrl=/EstateDetails?Id=" + Id);
            }

            if (Id <= 0)
            {
                return NotFound();
            }
            var estate = await _db.Estate.FirstOrDefaultAsync(e => e.Id == Id);
            if (estate is null)
            {
                return NotFound();
            }
            var user = await _db.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
            var checkIfRedundant = await _db.Favourite.FirstOrDefaultAsync(f => f.UserId == user.Id && f.EstateId == Id);
            if (checkIfRedundant is null)
            {
                await _db.AddAsync(new FavouriteModel()
                {
                    EstateId = Id,
                    UserId = user.Id
                });
                await _db.SaveChangesAsync();
            }
            return RedirectToPage("EstateDetails", new { Id });
        }
    }
}
