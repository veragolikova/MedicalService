using System;

namespace MedicalService.Models.SubModels
{
    public class Address
    {
        public Guid Id { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string StreetName { get; set; }
        public string HouseNumber { get; set; }
        public string FlatNumber { get; set; }
    }
}
