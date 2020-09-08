using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestWebApi.Data;
using TestWebApi.Data.Entities;
using TestWebApi.Services;

namespace TestWebApi.Controllers
{
    public class AppointmentsController : BaseAPIController
    {
        private readonly AppointmentsService _appointmentService;

        public AppointmentsController(AppointmentsService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet]
        public ActionResult<IList<Appointment>> GetAppointments()
        {
            var result = _appointmentService.ListAll().Result;
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult<Appointment> GetAppointmentById(int id)
        {
            var result = _appointmentService.GetById(id).Result;
            return Ok(result);
        }

        [HttpPost]
        public ActionResult<Appointment> CreateAppointment(Appointment appointment)
        {
            var result = _appointmentService.Create(appointment).Result;
            return result;
        }

        [HttpPut]
        public ActionResult<Appointment> UpdateAppointment(Appointment appointment)
        {
            var result = _appointmentService.Update(appointment).Result;
            return Ok(result);
        }

        [HttpDelete]
        public ActionResult<bool> DeleteAppointment(Appointment appointment)
        {
            var result = _appointmentService.Delete(appointment).Result;
            return Ok(result);
        }
    }
}