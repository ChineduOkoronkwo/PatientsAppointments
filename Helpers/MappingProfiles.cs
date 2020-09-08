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
                .ForMember(d => d.Appointments, s => new List<Appointment>());
                //.ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
        }
    }
}