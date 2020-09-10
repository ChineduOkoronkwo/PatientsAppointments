using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestWebApi.Data;
using TestWebApi.Data.Entities;
using TestWebApi.Dtos;

namespace TestWebApi.Controllers
{
    public class SearchController : BaseAPIController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SearchController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;

        }

        [HttpGet]
        public ActionResult<IList<ScheduleDto>> GetSchedules([FromQuery] SearchParamDto searchParamDto)
        {
            var appointments = _unitOfWork.Repository<Appointment>().ListAll().Result;
            var schedules = new List<Schedule>();
            foreach(var app in appointments)
            {
                if ((searchParamDto.PatientId == 0 || app.PatientId == searchParamDto.PatientId) 
                    && (searchParamDto.DateFrom == null || app.AppointmentTime >= searchParamDto.DateFrom)
                    && (searchParamDto.AppointmentId == 0 || app.Id == searchParamDto.AppointmentId))
                {
                    var schedule = new Schedule();
                    schedule.Appointment = app;
                    schedule.Patient = _unitOfWork.Repository<Patient>().GetById(app.PatientId).Result;
                    schedules.Add(schedule);
                }
            }

            var result = _mapper.Map<List<Schedule>, List<ScheduleDto>>(schedules);

            return Ok(result);
                      
        }

    }
}