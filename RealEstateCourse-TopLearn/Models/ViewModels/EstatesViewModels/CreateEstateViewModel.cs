using Microsoft.AspNetCore.Mvc.Rendering;

namespace RealEstateCourse_TopLearn.Models.ViewModels.EstatesViewModels
{
    public class CreateEstateViewModel
    {
        public EstateModel? Estate { get; set; }

        #region Upload
        public IFormFile? ImgUp { get; set; }
        #endregion
        #region Category
        public SelectList? CategoryOptions { get; set; }
        public string? SelectedCategory { get; set; }
        #endregion

    }
}
