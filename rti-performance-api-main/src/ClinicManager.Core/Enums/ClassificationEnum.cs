using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Manager.Core.Enums
{
    public enum ClassificationEnum
    {
        [Description("Vermelho")]
        Red,

        [Description("Amarelo")]
        Yellow,

        [Description("Verde")]
        Green,

        [Description("Azul")]
        Blue,
    }
}
