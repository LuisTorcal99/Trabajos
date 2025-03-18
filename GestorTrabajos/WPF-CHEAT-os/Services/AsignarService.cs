using System;
using System.Collections.Generic;
using System.Linq;
using WPF_CHEAT_os.DTO;
using WPF_CHEAT_os.Interfaces;
using WPF_CHEAT_os.Utils;

namespace WPF_CHEAT_os.Services
{
    public class AsignarService : IAsignarProvider
    {
        private const int HORAS_PROFES_TOTALES = 540;

        public AsignarService() { }

        // Número de alumnos entre número de horas totales y multiplicado por la hora de cada profesor
        public int TutorPuedeLlevar(int horas, int alumnos)
        {
            // Calcular el número de alumnos que puede llevar el tutor
            int alumnosPermitidos = (alumnos / HORAS_PROFES_TOTALES) * horas;

            return alumnosPermitidos;
        }

        // Asigna alumnos de manera aleatoria asegurándose de que solo sean alumnos
        public List<UsuarioDTO> AlumnosAsignados(List<UsuarioDTO> listaAprobados, int maxAlumnos)
        {
            if (listaAprobados == null || listaAprobados.Count == 0) return new List<UsuarioDTO>();

            // Filtrar solo los usuarios que tienen el rol de "Alumno"
            List<UsuarioDTO> soloAlumnos = listaAprobados.Where(u => u.Rol == Constants.ROLE_REGISTRER_ALUMNO).ToList();

            // Mezclar la lista de alumnos aleatoriamente
            Random r = new Random();
            List<UsuarioDTO> listaMezclada = soloAlumnos.OrderBy(x => r.Next()).ToList();

            // Tomar solo el número máximo de alumnos permitido
            return listaMezclada.Take(maxAlumnos).ToList();
        }

        // Metodo para eliminar de la lista de aprobados los alumnos que ya estan asignados a otro profesor
        public List<UsuarioDTO> ListaClean(List<UsuarioDTO> listaAprobados, List<UsuarioDTO> ListaAsignada)
        {
            if (listaAprobados == null || ListaAsignada == null) return new List<UsuarioDTO>();

            // Filtrar solo los alumnos de la lista de aprobados
            List<UsuarioDTO> soloAlumnosAprobados = listaAprobados.Where(u => u.Rol == Constants.ROLE_REGISTRER_ALUMNO).ToList();

            // Remover de la lista de aprobados los alumnos que ya han sido asignados
            soloAlumnosAprobados.RemoveAll(alumno => ListaAsignada.Any(asignado => asignado.Id == alumno.Id));

            return soloAlumnosAprobados;
        }
    }
}
