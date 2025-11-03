using IngSw_Application.Services;
using IngSw_Domain.Entities;
using IngSw_Domain.Interfaces;
using NSubstitute;

namespace IngSw_Tests.RegisterPatient;

public class PatientsServiceTest
{
    [Fact]
    public void AddPatient()
    {
        // Arrange
        var iPatientRepository = Substitute.For<IPatientRepository>();
        var patientsService = new PatientsService(iPatientRepository);
        // Act

        // Assert
    }
}
