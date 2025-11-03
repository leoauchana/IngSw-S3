using IngSw_Domain.Entities;
using IngSw_Domain.Interfaces;
using IngSw_Domain.ValueObjects;
using IngSw_Infraestructure.Data.DAOs;

namespace IngSw_Infraestructure.Data.Repository;

public class PatientsRepository : IPatientRepository
{
    private readonly PatientDao _patientDao;
    public PatientsRepository(PatientDao patientDao)
    {
        _patientDao = patientDao;
    }
    public Task<List<Patient>?> GetAll()
    {
        throw new NotImplementedException();
    }
    public async Task<List<Patient>?> GetByCuil(string cuilPatient)
    {
        var patientsFound = await _patientDao.GetByCuil(cuilPatient);
        return patientsFound?
                    .Select(p => MapEntity(p))
                    .ToList();
    }
    public async Task AddPatient(Patient newPatient) => await _patientDao.AddPatient(newPatient);
    public Task<Patient?> GetById(int id)
    {
        throw new NotImplementedException();
    }
    private Patient MapEntity(Dictionary<string, object>? reader)
    {
        return new Patient
        {
            Id = (Guid)reader["id"],
            Name = reader["name"]?.ToString(),
            LastName = reader["last_name"].ToString(),
            Cuil = Cuil.Create(reader["cuil"].ToString()),
            Domicilie = new Domicilie
            {
                Number = Convert.ToInt32(reader["number"]),
                Street = reader["street"].ToString(),
                Locality = reader["locality"].ToString()
            }
        };
    }
}
