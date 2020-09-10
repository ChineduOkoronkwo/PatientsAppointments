using System;

namespace TestWebApi.Dtos
{
    public class SearchParamDto
    {
        public int PatientId { get; set; }
        public int AppointmentId { get; set; }
        public DateTime DateFrom { get; set; }
    }
}