using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestWebApi.Data;
using TestWebApi.Data.Entities;
using TestWebApi.Dtos;

namespace TestWebApi.Controllers
{
    public class PatientsController : BaseAPIController
    {
        private readonly IGenericRepository<Patient> _genericRepo;
        private readonly IMapper _mapper;
        public PatientsController(IGenericRepository<Patient> genericRepo, IMapper mapper)
        {
            _mapper = mapper;
            _genericRepo = genericRepo;
        }

        [HttpGet]
        public ActionResult<IList<PatientDto>> GetPatients()
        {
            var list = _genericRepo.ListAll().Result;
            var result = _mapper.Map<IList<Patient>, IList<PatientDto>>(list);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult<PatientDto> GetPatientById(int id)
        {
            var patient = _genericRepo.GetById(id).Result;
            var result = _mapper.Map<Patient, PatientDto>(patient);
            return Ok(result);
        }

        [HttpPost]
        public ActionResult<Patient> CreatePatient(PatientDto patientDto)
        {
            var patient = _mapper.Map<PatientDto, Patient>(patientDto);
            var result = _genericRepo.Create(patient).Result;
            return Ok(result);
        }

        [HttpPut]
        public ActionResult<Patient> UpdatePatient(PatientDto patientDto)
        {
            var patient = _mapper.Map<PatientDto, Patient>(patientDto);

            var existingPatient = _genericRepo.GetById(patient.Id).Result;
            patient.Appointments = existingPatient.Appointments;
            
            var result = _genericRepo.Update(patient).Result;
            return Ok(result);
        }

        [HttpDelete]
        public ActionResult<bool> DeletePatient(Patient Patient)
        {
            var result = _genericRepo.Delete(Patient).Result;
            return Ok(result);
        }
    }
}