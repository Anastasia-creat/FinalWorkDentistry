using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace FinalWorkDentistry.Domains
{
    [Table("CategoryDoctor")]
    public class CategoryDoctor
    {
        [Key]
        public long CategoryDoctorId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Doctor> DoctorOfCategory { get; set; }

        public virtual ICollection<Reviews> ReviewsOfCategory { get; set; }
    }
}
