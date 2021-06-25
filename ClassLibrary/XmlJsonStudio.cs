using Newtonsoft.Json;
using System.IO;
using System.Xml.Serialization;

namespace ClassLibrary
{
    /// <summary>
    /// Класс с методами сериализации и десериализации объектов произвольного класса из формата XML или Json.
    /// </summary>
    public static class XmlJsonStudio
    {
                /// <summary>
        /// Восстанавливает объект класса T из формата XML или Json (по умолчанию).
        /// </summary>
        /// <param name="path">Маршрут к файлу.</param>
        /// <param name="mode">Режим сериализации xml - true или json - false (по умолчанию).</param>
        /// <returns>Восстановленный объект.</returns>
        public static T Deserialize<T>(string path, bool mode = false)
        {
            if (mode)
                using (Stream fs = File.OpenRead(path))
                    return (T)new XmlSerializer(typeof(T)).Deserialize(fs);
            else
                return JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
        }
        /// <summary>
        /// Сериализует объект класса T в формат XML или Json (по умолчанию).
        /// </summary>
        /// <param name="path">Маршрут к файлу.</param>
        /// <param name="this">Объект для сериализации.</param>
        /// <param name="mode">Режим сериализации xml - true или json - false (по умолчанию).</param>
        public static void Serialize<T>(string path, T @this, bool mode = false)
        {
            if (mode)
                using (Stream fs = File.OpenWrite(path))
                    new XmlSerializer(typeof(T)).Serialize(fs, @this);
            else
                File.WriteAllText(path, JsonConvert.SerializeObject(@this));
        }
    }
}
