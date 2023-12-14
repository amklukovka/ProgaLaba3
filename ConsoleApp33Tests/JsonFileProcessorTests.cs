using ConsoleApp33.Laba3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleApp3Tests
{
    public class JsonFileProcessorTests
    {
        private List<Contact> contacts;
        private JsonFileProcessor processor;

        public JsonFileProcessorTests()
        {
            contacts = new List<Contact> { new Contact { Name = "Test", Surname = "User", Phone = "1234567890", Email = "test.user@example.com" } };
            processor = new JsonFileProcessor(contacts);
        }

        // тест для метода SaveToJson()
        [Fact]
        public void Test_SaveToJson()
        {
            // Arrange
            var filename = "test.json";

            // Act
            processor.SaveToJson(filename);

            // Assert
            Assert.True(File.Exists(filename));
            var content = File.ReadAllText(filename);
            Assert.Equal(JsonSerializer.Serialize(contacts), content);

            // Cleanup
            File.Delete(filename);
        }

        // тест для метода DisplayContacts()
        [Fact]
        public void Test_DisplayContacts()
        {
            // Arrange
            var output = new StringWriter();
            Console.SetOut(output);

            // Act
            processor.DisplayContacts();

            // Assert
            var expectedOutput = "{\r\n  \"Name\": \"Test\",\r\n  \"Surname\": \"User\",\r\n  \"Phone\": \"1234567890\",\r\n  \"Email\": \"test.user@example.com\"\r\n}\r\n";
            Assert.Equal(expectedOutput, output.ToString());
        }


    }
}
