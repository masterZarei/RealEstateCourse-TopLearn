using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RealEstateCourse_TopLearn.Data;
using RealEstateCourse_TopLearn.Models;

namespace RealEstateCourse_TopLearn.Pages.Panel.Favourites
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public List<FavouriteModel> Favourites { get; set; }
        public async Task<IActionResult> OnGet()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login?returnUrl=/Panel/Favourites");

            }
            var user = await _db.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

            Favourites = await _db.Favourite
                .Include(e => e.Estate)
                .Where(f => f.UserId == user.Id).ToListAsync();

            return Page();

        }
    }
}
