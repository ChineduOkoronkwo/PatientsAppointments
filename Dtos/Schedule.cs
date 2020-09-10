using TestWebApi.Data.Entities;

namespace TestWebApi.Dtos
{
    public class Schedule
    {
        public Patient Patient { get; set; }
        public Appointment Appointment { get; set; }
    }
}