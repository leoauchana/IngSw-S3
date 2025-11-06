using IngSw_Application.DTOs;
using IngSw_Application.Exceptions;
using IngSw_Application.Services;
using IngSw_Domain.Entities;
using IngSw_Domain.Interfaces;
using IngSw_Domain.ValueObjects;
using NSubstitute;
using System.Data.Common;

namespace IngSw_Tests.RegisterPatient;

public class PatientsServiceTest
{
    private readonly IPatientRepository _patientsRepository;
    private readonly PatientsService _patientsService;
    public PatientsServiceTest()
    {
        _patientsRepository = Substitute.For<IPatientRepository>();
        _patientsService = new PatientsService(_patientsRepository);
    }
    [Fact]
    public async Task AddPatient_WhenTheHealthcareSystemExists_ShouldCreateThePatient()
    {
        // Arrange
        var patientDto = new PatientDto.Request(
            cuilPatient: "20-45750673-8",
            namePatient: "Lautaro",
            lastNamePatient: "Lopez",
            email: "lautalopez@gmail.com",
            streetDomicilie: "Avenue Nine Of July",
            numberDomicilie: 356,
            localityDomicilie: "CABA"
        );
        // Act
        var result = await _patientsService.AddPatient(patientDto);

        // Assert
        await _patientsRepository.Received(1).AddPatient(Arg.Any<Patient>());
        Assert.NotNull(result);
        Assert.Equal(patientDto.cuilPatient, result.cuilPatient);
        Assert.Equal(patientDto.namePatient, result.namePatient);
        Assert.Equal(patientDto.lastNamePatient, result.lastNamePatient);
    }

    [Fact]
    public async Task AddPatient_WhenCuilIsNotValid_ThenShouldThrowExceptionAndNotCreateThePatient()
    {
        // Arrange
        var patientDto = new PatientDto.Request(
            cuilPatient: "45750673",
            namePatient: "Lautaro",
            lastNamePatient: "Lopez",
            email: "lautalopez@gmail.com",
            streetDomicilie: "Avenue Nine Of July",
            numberDomicilie: 356,
            localityDomicilie: "CABA"
        );

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(
            () => _patientsService.AddPatient(patientDto)
        );

        Assert.Equal("CUIL con formato inválido.", exception.Message);

        await _patientsRepository.DidNotReceive().AddPatient(Arg.Any<Patient>());
    }

    [Fact]
    public async Task AddPatient_WhenCuilIsNull_ThenShouldThrowExceptionAndNotCreateThePatient()
    {
        // Arrange
        var patientDto = new PatientDto.Request(
            cuilPatient: null!,
            namePatient: "Lautaro",
            lastNamePatient: "Lopez",
            email: "lautalopez@gmail.com",
            streetDomicilie: "Avenue Nine Of July",
            numberDomicilie: 356,
            localityDomicilie: "CABA"
        );

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(
            () => _patientsService.AddPatient(patientDto)
        );

        Assert.Equal("CUIL no puede ser vacío.", exception.Message);

        await _patientsRepository.DidNotReceive().AddPatient(Arg.Any<Patient>());
    }

    [Fact]
    public async Task AddPatient_WhenCuilIsWhiteSpace_ThenShouldThrowExceptionAndNotCreateThePatient()
    {
        // Arrange
        var patientDto = new PatientDto.Request(
            cuilPatient: "   ",
            namePatient: "Lautaro",
            lastNamePatient: "Lopez",
            email: "lautalopez@gmail.com",
            streetDomicilie: "Avenue Nine Of July",
            numberDomicilie: 356,
            localityDomicilie: "CABA"
        );
        
        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(
            () => _patientsService.AddPatient(patientDto)
        );
        Assert.Equal("CUIL no puede ser vacío.", exception.Message);
        await _patientsRepository.DidNotReceive().AddPatient(Arg.Any<Patient>());
    }

    [Fact]
    public async Task AddPatient_WhenPatientAlreadyExists_ThenShouldThrowExceptionAndNotCreateThePatient()
    {
        // Arrange
        var patientDto = new PatientDto.Request(
            cuilPatient: "20-45750673-8",
            namePatient: "Lautaro",
            lastNamePatient: "Lopez",
            email: "lautalopez@gmail.com",
            streetDomicilie: "Avenue Nine Of July",
            numberDomicilie: 356,
            localityDomicilie: "CABA"
        );

        var existingPatient = new Patient
        {
            Cuil = Cuil.Create("20-45750673-8"),
            Name = "Lautaro",
            LastName = "Lopez",
            Email = "lautalopez@gmail.com"
        };

        _patientsRepository.GetByCuil(patientDto.cuilPatient)!.Returns(Task.FromResult(new List<Patient> { existingPatient }));

        // Act & Assert
        var exception = await Assert.ThrowsAsync<BusinessConflictException>(
            () => _patientsService.AddPatient(patientDto)
        );
        Assert.Equal($"El paciente de cuil {patientDto.cuilPatient} ya se encuentra registrado", exception.Message);
        await _patientsRepository.DidNotReceive().AddPatient(Arg.Any<Patient>());
    }

    [Fact]
    public async Task GetByCuil_WhenPatientsExistWithMatchingCuil_ShouldReturnAllMatchingPatients()
    {
        // Arrange
        string cuilReceived = "20-45758";

        var patientsFromRepository = new List<Patient>
        {
            new Patient
            {
                Cuil = Cuil.Create("20-45758331-8"),
                Name = "Lautaro",
                LastName = "Lopez",
                Email = "lautalopez@gmail.com",
                Domicilie = new Domicilie
                {
                    Number = 324,
                    Street = "Jujuy",
                    Locality = "San Miguel"
                }
            },
            new Patient
            {
                Cuil = Cuil.Create("20-43758621-4"),
                Name = "Lucia",
                LastName = "Perez",
                Email = "luciaperez@gmail.com",
                Domicilie = new Domicilie
                {
                    Number = 356,
                    Street = "Avenue Nine Of July",
                    Locality = "CABA"
                },
            }
        };

        _patientsRepository.GetByCuil(cuilReceived)
               .Returns(Task.FromResult<List<Patient>?>(patientsFromRepository));

        // Act
        var patientsFound = await _patientsService.GetByCuil(cuilReceived);

        // Assert
        await _patientsRepository.Received(1).GetByCuil(cuilReceived);

        Assert.NotNull(patientsFound);
        Assert.Equal(2, patientsFound.Count);
        // Comprobamos propiedades de los DTOs resultantes
        Assert.Equal(patientsFromRepository[0].Cuil.Value, patientsFound[0].cuilPatient);
        Assert.Equal(patientsFromRepository[1].Cuil.Value, patientsFound[1].cuilPatient);
        Assert.Equal(patientsFromRepository[0].Name, patientsFound[0].namePatient);
        Assert.Equal(patientsFromRepository[1].Name, patientsFound[1].namePatient);
    }
    [Fact]
    public async Task GetByCuil_WhenNoPatientsFound_ShouldThrowNullException()
    {
        // Arrange
        string cuilReceived = "20-45758";
        //Act & Arrange
        var exception = await Assert.ThrowsAsync<NullException>(
            () => _patientsService.GetByCuil(cuilReceived)
            );
        Assert.Equal($"No hay pacientes que coincidan con el cuil {cuilReceived} registrados.", exception.Message);
    }

}
