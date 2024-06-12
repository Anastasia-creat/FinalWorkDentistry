using FinalWorkDentistry.Abstract;
using FinalWorkDentistry.Domains;
using FinalWorkDentistry.Models;
using FinalWorkDentistry.UsersRoles;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Numerics;
using System.Security.Claims;

namespace FinalWorkDentistry.DataAccessLayer
{
    public static class DataSeeder
    {
        public static void SeedServices(IServiceProvider provider)
        {

            var serviceRepository = provider
               .GetRequiredService<IRepository<Service>>();

            var doctorRepository = provider
                .GetRequiredService<IRepository<Doctor>>();

            var reviewRepository = provider
              .GetRequiredService<IRepository<Reviews>>();
            var categoryUslugiRepository = provider
                .GetRequiredService<IRepository<CategoryService>>();

            var categoryMedicRepository = provider
               .GetRequiredService<IRepository<CategoryDoctor>>();


            if (serviceRepository.GetList().Count() > 0)
                return;

            var filesService = new DirectoryInfo("ClinicJson")
                .GetFiles("*.json");

            foreach (var fi in filesService)
            {
                string categoryName = Path.GetFileName(fi.FullName);
                categoryName = Path.GetFileNameWithoutExtension(categoryName);

                var categoryUslugi = new CategoryService { Name = categoryName };
                categoryUslugiRepository.Create(categoryUslugi);

                string jsonText = File.ReadAllText(fi.FullName);

                var services = JsonConvert
                    .DeserializeObject<List<ServicesModel>>(jsonText);

                foreach (var model in services)
                {
                    var product = new Service
                    {
                        Name = model.Name,
                        Price = model.Price,
                        ServiceCategory = categoryUslugi
                      
                    };

                    serviceRepository.Create(product);
                }
            }


            if (doctorRepository.GetList().Count() > 0)
                return;

            var filesDoctor = new DirectoryInfo("DoctorsJson")
                .GetFiles("*.json");

            foreach (var fi in filesDoctor)
            {
                string categoryNameDoctor = Path.GetFileName(fi.FullName);
                categoryNameDoctor = Path.GetFileNameWithoutExtension(categoryNameDoctor);

                var categoryMedic = new CategoryDoctor { Name = categoryNameDoctor };
                categoryMedicRepository.Create(categoryMedic);


                string jsonText = File.ReadAllText(fi.FullName);
                var doctors = JsonConvert
                    .DeserializeObject<List<DoctorModel>>(jsonText);

                foreach (var model in doctors)
                {
                    var doctor = new Doctor
                    {

                        Description = model.Description,
                        Job = model.Job,
                        Name = model.Name,
                        ImageUrl = model.ImageUrl,
                        DoctorCategory = categoryMedic
                    };

                    doctorRepository.Create(doctor);
                }
            }


            if (reviewRepository.GetList().Count() > 0) return;
            var filesReviews = new DirectoryInfo("ReviewsJson").GetFiles("*json");
            foreach (var fi in filesReviews)
            {
               


                string jsonText = File.ReadAllText(fi.FullName);
                var reviews = JsonConvert
                    .DeserializeObject<List<ReviewsModel>>(jsonText);

                foreach (var model in reviews)
                {
                    var review = new Reviews
                    {


                        NameReview = model.NameReview,
                      // ReviewId = model.ReviewId,
                       Text = model.Text,


                    };

                    reviewRepository.Create(review);
                }
            }
           
        }

        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            if (userManager.Users.Count() > 0) return;

            var user1 = new ApplicationUser
            {
                UserName = "admin@ya.ru",
                LastName = "Админов",
                FirstName = "Админ",
                Email = "admin@ya.ru",
                EmailConfirmed = true
            };
            IdentityResult userResult = userManager.CreateAsync(user1, "1Qwerty!").Result;
            if (userResult.Succeeded)
            {
                userResult = userManager.AddToRoleAsync(user1, AppRoles.Administrator).Result;
            }


            var user2 = new ApplicationUser
            {
                UserName = "content@ya.ru",
                LastName = "Манагер контента",
                FirstName = "Коля",
                Email = "content@ya.ru",
                EmailConfirmed = true
            };
            userResult = userManager.CreateAsync(user2, "1Qwerty!").Result;
            if (userResult.Succeeded)
            {
                userResult = userManager.AddToRoleAsync(user2, AppRoles.ContentManager).Result;
            }
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = new string[]
            {
                AppRoles.Administrator,
                AppRoles.ContentManager,
                AppRoles.Order,
                AppRoles.Guest
            };
            if (roleManager.Roles.Count() > 0) return;

            foreach (var roleName in roleNames)
            {
                var role = new IdentityRole { Name = roleName };
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }

        public static async Task AddPermissionClaim(
           this RoleManager<IdentityRole> roleManager,
           IdentityRole role,
           string module)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            var allPermissions = Permissions.GeneratePermissionsForModule(module);
            foreach (var permission in allPermissions)
            {
                if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
                {
                    await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
                }
            }
        }

    }
}

