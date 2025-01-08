using CipherShield.Models;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace CipherShield.Helpers
{
    internal class DatabaseHelper
    {
        private readonly string _connectionString;

        // Initialize SQLitePCL to use SQLCipher
        static DatabaseHelper()
        {
            // Initialize SQLitePCL to use SQLCipher
            SQLitePCL.Batteries_V2.Init();
        }

        // Constructor for the database helper
        public DatabaseHelper(string password)
        {
            _connectionString = $"Data Source=encrypted_pwd_database.db;Password={password}";
            InitializeDatabase();
        }

        // Initialize the database
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

        // Get all entries from the database
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

        // Add a new entry to the database
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

        // Update an existing entry in the database
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

        // Delete an entry from the database
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
