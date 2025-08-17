using System.ComponentModel;

namespace Clinic_Manager.Core.Enums
{
    public enum StatusEnum
    {
        [Description("Acolhimento")]
        Reception,

        [Description("Consulta do Bem Estar")]
        WellbeingConsultation,

        [Description("Treinamento")]
        Training,

        [Description("Tratamento")]
        Treatment,

        [Description("Alta")]
        MedicalRelease,

        [Description("Obito")]
        Death,
    }
}
