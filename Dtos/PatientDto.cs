using System;
using System.ComponentModel.DataAnnotations;

namespace TestWebApi.Dtos
{
    public class PatientDto
    {
        public int Id { get; set; }
        
        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }
    }
}