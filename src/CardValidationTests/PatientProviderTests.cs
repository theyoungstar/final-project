using Catalyte.Apparel.Data.Interfaces;
using Catalyte.Apparel.Data.Model;
using Catalyte.Apparel.Providers.Providers;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalyte.Apparel.Utilities.HttpResponseExceptions;

namespace Catalyte.Apparel.Test.Unit
{
    public class PatientProviderTest
    {
        private readonly PatientProvider provider;
        private readonly Mock<IPatientRepository> repositoryStub;
        private readonly Mock<ILogger<PatientProvider>> loggerStub;
        private readonly Mock<PatientValidation> patientValidationStub;
        private readonly Mock<IEncounterRepository> encounterRepositoryStub;
        private readonly ServiceUnavailableException exception;
        private readonly Patient testPatient;
        public PatientProviderTest()
        {
            repositoryStub = new Mock<IPatientRepository>();
            encounterRepositoryStub = new Mock<IEncounterRepository>();
            loggerStub = new Mock<ILogger<PatientProvider>>();
            patientValidationStub = new Mock<PatientValidation>();
            exception = new ServiceUnavailableException("There was a problem connecting to the database.");

            provider = new PatientProvider(repositoryStub.Object, loggerStub.Object, patientValidationStub.Object, encounterRepositoryStub.Object);
        }

        [Fact]
        public async Task CreatePatientReturnsPurchase()
        {
            //Arrange
            Patient patient = new();
            List<string> errorsList = new List<string>();
            patientValidationStub.Setup(stub => stub.ValidationForPatient(It.IsAny<Patient>())).Returns(errorsList);
            repositoryStub.Setup(stub => stub.CreatePatientAsync(It.IsAny<Patient>())).ReturnsAsync(patient);

            //Act
            var actual = await provider.CreatePatientAsync(patient);

            //Assert
            Assert.Equal(actual, patient);
        }
        [Fact]
        public void GetAllPatientsAsync_ThrowsServiceUnavailableExceptionIfDatabaseIsInactive()
        {
            repositoryStub.Setup(repo => repo.GetAllPatientsAsync()).ThrowsAsync(exception);

            Assert.ThrowsAsync<ServiceUnavailableException>(() => provider.GetAllPatientsAsync());
        }
        [Fact]
        public void GetPatientById_ReturnsCorrectPatient()
        {
            var target = testPatient;
            var queried = repositoryStub.Object.GetPatientByIdAsync(1).Result;
            Assert.Equal(target, queried);
        }
    }
}
