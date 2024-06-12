using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalWorkDentistry.Domains
{
    [Table("Services")]
    public class Service
    {
        [Key]
        public long ServiceId { get; set; }

        public string Name { get; set; }

       


        public virtual CategoryService ServiceCategory { get; set; }

     

        public int Price { get; set; }
    }
}
