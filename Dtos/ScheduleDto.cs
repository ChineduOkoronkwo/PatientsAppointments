using System;

namespace TestWebApi.Dtos
{
    public class ScheduleDto
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int AppointmentId { get; set; }
        public DateTime AppointmentTime { get; set; }
        public string Notes { get; set; }
    }
}