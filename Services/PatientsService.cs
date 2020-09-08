using System.Collections.Generic;
using System.Threading.Tasks;
using TestWebApi.Data;
using TestWebApi.Data.Entities;

namespace TestWebApi.Services
{
    public class PatientsService : IPatientsService<Patient>
    {
        private readonly IUnitOfWork _unitOfWork;
        public PatientsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Patient> Create(Patient entity)
        {
           return _unitOfWork.Repository<Patient>().Create(entity);
        }

        public Task<bool> Delete(Patient entity)
        {
            // check that patient exist
            var exitingPatient = GetById(entity.Id).Result;

            if (exitingPatient != null) 
            {
                // delete all appointmnts
                if (exitingPatient.Appointments != null) {
                    foreach(var appointmnt in exitingPatient.Appointments)
                    {
                        var deletedApp = _unitOfWork.Repository<Appointment>().Delete(appointmnt).Result;
                    }
                }


                // delete patient
                var deletePat = _unitOfWork.Repository<Patient>().Delete(entity);

                // return true
                return Task.FromResult(true);
            }
            
            // return false
            return Task.FromResult(false);
        }

        public Task<Patient> GetById(int id)
        {
            return _unitOfWork.Repository<Patient>().GetById(id);
        }

        public Task<IList<Patient>> ListAll()
        {
            return _unitOfWork.Repository<Patient>().ListAll();
        }

        public Task<Patient> Update(Patient entity)
        {
            // check that patient exist
            var existingPatient = GetById(entity.Id).Result;
            if (existingPatient != null)
            {
                // assign existing appointments
                entity.Appointments = existingPatient.Appointments;

                // update patient
                _unitOfWork.Repository<Patient>().Update(entity);

                // return patient
                return GetById(entity.Id);
            }

            return Task.FromResult(existingPatient);
        }
    }
}