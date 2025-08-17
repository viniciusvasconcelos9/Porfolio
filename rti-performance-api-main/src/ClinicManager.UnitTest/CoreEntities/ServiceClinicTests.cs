using Clinic_Manager.Core.Entities;
using Clinic_Manager.Core.Enums;

namespace ClinicManager.UnitTests.CoreEntities
{
    public class ServiceClinicTests
    {
        [Fact]
        public void TestIfProjectStartWorks()
        {
            var serviceClinic = new ServiceClinic
            {
                IdPatient = new Patient(),
                IdService = new Service(),
                IdDoctor = new Doctor(),
                HealthInsurance = "Insurance",
                Start = DateTime.Now,
                End = DateTime.Now.AddHours(1),
                TypeServices = TypeServiceEnum.Receita
            };

            var result = serviceClinic.IsValid();

            Assert.True(result);

        }
    }
}
