using CipherSync.Models;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace CipherSync.Helpers
{
    internal class DatabaseHelper
    {
        private readonly string _connectionString;

        static DatabaseHelper()
        {
            // Initialize SQLitePCL to use SQLCipher
            SQLitePCL.Batteries_V2.Init();
        }

        public DatabaseHelper()
        {
            // Set the connection string with the encryption key
            _connectionString = "Data Source=encrypted_pwd_database.db;Password=ZM!{K^LPmW5gWoYN1o?7T4DTG/-]W>G&+*Y|z5gYj$-vei2ADsFkGl!](>i}xDJ}d!%S!xDTH#J$ZId#q(nh%w3p{)3%Hm^Q/r/njI!{h^fDI^?S6rcXw}8}!p2DV/Q>]j#&zn)OD$VaV]7rVTnq$0#WuO[NoE{!Z>a7zs*#K{7F[MDU+@]H64obouK6!W}P}y6k/r/n(F!K+^p+XQCXQG)R^&a!$K&n]/H%&gEXLmPjqEN%q?I0+Jc!z5=]3rRK}c#Bq@dMtC{63p?FhR#j+Ls#BQt5oNApKXyFh@^%/r/n^[!{g}]B-w(Awc3!rS##qW((-]nlO{4ks#2/3O5[z#Sj?SV@C#hjV5E&96UacfqR#wY0LiOT#FaTLFR>#u@SJSi]o@-K7g5r/r/n#@o[WUCdX%h7#W!yzjU!05KF0m(t)D^05g=rKpO90=!%f$&p%dZZ($^*YU5kR+lj4}GE0OKo!}+gIB(GNi!tGBhg!q!p/--G";
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
