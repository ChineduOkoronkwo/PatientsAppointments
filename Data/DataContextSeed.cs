using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TestWebApi.Data.Entities;
using TestWebApi.Services;

namespace TestWebApi.Data
{
    public class DataContextSeed
    {
        public static Task Seed(PatientsService patientsService, AppointmentsService appointmentService, 
        ILoggerFactory loggerFactory) 
        {
            var logger = loggerFactory.CreateLogger<DataContextSeed>();
            try
            {
                // seed patients
                for (int i = 1; i <= 5; i++) 
                {
                    var patient = new Patient() {
                        FirstName = "Patient First" + i, 
                        LastName = "Patient Last" + i,
                        DateOfBirth = DateTime.Now.AddYears(i * -10)
                    };
                    patientsService.Create(patient);
                }

                for(int i = 1; i <= 5; i++) 
                {
                    var appointment = new Appointment()
                    {
                        PatientId = i,
                        AppointmentTime = DateTimeOffset.Now.AddDays(i * 5).AddHours(i * 2),
                        Notes = "Give patient instructions on how to use prescribed medications."
                    };
                    appointmentService.Create(appointment);
                }
            }
            catch (Exception ex) 
            {
                //var logger = loggerFactory.CreateLogger<DataContextSeed>();
                logger.LogError(ex, "An error occured during data seeding");
            }  
            
            return Task.FromResult(0);          
        }
    }
}