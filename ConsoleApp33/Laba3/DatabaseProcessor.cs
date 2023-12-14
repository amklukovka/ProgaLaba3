using ConsoleApp33.Laba3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;


namespace ConsoleApp33.Laba3
{
    public class DatabaseProcessor
    {
        private string connectionString;
        public List<Contact> contacts;

        // конструктор
        public DatabaseProcessor(string dbName, List<Contact> contactsList)
        {
            connectionString = $"Data Source={dbName};";
            contacts = contactsList;
        }

        // метод создания таблицы в базе данных и заполнения её данными
        public void CreateContactsTable()
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                // создание таблицы
                using (SqliteCommand createTableCommand = new SqliteCommand("CREATE TABLE IF NOT EXISTS contacts (name TEXT, surname TEXT, phone TEXT, email TEXT);", connection))
                {
                    createTableCommand.ExecuteNonQuery();
                }

                // вставка данных в таблицу
                foreach (var contact in contacts)
                {
                    using (SqliteCommand insertCommand = new SqliteCommand("INSERT INTO contacts (name, surname, phone, email) VALUES (@name, @surname, @phone, @email);", connection))
                    {
                        insertCommand.Parameters.AddWithValue("@name", contact.Name);
                        insertCommand.Parameters.AddWithValue("@surname", contact.Surname);
                        insertCommand.Parameters.AddWithValue("@phone", contact.Phone);
                        insertCommand.Parameters.AddWithValue("@email", contact.Email);
                        insertCommand.ExecuteNonQuery();
                    }
                }
            }
        }

        // метод вывода данных на экран
        public void DisplayContactsFromDatabase()
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                using (SqliteCommand command = new SqliteCommand("SELECT name, surname, phone, email FROM contacts", connection))
                {
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"INSERT INTO contacts (name, surname, phone, email) VALUES ('{reader["name"]}', '{reader["surname"]}', '{reader["phone"]}', '{reader["email"]}');");
                        }
                    }
                }
            }
        }
    }

    // вспомогательный
    class SqliteCommandExample
    {
        private SqliteCommand command;

        // конструктор
        public SqliteCommandExample(SqliteConnection connection, string commandText)
        {
            command = new SqliteCommand(commandText, connection); // создание экземпляра с текстом команды и подключением к базе данных
        }

        // метод для выполнения команды
        public void ExecuteCommand()
        {
            // открытие подключения к базе данных, если оно ещё не открыто
            if (command.Connection.State != System.Data.ConnectionState.Open)
            {
                command.Connection.Open();
            }

            command.ExecuteNonQuery(); // выполнение команды
            command.Connection.Close(); // закрытие подключения к базе данных
        }
    }

}
