using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FinalWorkDentistry.Domains
{
    [Table("CategoryService")]
    public class CategoryService
    {

        [Key]
        [Display(Name = "Номер")]
        public long CategoryServiceId { get; set; }
        [Display(Name = "Название")]
        public string Name { get; set; }

        public virtual ICollection<Service> ServiceOfCategory { get; set; }
    }
}
