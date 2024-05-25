using System.Collections;
using System.Collections.Generic;

namespace FinalWorkDentistry.Models
{
    public class ServicePageModel
    {
        /// <summary>
        /// Информация о странице с услугами
        /// </summary>
        public string CategoryName { get; set; }
        public IEnumerable<ServicesBriefModel> ServicesForPage { get; set; }

       // public IEnumerable<DoctorBriefModel> DoctorForPage { get; set; }

        public int PagesQuantity { get; set; }

        public int ActivePage { get; set; }
    }
}
