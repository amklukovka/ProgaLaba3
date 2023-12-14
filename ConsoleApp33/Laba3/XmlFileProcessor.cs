using ConsoleApp33.Laba3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApp33.Laba3
{
    public class XmlFileProcessor
    {
        private List<Contact> contacts;

        // конструктор
        public XmlFileProcessor(List<Contact> c)
        {

            contacts = new List<Contact>((IEnumerable<Contact>)c);
        }

        // метод сохранения данных
        public void SaveToXml(string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Contact>));
            using (FileStream stream = new FileStream(filename, FileMode.Create))
            {
                serializer.Serialize(stream, contacts);
            }
        }

        // метод вывода данных на экран
        public void DisplayContacts()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Contact));
            using (StringWriter writer = new StringWriter())
            {
                foreach (var contact in contacts)
                {
                    serializer.Serialize(writer, contact);
                    Console.WriteLine(writer.ToString());
                    writer.GetStringBuilder().Clear(); // очистка содержимого writer для следующего контакта
                }
            }
        }
    }
}
