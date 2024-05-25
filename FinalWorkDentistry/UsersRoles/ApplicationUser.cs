using Microsoft.AspNetCore.Identity;

namespace FinalWorkDentistry.UsersRoles
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
       // public string PhoneNumber { get; set; }
       // public string Address { get; set; }
     
    }
}
