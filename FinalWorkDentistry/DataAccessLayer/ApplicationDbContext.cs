using FinalWorkDentistry.Domains;
using FinalWorkDentistry.UsersRoles;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace FinalWorkDentistry.DataAccessLayer
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Service> Services { get; set; }
    

        public DbSet<CategoryService> CategoryService { get; set; }


        public DbSet<CategoryDoctor> CategoryDoctor { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Request> Requests { get; set; }

        public DbSet<Reviews> Reviews { get; set; }

        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {

        }
    }
}
