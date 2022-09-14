using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Media.Imaging;
using GaleriaOwocow.Windows;
using Microsoft.Data.Sqlite;

namespace GaleriaOwocow
{
    public class DatabaseManager
    {
        private readonly string _dbFilePath;
        private readonly string _connectionString;

        public readonly List<FruitModel> Fruits;

        public DatabaseManager()
        {
            _dbFilePath = Path.Combine(Environment.CurrentDirectory, "database.db");
            _connectionString = $"Data Source={_dbFilePath}";
            Fruits = new List<FruitModel>();
        }

        public void CheckDatabase()
        {
            if (File.Exists(_dbFilePath)) return;

            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "create table if not exists Fruits " +
                                  "(Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT, Color TEXT, Description TEXT, Image BLOB)";
            command.ExecuteNonQuery();
        }

        public void CreateFruit(string name, string color, string description, BitmapImage image)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "insert into Fruits (Name, Color, Description, Image) VALUES($name, $color, $description, $image)";
            command.Parameters.AddWithValue("$name", name);
            command.Parameters.AddWithValue("$color", color);
            command.Parameters.AddWithValue("$description", description);
            command.Parameters.Add(new SqliteParameter
            {
                ParameterName = "$image",
                Value = BitmapToBytes(image),
                DbType = DbType.Binary
            });
            command.ExecuteNonQuery();

            ReloadFruits();
        }

        public void UpdateFruit(FruitModel fruit, BitmapImage image)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "update Fruits set Name = $name, Color = $color, Description = $description, Image = $image where Id = $id";
            command.Parameters.AddWithValue("$id", fruit.Id);
            command.Parameters.AddWithValue("$name", fruit.Name);
            command.Parameters.AddWithValue("$color", fruit.Color);
            command.Parameters.AddWithValue("$description", fruit.Description);
            command.Parameters.Add(new SqliteParameter
            {
                ParameterName = "$image",
                Value = BitmapToBytes(image),
                DbType = DbType.Binary
            });
            command.ExecuteNonQuery();

            ReloadFruits();
        }

        public void DeleteFruit(FruitModel fruit)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "delete from Fruits where Id = $id";
            command.Parameters.AddWithValue("$id", fruit.Id);
            command.ExecuteNonQuery();

            ReloadFruits();
        }

        public void ReloadFruits()
        {
            Fruits.Clear();

            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "select * from Fruits";

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                Fruits.Add(new FruitModel
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = Convert.ToString(reader["Name"]) ?? string.Empty,
                    Color = Convert.ToString(reader["Color"]) ?? string.Empty,
                    Description = Convert.ToString(reader["Description"]) ?? string.Empty,
                    Image = (byte[])reader["Image"]
                });
            }

            MainWindow.RefreshView();
        }

        public static byte[] BitmapToBytes(BitmapImage bitmap)
        {
            var encoder = new BmpBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmap));
            
            using var ms = new MemoryStream();
            encoder.Save(ms);

            return ms.ToArray();
        }

        public static BitmapImage BytesToBitmap(byte[] bytes)
        {
            var image = new BitmapImage();
            using (var stream = new MemoryStream(bytes))
            {
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = stream;
                image.EndInit();
            }
            image.Freeze();

            return image;
        }
    }
}
