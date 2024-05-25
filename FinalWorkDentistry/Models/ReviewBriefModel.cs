using FinalWorkDentistry.Domains;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FinalWorkDentistry.Models;

public class ReviewBriefModel
{

    [HiddenInput(DisplayValue = false)]
    public long ReviewId { get; set; }



    [Display(Name = "Клиент")]
    public string NameReview { get; set; }

   
    public string Text { get; set; }



    public ReviewBriefModel()
    {
        
    }

    public ReviewBriefModel(Reviews entity)
    {
        NameReview = entity.NameReview;
        Text = entity.Text;
        ReviewId = entity.ReviewId;
    }

   
   
}
