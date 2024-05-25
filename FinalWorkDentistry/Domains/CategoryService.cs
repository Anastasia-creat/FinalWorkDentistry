using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FinalWorkDentistry.Domains
{
    [Table("CategoryService")]
    public class CategoryService
    {

        [Key]
        public long CategoryServiceId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Service> ServiceOfCategory { get; set; }
    }
}
