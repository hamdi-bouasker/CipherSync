using CipherShield.Models;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace CipherShield.Helpers
{
    internal class DatabaseHelper
    {
        private readonly string _connectionString;

        static DatabaseHelper()
        {
            // Initialize SQLitePCL to use SQLCipher
            SQLitePCL.Batteries_V2.Init();
        }

        public DatabaseHelper(string password)
        {
            _connectionString = $"Data Source=encrypted_pwd_database.db;Password={password}";
            InitializeDatabase();
        }



        private void InitializeDatabase()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"
                CREATE TABLE IF NOT EXISTS PasswordEntries (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Website TEXT NOT NULL,
                    Username TEXT NOT NULL,
                    Password TEXT NOT NULL
                )";
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<PasswordEntry> GetAllEntries()
        {
            var entries = new List<PasswordEntry>();

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT Id, Website, Username, Password FROM PasswordEntries";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var entry = new PasswordEntry
                        {
                            Id = reader.GetInt32(0),
                            Website = reader.GetString(1),
                            Username = reader.GetString(2),
                            Password = reader.GetString(3)
                        };
                        entries.Add(entry);
                    }
                }
            }

            return entries;
        }

        public void AddEntry(PasswordEntry entry)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"
                INSERT INTO PasswordEntries (Website, Username, Password)
                VALUES ($website, $username, $password)";
                command.Parameters.AddWithValue("$website", entry.Website);
                command.Parameters.AddWithValue("$username", entry.Username);
                command.Parameters.AddWithValue("$password", entry.Password);

                command.ExecuteNonQuery();
            }
        }

        public void UpdateEntry(PasswordEntry entry)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"
                UPDATE PasswordEntries
                SET Website = $website, Username = $username, Password = $password
                WHERE Id = $id";
                command.Parameters.AddWithValue("$website", entry.Website);
                command.Parameters.AddWithValue("$username", entry.Username);
                command.Parameters.AddWithValue("$password", entry.Password);
                command.Parameters.AddWithValue("$id", entry.Id);

                command.ExecuteNonQuery();
            }
        }

        public void DeleteEntry(int id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM PasswordEntries WHERE Id = $id";
                command.Parameters.AddWithValue("$id", id);

                command.ExecuteNonQuery();
            }
        }
    }
}
