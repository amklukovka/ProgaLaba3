using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp33.Laba3;

namespace ConsoleApp33.Laba3
{
    public class Contact
    {
        // поля для информации контакта
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        // контструктор
        public Contact(string name, string surname, string phone, string email)
        {
            Name = name;
            Surname = surname;
            Phone = phone;
            Email = email;
        }

        // конструктор без параметров
        public Contact()
        {
        }

        // метод отображения данных контакта
        public void Show()
        {
            Console.WriteLine("Name: {0}", Name);
            Console.WriteLine("Surname: {0}", Surname);
            Console.WriteLine("Phone: {0}", Phone);
            Console.WriteLine("E-mail: {0}", Email);
        }
    }
}