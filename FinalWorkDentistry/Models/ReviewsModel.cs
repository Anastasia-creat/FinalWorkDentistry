using FinalWorkDentistry.Domains;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FinalWorkDentistry.Models
{
    public class ReviewsModel
    {

        [HiddenInput(DisplayValue = false)]
        public long ReviewId { get; set; }

        [Display(Name = "Клиент")]
        public string NameReview { get; set; }


        [Display(Name = "Отзыв")]
        public string Text { get; set; }

        public IEnumerable<ReviewBriefModel> ReviewForPage { get; set; }

        public ReviewsModel()
        {
            
        }
        public ReviewsModel(Reviews entity)
        {
            NameReview = entity.NameReview;
            ReviewId = entity.ReviewId;
            Text = entity.Text;
        }
    }
}
