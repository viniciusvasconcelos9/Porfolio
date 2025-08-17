using System.ComponentModel;

namespace Clinic_Manager.Core.Enums
{
    public enum EducationLevelEnum
    {
        [Description("Nenhuma")]
        None,

        [Description("Pré-escola")]
        Preschool,

        [Description("Ensino Fundamental")]
        ElementarySchool,

        [Description("Ensino Médio (Fundamental II)")]
        MiddleSchool,

        [Description("Ensino Médio")]
        HighSchool,

        [Description("Curso Técnico")]
        TechnicalCourse,

        [Description("Tecnólogo")]
        AssociateDegree,

        [Description("Graduação (Bacharelado)")]
        BachelorDegree,

        [Description("Pós-graduação")]
        PostGraduate,

        [Description("Mestrado")]
        MasterDegree,

        [Description("Doutorado")]
        DoctorateDegree,

        [Description("Pós-Doutorado")]
        PostDoctorate
    }
}
