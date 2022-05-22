using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using MedicalService.Models.SubModels;

namespace MedicalService.Models
{
    public class Event
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TimesADame { get; set; }
        public Guid? PatientId { get; set; }
        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
        //public Guid? EventTypeId { get; set; }
        //[ForeignKey("EventTypeId")]
        //public EventType EventType { get; set; }
        //public bool AllDay { get; set; }
        //public Guid? EventStatusId { get; set; }
        //[ForeignKey("EventStatusId")]
        //public EventStatus EventStatus { get; set; }
        //public string ThemeColor { get; set; }

    }
}
