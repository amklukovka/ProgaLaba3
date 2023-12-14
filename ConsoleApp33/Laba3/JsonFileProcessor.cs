using ConsoleApp33.Laba3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleApp33.Laba3
{
    public class JsonFileProcessor
    {
        private List<Contact> contacts;

        // конструктор
        public JsonFileProcessor(List<Contact> c)
        {
            contacts = new List<Contact>((IEnumerable<Contact>)c);
        }

        // метод сохранения данных в файл в формате JSON
        public void SaveToJson(string filename)
        {
            string json = JsonSerializer.Serialize(contacts);
            File.WriteAllText(filename, json);
        }

        // метод вывода данных на экран
        public void DisplayContacts()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            foreach (var contact in contacts)
            {
                Console.WriteLine(JsonSerializer.Serialize(contact, options));
            }
        }
    }
}
