using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FinalWorkDentistry.Domains
{
    [Table("Reviews")]
    public class Reviews
    {
        [Key]
        public long ReviewId { get; set; }
        public string NameReview { get; set; }
        public string Text { get; set; }
        //public string Date { get; set; }


        //public virtual CategoryDoctor ReviewsCategory { get; set; }

    }
}
