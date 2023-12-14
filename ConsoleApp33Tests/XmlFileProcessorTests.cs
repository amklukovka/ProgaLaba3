using ConsoleApp33.Laba3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApp3Tests
{
    public class XmlFileProcessorTests
    {
        private List<Contact> contacts;
        private XmlFileProcessor processor;

        public XmlFileProcessorTests()
        {
            contacts = new List<Contact> { new Contact("Test", "User", "1234567890", "test.user@example.com") };
            processor = new XmlFileProcessor(contacts);
        }

        // тест для метода SaveToXml()
        [Fact]
        public void Test_SaveToXml()
        {
            // Arrange
            var filename = "test.xml";

            // Act
            processor.SaveToXml(filename);

            // Assert
            Assert.True(File.Exists(filename));
            var content = File.ReadAllText(filename);
            var serializer = new XmlSerializer(typeof(List<Contact>));
            using (var reader = new StringReader(content))
            {
                var deserializedContacts = (List<Contact>)serializer.Deserialize(reader);
                Assert.False(contacts.SequenceEqual(deserializedContacts));
            }

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
            var serializer = new XmlSerializer(typeof(Contact));
            var expectedOutput = string.Join("\r\n", contacts.Select(c =>
            {
                using (var writer = new StringWriter())
                {
                    serializer.Serialize(writer, c);
                    return writer.ToString();
                }
            })) + "\r\n";
            Assert.Equal(expectedOutput, output.ToString());
        }
    }
}
