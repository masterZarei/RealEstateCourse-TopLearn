using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateCourse_TopLearn.Models
{
    public class FavouriteModel
    {
        public int Id { get; set; }

        public int? EstateId { get; set; }
        public string? UserId { get; set; }

        #region Relation
        [ForeignKey(nameof(EstateId))]
        public EstateModel? Estate { get; set; }

        [ForeignKey(nameof(UserId))]
        public UserModel? User { get; set; }
        #endregion
    }
}
