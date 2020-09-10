using System;
using System.ComponentModel.DataAnnotations;

namespace TestWebApi.Data.Entities
{
    public class Appointment : BaseEntity
    {
        [Required]
        public int PatientId { get; set; }

        [Required]
        public DateTime AppointmentTime { get; set; }

        [MaxLength(200)]
        public string Notes { get; set; }
    }
}