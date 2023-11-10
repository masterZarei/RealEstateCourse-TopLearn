using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RealEstateCourse_TopLearn.Data;
using RealEstateCourse_TopLearn.Models.ViewModels.EstatesViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using RealEstateCourse_TopLearn.Models;
using Microsoft.IdentityModel.Tokens;

namespace RealEstateCourse_TopLearn.Pages.Admin.Estates
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public CreateEstateViewModel? EstateViewModel { get; set; }
        public void OnGet()
        {
            InitCategories();
        }
        private void InitCategories()
        {
            EstateViewModel = new()
            {
                CategoryOptions = new SelectList(_db.Category, nameof(CategoryModel.Id), nameof(CategoryModel.Title))
            };
        }
        public async Task<IActionResult> OnPost()
        {
            #region Validation
            if (!ModelState.IsValid || string.IsNullOrEmpty(EstateViewModel.SelectedCategory))
            {
                InitCategories();
                return Page();
            }
            bool check = int.TryParse(EstateViewModel.SelectedCategory, out int categoryId);
            if (check is false)
            {
                ModelState.AddModelError(string.Empty, "دسته بندی انتخاب شده نامعتبر است");
                InitCategories();
                return Page();
            }
            var category = await _db.Category.FindAsync(categoryId);
            if (category is null)
            {
                ModelState.AddModelError(string.Empty, "دسته بندی انتخاب شده نامعتبر است");
                InitCategories();
                return Page();
            }
            #endregion
            #region Upload Image

            if (EstateViewModel.ImgUp is not null)
            {
                string saveDir = "wwwroot/image/Estates";
                if (!Directory.Exists(saveDir))
                    Directory.CreateDirectory(saveDir);

                EstateViewModel.Estate.Image = Guid.NewGuid().ToString() + Path.GetExtension(EstateViewModel.ImgUp.FileName);
                string savepath = Path.Combine(Directory.GetCurrentDirectory(), saveDir, EstateViewModel.Estate.Image);
                using var filestream = new FileStream(savepath, FileMode.Create);
                EstateViewModel.ImgUp.CopyTo(filestream);
            }
            #endregion
            EstateViewModel.Estate.CategoryId = categoryId;
            await _db.Estate.AddAsync(EstateViewModel.Estate);
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
