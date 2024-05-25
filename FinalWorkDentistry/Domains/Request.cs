using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalWorkDentistry.Domains;

[Table("Requests")]
public class Request
{

    [Key]
    public long RequestId { get; set; }
    public string Name { get; set; }

    public string Phone { get; set; }

    public string Symptoms { get; set; }
    public string Time { get; set; }

}
