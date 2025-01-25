using System;
using System.IO;

namespace BasicAPP.Utils
{
    public class CrearDirectorio
    {
        public static void CrearDirectorioInicial()
        {
            if (!Directory.Exists(Constantes.BASE_LOCAL_DIRECTORY))
            {
                // Crear el directorio FILES
                Directory.CreateDirectory(Constantes.BASE_LOCAL_DIRECTORY);
            }
        }

        public static void CrearCarpeta(string nombre, string rutaBase)
        {
            try
            {
                string nuevaRuta = Path.Combine(rutaBase, nombre);
                if (!Directory.Exists(nuevaRuta))
                {
                    Directory.CreateDirectory(nuevaRuta);
                }
                else
                {
                    Console.WriteLine("La carpeta ya existe: " + nuevaRuta);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al crear la carpeta: " + ex.Message);
            }
        }

        public static void CrearFichero(string nombre, string rutaBase, string contenido)
        {
            try
            {
                string nuevaRutaFichero = Path.Combine(rutaBase, nombre);
                if (!File.Exists(nuevaRutaFichero))
                {
                    File.WriteAllText(nuevaRutaFichero, contenido);
                }
                else
                {
                    Console.WriteLine("El fichero ya existe: " + nuevaRutaFichero);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al crear el fichero: " + ex.Message);
            }
        }

        private static string GenerateRandomFileName()
        {
            // Generar un nombre de archivo aleatorio
            return Path.GetRandomFileName().Replace(".", "");
        }
    }
}