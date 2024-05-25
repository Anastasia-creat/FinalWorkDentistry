using FinalWorkDentistry.Domains;
using FinalWorkDentistry.Models;
using Mapster;


namespace FinalWorkDentistry.AutoMapping
{
    public static class ModelsMapping
    {
        public static void InitializeMappingService()
        {
            TypeAdapterConfig<Service, ServicesModel>
                  .NewConfig()
                  .Map(dest => dest.ServiceId, source => source.ServiceId)
                  .Map(dest => dest.Name, source => source.Name)
                  
                  .Map(dest => dest.CategoryService, source => source.ServiceCategory)
                  .Map(dest => dest.Price, source => source.Price);


            TypeAdapterConfig<Service, ServicesBriefModel>
                .NewConfig()
                .Map(dest => dest.ServiceId, source => source.ServiceId)
                .Map(dest => dest.Name, source => source.Name)
                .Map(dest => dest.CategoryService, source => source.ServiceCategory)
                .Map(dest => dest.Price, source => source.Price);


            TypeAdapterConfig<Service, EditServicesModel>
                .NewConfig()
                .Map(dest => dest.ServiceId, source => source.ServiceId)
                .Map(dest => dest.Name, source => source.Name)
                .Map(dest => dest.Price, source => source.Price)
                .Map(dest => dest.CategoryService, source => source.ServiceCategory);

                
        } 

        
        public static void InitializeMappingDoctor()
        {
            TypeAdapterConfig<Doctor, DoctorModel>
                .NewConfig()
                .Map(dest => dest.DoctorId, source => source.DoctorId)
                .Map(dest => dest.Name, source => source.Name)
                .Map(dest => dest.Description, source => source.Description)
                
                .Map(dest => dest.CategoryDoctor, source => source.DoctorCategory)
                .Map(dest => dest.ImageUrl, source => source.ImageUrl);

            TypeAdapterConfig<Doctor, DoctorBriefModel>
                .NewConfig()
                .Map(dest => dest.DoctorId, source => source.DoctorId)
                .Map(dest => dest.Name, source => source.Name)
               
                .Map(dest => dest.CategoryDoctor, source => source.DoctorCategory);


            TypeAdapterConfig<Doctor, EditDoctorsModel>
                .NewConfig()
                .Map(dest => dest.DoctorId, source => source.DoctorId)
                .Map(dest => dest.Name, source => source.Name)
                .Map(dest => dest.Description, source => source.Description)
                .Map(dest => dest.CategoryDoctor, source => source.DoctorCategory)
                .Map(dest => dest.ImageUrl, source => source.ImageUrl);

        }
    }
}
