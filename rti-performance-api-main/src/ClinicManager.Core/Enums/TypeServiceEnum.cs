using System.ComponentModel;

namespace Clinic_Manager.Core.Enums
{
    public enum TypeServiceEnum
    {
        [Description("Atestado")]
        Atestado = 1,
        [Description("Receita")]
        Receita = 2,
        [Description("Evolução")]
        Evolução = 3

    }
}
