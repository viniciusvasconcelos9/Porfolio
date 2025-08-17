using System.ComponentModel;

namespace Clinic_Manager.Core.Enums
{
    public enum MaritalStatusEnum
    {
        [Description("Solteiro")]
        Single,

        [Description("Casado")]
        Married,

        [Description("Separado")]
        Separated,

        [Description("Divorciado")]
        Divorced,

        [Description("Viuvo")]
        Widowed,
    }
}
