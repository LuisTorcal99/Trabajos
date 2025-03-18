using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_CHEAT_os.DTO;

namespace WPF_CHEAT_os.Interfaces
{
    public interface IAsignarProvider
    {
        List<UsuarioDTO> ListaClean(List<UsuarioDTO> listaAprobados, List<UsuarioDTO> ListaAsignada);
        List<UsuarioDTO> AlumnosAsignados(List<UsuarioDTO> listaAprobados, int maxAlumnos);
        int TutorPuedeLlevar(int horas, int alumnos);
    }
}
