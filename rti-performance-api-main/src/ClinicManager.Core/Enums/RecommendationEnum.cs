using System.ComponentModel;

namespace Clinic_Manager.Core.Enums
{
    public enum RecommendationEnum
    {
        [Description("Medico")]
        Doctor = 0,

        [Description("Fisioterapeuta")]
        Physiotherapist = 1,

        [Description("Professor de Educacao Fisica")]
        PhysicalEducationTeacher = 2,

        [Description("Terapeuta Ocupacional")]
        OccupationalTherapist = 3,
    }
}
