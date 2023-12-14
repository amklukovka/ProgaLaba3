using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using ConsoleApp33.Laba3;

namespace Laba3.Tests
{
    public class NotebookTests
    {
        private Notebook notebook; // тестовый экземпляр класса Notebook

        public NotebookTests()
        {
            notebook = new Notebook(); // инициализация тестового экземпляра
        }

        // тест для конструктора
        [Fact]
        public void Test_Constructor()
        {
            // Act
            notebook = new Notebook();

            // Assert
            Assert.NotNull(notebook.contacts);
            Assert.Empty(notebook.contacts);
        }

        // тест для метода ShowMenu
        [Fact]
        public void Test_ShowMenu()
        {
            // Arrange
            var expectedOutput = "Введите номер действия и нажмите [Enter].\r\nMenu:\r\n1. View all contacts\r\n2. Search\r\n3. New contact\r\n4. Save and show file\r\n5. Exit\r\n";
            var writer = new StringWriter();
            Console.SetOut(writer);

            // Act
            notebook.ShowMenu();

            // Assert
            Assert.Equal(expectedOutput, writer.ToString());
        }

        // тест для метода ViewAll, когда список контактов пуст
        [Fact]
        public void TestViewAll_WhenNoContacts()
        {
            // Arrange
            notebook = new Notebook();
            var expectedOutput = "Все контакты:\r\nКонтакты не найдены.\r\n";
            var writer = new StringWriter();
            Console.SetOut(writer);

            // Act
            notebook.ViewAll();

            // Assert
            Assert.Equal(expectedOutput, writer.ToString());
        }

        // тест для метода ViewAll, когда список контактов не пуст
        [Fact]
        public void TestViewAll_WhenContactsExist()
        {
            // Arrange
            notebook = new Notebook();
            //var c = new Contact ("Test", "User", "1234567890", "test@user.com");
            notebook.contacts.Add(new Contact("Test", "User", "1234567890", "test@user.com"));
            var expectedOutput = "Все контакты:\r\nРезультатов найдено (1) :\r\nName: Test\r\nSurname: User\r\nPhone: 1234567890\r\nE-mail: test@user.com\r\n";
            var writer = new StringWriter();
            Console.SetOut(writer);

            // Act
            notebook.ViewAll();

            // Assert
            Assert.Equal(expectedOutput, writer.ToString());
        }

        // тест для метода Search()
        [Fact]
        public void Test_SearchReturnsCorrectResults()
        {
            // Arrange
            var notebook = new Notebook();
            notebook.contacts.Add(new Contact("Test", "User", "1234567890", "test.user@example.com"));

            var input = new StringReader("1\nTest\n");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            // Act
            notebook.Search();

            // Assert
            var expectedOutput = "Найти по:\r\n1. Name\r\n2. Surname\r\n3. Name and Surname\r\n4. Phone\r\n5. E-mail\r\nВведите данные для поиска: \r\nВыполняется поиск контакта...\r\nРезультатов найдено (1) :\r\nName: Test\r\nSurname: User\r\nPhone: 1234567890\r\nE-mail: test.user@example.com\r\n";
            Assert.Equal(expectedOutput, output.ToString());
        }

        // тест для метода NewContact()
        [Fact]
        public void Test_NewContact()
        {
            // Arrange
            var notebook = new Notebook();

            var input = new StringReader("Test\nUser\n1234567890\ntest.user@example.com\n");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            // Act
            notebook.NewContact();

            // Assert
            Assert.Single(notebook.contacts);
            Assert.Equal("Test", notebook.contacts[0].Name);
            Assert.Equal("User", notebook.contacts[0].Surname);
            Assert.Equal("1234567890", notebook.contacts[0].Phone);
            Assert.Equal("test.user@example.com", notebook.contacts[0].Email);

            var expectedOutput = "Новый контакт:\r\nName: Surname: Phone: E-mail: Контакт создан.\r\n";
            Assert.Equal(expectedOutput, output.ToString());
        }
    }
}
