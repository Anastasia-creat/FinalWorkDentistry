using System.Collections.Generic;

namespace FinalWorkDentistry.UsersRoles
{
    public class Permissions
    {
        public static List<string> GeneratePermissionsForModule(string module)
        {
            return new List<string>()
            {
                $"Permissions.{module}.Create",
                $"Permissions.{module}.View",
                $"Permissions.{module}.Edit",
                $"Permissions.{module}.Delete",
                $"Permissions.{module}.ViewService",
                $"Permissions.{module}.CreateService",
                $"Permissions.{module}.EditService",
                $"Permissions.{module}.DeleteService",
            };
        }
        public static class Doctor
        {
            public const string View = "Просмотр врачей";
            public const string Create = "Добавление врача";
            public const string Edit = "Редактирование врачей";
            public const string Delete = "Удаление врача";


            public const string ViewService = "Просмотр услуг";
            public const string CreateService = "Добавление услуги";
            public const string EditService = "Редактирование услуги";
            public const string DeleteService = "Удаление услуги";
            //  public const string EditPrice = "Permissions.Doctors.EditPrice";
        }
    }
}
