using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestWebApi.Data;
using TestWebApi.Data.Entities;
using TestWebApi.Dtos;
using TestWebApi.Services;

namespace TestWebApi.Controllers
{
    public class PatientsController : BaseAPIController
    {
        private readonly PatientsService _patientsService;

        private readonly IMapper _mapper;
        public PatientsController(PatientsService patientsService, IMapper mapper)
        {
            _patientsService = patientsService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IList<PatientDto>> GetPatients()
        {
            var list = _patientsService.ListAll().Result;
            var result = _mapper.Map<IList<Patient>, IList<PatientDto>>(list);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult<PatientDto> GetPatientById(int id)
        {
            var patient = _patientsService.GetById(id).Result;
            var result = _mapper.Map<Patient, PatientDto>(patient);
            return Ok(result);
        }

        [HttpPost]
        public ActionResult<Patient> CreatePatient(PatientDto patientDto)
        {
            var patient = _mapper.Map<PatientDto, Patient>(patientDto);
            var result = _patientsService.Create(patient).Result;
            return Ok(result);
        }

        [HttpPut]
        public ActionResult<Patient> UpdatePatient(PatientDto patientDto)
        {
            var patient = _mapper.Map<PatientDto, Patient>(patientDto);

            /*var existingPatient = _patientsService.GetById(patient.Id).Result;
            patient.Appointments = existingPatient.Appointments;*/

            var result = _patientsService.Update(patient).Result;
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public ActionResult<bool> DeletePatient(int id)
        {
            var patient = _patientsService.GetById(id).Result;
            if (patient != null)
            {
                var result = _patientsService.Delete(patient).Result;
                if (result) {
                    return Ok(result);
                } 
                return BadRequest("Unable to delete record");               
            }
            return BadRequest("No matching record was found");
        }
    }
}