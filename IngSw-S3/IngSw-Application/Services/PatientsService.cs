using IngSw_Application.DTOs;
using IngSw_Application.Exceptions;
using IngSw_Domain.Entities;
using IngSw_Domain.Interfaces;
using IngSw_Domain.ValueObjects;

namespace IngSw_Application.Services;

public class PatientsService
{
    private readonly IPatientRepository _patientRepository;
    public PatientsService(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<PatientDto.Response?> AddPatient(PatientDto.Request patientData)
    {
        var patientFound = await _patientRepository.GetByCuil(patientData.cuilPatient);
        if (patientFound != null)
            throw new BusinessConflictException($"El paciente de cuil {patientData.cuilPatient} ya se encuentra registrado");
        var newPatient = new Patient
        {
            Cuil = Cuil.Create(patientData.cuilPatient),
            Name = patientData.namePatient,
            LastName = patientData.lastNamePatient,
            Email = patientData.email,
            Domicilie = new Domicilie
            {
                Number = patientData.numberDomicilie,
                Street = patientData.streetDomicilie,
                Locality = patientData.localityDomicilie
            }
        };
        await _patientRepository.AddPatient(newPatient);
        return new PatientDto.Response(newPatient.Cuil.Value!, newPatient.Name!, newPatient.LastName!, newPatient.Email!,
                    newPatient.Domicilie!.Street!, newPatient.Domicilie.Number, newPatient.Domicilie.Locality!);
    }

    public async Task<List<PatientDto.Response>?> GetByCuil(string cuilPatient)
    {
        var cuilValid = Cuil.Create(cuilPatient);
        var patientsFounds = await _patientRepository.GetByCuil(cuilValid.Value);
        if (patientsFounds == null || !(patientsFounds.Count > 0))
            throw new NullException($"No hay pacientes que coincidan con el cuil {cuilValid.Value} registrados.");
        return patientsFounds.Select(pr => new PatientDto.Response(pr.Cuil!.Value!, pr.Name!, pr.LastName!,
                    pr.Email!, pr.Domicilie!.Street!, pr.Domicilie.Number, pr.Domicilie.Locality!))
                    .ToList();
    }
}
