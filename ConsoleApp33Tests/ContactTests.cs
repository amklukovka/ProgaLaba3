using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using ConsoleApp33.Laba3;


namespace Laba3.Tests
{
    public class ContactTests
    {
        // тест для конструктора Contact
        [Fact]
        public void Test_Contact_Constructor()
        {
            // создаем экземпляр класса Contact с заданными параметрами
            Contact contact = new Contact("Тони", "Старк", "+71234567899", "tonystark@gmail.com");

            // проверяем, что поля класса Contact соответствуют заданным параметрам
            Assert.Equal("Тони", contact.Name);
            Assert.Equal("Старк", contact.Surname);
            Assert.Equal("+71234567899", contact.Phone);
            Assert.Equal("tonystark@gmail.com", contact.Email);
        }

        // тест для метода Show
        [Fact]
        public void Test_Show()
        {
            // создаем экземпляр класса Contact с заданными параметрами
            Contact contact = new Contact("Питер", "Паркер", "+73216549877", "peterparker@gmail.com");

            // перенаправляем стандартный вывод в строковый писатель
            using (System.IO.StringWriter sw = new System.IO.StringWriter())
            {
                Console.SetOut(sw); // перенаправляем поток вывода на объект
                contact.Show(); // вызываем метод Show для контакта

                Console.SetOut(Console.Out); // восстанавливаем стандартный вывод на консоль
                string output = sw.ToString(); // получаем строку, которая была выведена методом Show

                // проверяем, что строка содержит ожидаемые данные контакта
                Assert.Contains("Name: Питер", output);
                Assert.Contains("Surname: Паркер", output);
                Assert.Contains("Phone: +73216549877", output);
                Assert.Contains("E-mail: peterparker@gmail.com", output);
            }
        }
    }
}