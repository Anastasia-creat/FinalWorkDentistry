using FinalWorkDentistry.Domains;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FinalWorkDentistry.Models;

public class RequestModel
{
    [Display(Name = "Номер заявки")]
  
    public long RequestId { get; set; }

    [Display(Name = "Клиент")]
    public string Name { get; set; }


    [Display(Name = "Телефон")]
    public string Phone { get; set; }

    [Display(Name = "Возможные симптомы")]
    public string Symptoms { get; set; }

    [Display(Name = "Удобное время для связи")]
    public string Time { get; set; }



    //public IEnumerable<ReviewBriefModel> ReviewForPage { get; set; }


    public RequestModel()
    {
        
    }

    public RequestModel(Request entity)
    {
        Name = entity.Name;
        Phone = entity.Phone;
        Symptoms = entity.Symptoms;
        RequestId = entity.RequestId;
    }
   
}


