using IngSw_Application.DTOs;
using System.Threading.Tasks;

namespace IngSw_Application.Interfaces;

public interface IPatientsService
{
    Task<PatientDto.Response?> AddPatient(PatientDto.Request patientData);
    Task<List<PatientDto.Response>?> GetByCuil(string cuilPatient);
}
