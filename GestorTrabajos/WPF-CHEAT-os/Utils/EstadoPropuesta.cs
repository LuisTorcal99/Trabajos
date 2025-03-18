

using System.ComponentModel;

namespace WPF_CHEAT_os.Utils
{
    public enum EstadoPropuesta
    {
        [Description("Enviada")]
        Enviada,

        [Description("Aceptada")]
        Aceptada,

        [Description("Rechazada")]
        Rechazada,

        [Description("Requiere Ampliación")]
        RequiereAmpliacion
    }

}
