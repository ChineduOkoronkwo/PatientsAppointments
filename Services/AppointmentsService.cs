using System.Collections.Generic;
using System.Threading.Tasks;
using TestWebApi.Data;
using TestWebApi.Data.Entities;

namespace TestWebApi.Services
{
    public class AppointmentsService : IPatientsService<Appointment>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AppointmentsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Appointment> Create(Appointment entity)
        {
            // get patient
            var patient = _unitOfWork.Repository<Patient>().GetById(entity.PatientId).Result;

            Appointment result = null;
            if (patient != null)
            {
                // create appointment
                result = _unitOfWork.Repository<Appointment>().Create(entity).Result;

                // add appointment to patient's list of appointments
                if (patient.Appointments == null)
                {
                    patient.Appointments = new List<Appointment>();
                }
                patient.Appointments.Add(result);

                // update patient
                patient = _unitOfWork.Repository<Patient>().Update(patient).Result;
                if (patient == null)
                {
                    // Rollback appointment created
                    _unitOfWork.Repository<Appointment>().Delete(result);
                }
            }

            return Task.FromResult(result);
        }

        public Task<bool> Delete(Appointment entity)
        {
            // get the patient that has the appointment
            var patient = _unitOfWork.Repository<Patient>().GetById(entity.PatientId).Result;
            
            if (patient != null && patient.Appointments != null) 
            {
                var appIndex = patient.Appointments.FindIndex(p => p.Id == entity.Id);
                if (appIndex >= 0) 
                {
                    // remove appointment from patient's appointments list
                    patient.Appointments.RemoveAt(appIndex);

                    // delete appointment
                    var result = _unitOfWork.Repository<Appointment>().Delete(entity);

                    // update patient
                    patient = _unitOfWork.Repository<Patient>().Update(patient).Result;

                    // return result
                    return result;
                }
            }  

            return Task.FromResult(false);         
            
        }

        public Task<Appointment> GetById(int id)
        {
            var result = _unitOfWork.Repository<Appointment>().GetById(id).Result;
            return Task.FromResult(result);
        }

        public Task<IList<Appointment>> ListAll()
        {
            var result = _unitOfWork.Repository<Appointment>().ListAll().Result;
            return Task.FromResult(result);
        }

        public Task<Appointment> Update(Appointment entity)
        {
            // get patient
            var patient = _unitOfWork.Repository<Patient>().GetById(entity.PatientId).Result;
            Appointment result = null;
            if (patient != null && patient.Appointments != null) 
            {
                var appIndex = patient.Appointments.FindIndex(p => p.Id == entity.Id);
                if (appIndex >= 0) 
                {
                    patient.Appointments[appIndex] = entity;

                    // update appointment
                    result = _unitOfWork.Repository<Appointment>().Update(entity).Result;
                    patient = _unitOfWork.Repository<Patient>().Update(patient).Result;
                }
                
            }

            return Task.FromResult(result);  
        }
    }
}