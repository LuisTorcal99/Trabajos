using System.IO;
using System.Text.Json;

namespace BasicAPP.Utils
{
    public static class FileUtils<T>
    {
        /// <summary>
        /// Sobrescribe un archivo con el contenido serializado del objeto proporcionado.
        /// </summary>
        /// <param name="path">Ruta del archivo.</param>
        /// <param name="obj">Objeto a serializar y escribir.</param>
        /// <returns>Devuelve true si la operación fue exitosa, de lo contrario false.</returns>
        public static async Task<bool> OverWriteFile(string path, T obj)
        {
            try
            {
                string listaSerializada = JsonSerializer.Serialize(obj);
                await File.WriteAllTextAsync(path, listaSerializada);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Agrega una nueva línea al archivo con el contenido serializado del objeto proporcionado.
        /// </summary>
        /// <param name="path">Ruta del archivo.</param>
        /// <param name="obj">Objeto a serializar y agregar al archivo.</param>
        /// <returns>Devuelve true si la operación fue exitosa, de lo contrario false.</returns>
        public static async Task<bool> AppendLineToFile(string path, T obj)
        {
            try
            {
                using (StreamWriter w = File.AppendText(path))
                {
                    string listaSerializada = JsonSerializer.Serialize(obj);
                    w.WriteLine(listaSerializada);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Lee un archivo completo y lo deserializa en un objeto del tipo especificado.
        /// </summary>
        /// <param name="path">Ruta del archivo.</param>
        /// <returns>Devuelve el objeto deserializado o el valor por defecto del tipo si ocurre un error.</returns>
        public static async Task<T> ReadFile(string path)
        {
            try
            {
                string contenidoArchivo = await File.ReadAllTextAsync(path);
                return JsonSerializer.Deserialize<T>(contenidoArchivo);
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        /// <summary>
        /// Lee un archivo línea por línea, deserializando cada línea en un objeto del tipo especificado.
        /// </summary>
        /// <param name="path">Ruta del archivo.</param>
        /// <returns>Devuelve una colección de objetos deserializados o una lista vacía si ocurre un error.</returns>
        public static async Task<IEnumerable<T>> ReadFileLineByLine(string path)
        {
            var result = new List<T>();
            try
            {
                string line;
                using (StreamReader reader = new StreamReader(path))
                {
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        result.Add(JsonSerializer.Deserialize<T>(line));
                    }
                }
                return result;

            }
            catch (Exception ex)
            {
                return result;
            }
        }
        public static async Task<IEnumerable<string>> ReadPlainTextFile(string path)
        {
            var lines = new List<string>();
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string line;
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        lines.Add(line);
                    }
                }
                return lines;
            }
            catch
            {
                return lines; // Devuelve una lista vacía en caso de error
            }
        }
        // IEnumerable<string> lineas = await FileUtils<string>.ReadPlainTextFile
    }
}
