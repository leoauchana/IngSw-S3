using IngSw_Domain.Entities;

namespace IngSw_Domain.Interfaces;

public interface IPatientRepository
{
    Task<List<Patient>?> GetByCuil(string cuilPatient);
    Task AddPatient(Patient newPatient);
}
