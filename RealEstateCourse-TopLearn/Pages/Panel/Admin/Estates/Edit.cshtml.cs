using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RealEstateCourse_TopLearn.Data;
using RealEstateCourse_TopLearn.Models;
using RealEstateCourse_TopLearn.Models.ViewModels.EstatesViewModels;

namespace RealEstateCourse_TopLearn.Pages.Panel.Admin.Estates
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public EstateViewModel ViewModel { get; set; }
        public async Task<IActionResult> OnGet(int Id)
        {
            if (Id <= 0)
            {
                return NotFound();
            }
            var estate = await _db.Estate.FindAsync(Id);
            if (estate is null)
            {
                return NotFound();
            }
            ViewModel = new()
            {
                Estate = estate
            };
            InitCategories();

            return Page();
        }
        private void InitCategories()
        {
            ViewModel.CategoryOptions = new SelectList(_db.Category, nameof(CategoryModel.Id), nameof(CategoryModel.Title),ViewModel?.Estate?.CategoryId);
        }
        public async Task<IActionResult> OnPost()
        {
            #region Validation
            if (!ModelState.IsValid || string.IsNullOrEmpty(ViewModel.SelectedCategory))
            {
                InitCategories();
                return Page();
            }
            bool check = int.TryParse(ViewModel.SelectedCategory, out int categoryId);
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

            if (ViewModel.ImgUp is not null)
            {
                string saveDir = "wwwroot/image/Estates";

                if (ViewModel.Estate.Image is not null)
                {
                    string deletePath = Path.Combine(Directory.GetCurrentDirectory(), saveDir, ViewModel.Estate.Image);
                    if (System.IO.File.Exists(deletePath))
                        System.IO.File.Delete(deletePath);
                }

                if (!Directory.Exists(saveDir))
                    Directory.CreateDirectory(saveDir);

                ViewModel.Estate.Image = Guid.NewGuid().ToString() + Path.GetExtension(ViewModel.ImgUp.FileName);
                string savepath = Path.Combine(Directory.GetCurrentDirectory(), saveDir, ViewModel.Estate.Image);
                using var filestream = new FileStream(savepath, FileMode.Create);
                ViewModel.ImgUp.CopyTo(filestream);
            }
            #endregion
            ViewModel.Estate.CategoryId = categoryId;
            _db.Estate.Update(ViewModel.Estate);
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
