using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Text.Json;
using System.Xml.Serialization;
using Microsoft.Data.Sqlite;
using ConsoleApp33.Laba3;


namespace ConsoleApp33.Laba3
{
    public class Notebook
    {
        // список для хранения контактов
        public List<Contact> contacts;

        // конструктор
        public Notebook()
        {
            contacts = new List<Contact>();
        }

        // метод для отображения меню
        public void ShowMenu()
        {
            Console.WriteLine("Введите номер действия и нажмите [Enter].");
            Console.WriteLine("Menu:");
            Console.WriteLine("1. View all contacts");
            Console.WriteLine("2. Search");
            Console.WriteLine("3. New contact");
            Console.WriteLine("4. Save and show file");
            Console.WriteLine("5. Exit");
        }

        // метод сохранения и вывода данных
        public void SaveD()
        {
            Console.WriteLine("Выберите формат для сохранения данных:");
            Console.WriteLine("1. JSON");
            Console.WriteLine("2. XML");
            Console.WriteLine("3. SQLite");
            int formatChoice = int.Parse(Console.ReadLine());
            switch (formatChoice)
            {
                case 1:
                    JsonFileProcessor jsonfile = new JsonFileProcessor(contacts);
                    jsonfile.SaveToJson("contacts.json");
                    jsonfile.DisplayContacts();
                    break;
                case 2:
                    XmlFileProcessor xmlfile = new XmlFileProcessor(contacts);
                    xmlfile.SaveToXml("contacts.xml");
                    xmlfile.DisplayContacts();
                    break;
                case 3:
                    Console.WriteLine("Введите имя для базы данных:");
                    string dbName = Console.ReadLine();
                    DatabaseProcessor sqlitefile = new DatabaseProcessor(dbName, contacts);
                    sqlitefile.CreateContactsTable();
                    sqlitefile.DisplayContactsFromDatabase();
                    break;
                default:
                    Console.WriteLine("Некорректный выбор.");
                    break;
            }
        }

        // метод отображения всех контактов
        public void ViewAll()
        {
            Console.WriteLine("Все контакты:");
            if (contacts.Count == 0)
            {
                Console.WriteLine("Контакты не найдены.");
            }
            else
            {
                int index = 1;
                foreach (Contact c in contacts)
                {
                    Console.WriteLine("Результатов найдено ({0}) :", index);
                    c.Show();
                    index++;
                }
            }
        }

        // метод поиска контактов
        public void Search()
        {
            Console.WriteLine("Найти по:");
            Console.WriteLine("1. Name");
            Console.WriteLine("2. Surname");
            Console.WriteLine("3. Name and Surname");
            Console.WriteLine("4. Phone");
            Console.WriteLine("5. E-mail");

            // объявляем переменную для хранения номера
            int number;

            // пытаемся преобразовать ввод пользователя в число
            bool isNumber = int.TryParse(Console.ReadLine(), out number);

            // если это не удалось, то выводим сообщение об ошибке и повторяем запрос
            while (!isNumber || number < 1 || number > 5)
            {
                Console.WriteLine("Некорректное значение. Введите целое число от 1 до 5.");
                isNumber = int.TryParse(Console.ReadLine(), out number);
            }

            Console.WriteLine("Введите данные для поиска: ");
            string info = Console.ReadLine();
            Console.WriteLine("Выполняется поиск контакта...");
            List<Contact> results = new List<Contact>();
            switch (number)
            {
                case 1: // поиск по Name
                    results = contacts.Where(c => c.Name.Contains(info)).ToList();
                    break;
                case 2: // поиск по Surname
                    results = contacts.Where(c => c.Surname.Contains(info)).ToList();
                    break;
                case 3: // поиск по Name and Surname
                    results = contacts.Where(c => c.Name.Contains(info) || c.Surname.Contains(info)).ToList();
                    break;
                case 4: // поиск по Phone
                    results = contacts.Where(c => c.Phone.Contains(info)).ToList();
                    break;
                case 5: // поиск по E-mail
                    results = contacts.Where(c => c.Email.Contains(info)).ToList();
                    break;
                default: // некорректное значение
                    Console.WriteLine("Некорректное значение.");
                    break;
            }
            if (results.Count == 0)
            {
                Console.WriteLine("Контакты не найдены.");
            }
            else
            {
                int index = 1;
                foreach (Contact c in results)
                {
                    Console.WriteLine("Результатов найдено ({0}) :", index);
                    c.Show();
                    index++;
                }
            }
        }

        // метод создания нового контакта
        public void NewContact()
        {
            Console.WriteLine("Новый контакт:");
            Console.Write("Name: ");
            string name = Console.ReadLine();

            while (string.IsNullOrEmpty(name)) // проверяем, что имя не пустое
            {
                Console.WriteLine("Имя не может быть пустым. Пожалуйста, введите имя ещё раз.");
                Console.Write("Name: ");
                name = Console.ReadLine();
            }
            Console.Write("Surname: ");
            string surname = Console.ReadLine();

            while (string.IsNullOrEmpty(surname)) // проверяем, что фамилия не пустая
            {
                Console.WriteLine("Фамилия не может быть пустой. Пожалуйста, введите фамилию ещё раз.");
                Console.Write("Surname: ");
                surname = Console.ReadLine();
            }
            Console.Write("Phone: ");
            string phone = Console.ReadLine();

            while (string.IsNullOrEmpty(phone)) // проверяем, что телефон не пустой
            {
                Console.WriteLine("Телефон не может быть пустым. Пожалуйста, введите телефон ещё раз.");
                Console.Write("Phone: ");
                phone = Console.ReadLine();
            }
            Console.Write("E-mail: ");
            string email = Console.ReadLine();

            while (string.IsNullOrEmpty(email)) // проверяем, что электронная почта не пустая
            {
                Console.WriteLine("E-mail не может быть пустым. Пожалуйста, введите e-mail ещё раз.");
                Console.Write("E-mail: ");
                email = Console.ReadLine();
            }
            Contact c = new Contact(name, surname, phone, email);
            contacts.Add(c);
            Console.WriteLine("Контакт создан.");
        }


        // метод закрытия записной книжки
        public void Exit()
        {
            Console.WriteLine("Выход...");
            Environment.Exit(0); // завершает программу успешно и без ошибок
        }
    }

}
