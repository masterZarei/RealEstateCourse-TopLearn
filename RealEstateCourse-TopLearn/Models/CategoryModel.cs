using System.ComponentModel.DataAnnotations;

namespace RealEstateCourse_TopLearn.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} نمی تواند خالی وارد شود")]
        [Display(Name = "عنوان")]
        [MaxLength(50)]
        public string Title { get; set; }

        [Display(Name = "توضیحات")]
        [MaxLength(1000)]
        public string? Description { get; set; }

        #region Relation
        public ICollection<EstateModel>? Estates { get; set; }
        #endregion
    }
}
