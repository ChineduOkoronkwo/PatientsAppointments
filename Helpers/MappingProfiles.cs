using System.Collections.Generic;
using AutoMapper;
using TestWebApi.Data.Entities;
using TestWebApi.Dtos;

namespace TestWebApi.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<PatientDto, Patient>()
                .ForMember(d => d.Appointments, s => new List<Appointment>())
                .ReverseMap();
                
            CreateMap<Schedule, ScheduleDto>()
                .ForMember(d => d.AppointmentTime, o => o.MapFrom(s => s.Appointment.AppointmentTime))
                .ForMember(d => d.AppointmentId, o => o.MapFrom(s => s.Appointment.Id))
                .ForMember(d => d.Notes, o => o.MapFrom(s => s.Appointment.Notes))
                .ForMember(d => d.PatientId, o => o.MapFrom(s => s.Patient.Id))
                .ForMember(d => d.LastName, o => o.MapFrom(s => s.Patient.LastName))
                .ForMember(d => d.FirstName, o => o.MapFrom(s => s.Patient.FirstName))
                .ForMember(d => d.DateOfBirth, o => o.MapFrom(s => s.Patient.DateOfBirth));
        }
    }
}