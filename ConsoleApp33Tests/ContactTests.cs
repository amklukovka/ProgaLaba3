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
        // ���� ��� ������������ Contact
        [Fact]
        public void Test_Contact_Constructor()
        {
            // ������� ��������� ������ Contact � ��������� �����������
            Contact contact = new Contact("����", "�����", "+71234567899", "tonystark@gmail.com");

            // ���������, ��� ���� ������ Contact ������������� �������� ����������
            Assert.Equal("����", contact.Name);
            Assert.Equal("�����", contact.Surname);
            Assert.Equal("+71234567899", contact.Phone);
            Assert.Equal("tonystark@gmail.com", contact.Email);
        }

        // ���� ��� ������ Show
        [Fact]
        public void Test_Show()
        {
            // ������� ��������� ������ Contact � ��������� �����������
            Contact contact = new Contact("�����", "������", "+73216549877", "peterparker@gmail.com");

            // �������������� ����������� ����� � ��������� ��������
            using (System.IO.StringWriter sw = new System.IO.StringWriter())
            {
                Console.SetOut(sw); // �������������� ����� ������ �� ������
                contact.Show(); // �������� ����� Show ��� ��������

                Console.SetOut(Console.Out); // ��������������� ����������� ����� �� �������
                string output = sw.ToString(); // �������� ������, ������� ���� �������� ������� Show

                // ���������, ��� ������ �������� ��������� ������ ��������
                Assert.Contains("Name: �����", output);
                Assert.Contains("Surname: ������", output);
                Assert.Contains("Phone: +73216549877", output);
                Assert.Contains("E-mail: peterparker@gmail.com", output);
            }
        }
    }
}