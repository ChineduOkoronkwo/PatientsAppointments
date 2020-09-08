using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestWebApi.Data.Entities
{
    public class Patient : BaseEntity
    {        
        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        public List<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}