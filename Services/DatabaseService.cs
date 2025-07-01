using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using SoftAlcoholApp.Models;

namespace SoftAlcoholApp.Services
{
    public class DatabaseService
    {
        private readonly string connectionString;

        public DatabaseService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        // Отримати всі напої
        public List<Drink> GetAllDrinks()
        {
            var drinks = new List<Drink>();

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT id, type, brand, manufacturer, supplier, expiration_date, price FROM Drinks";

                using (var cmd = new MySqlCommand(query, connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        drinks.Add(new Drink
                        {
                            Id = reader.GetInt32("id"),
                            Type = reader.GetString("type"),
                            Brand = reader.GetString("brand"),
                            Manufacturer = reader.GetString("manufacturer"),
                            Supplier = reader.GetString("supplier"),
                            ExpirationDate = reader.GetDateTime("expiration_date"),
                            Price = reader.GetDecimal("price")
                        });
                    }
                }
            }

            return drinks;
        }

        // Додати новий напій
        public void AddDrink(Drink drink)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = @"INSERT INTO Drinks (type, brand, manufacturer, supplier, expiration_date, price) 
                                 VALUES (@type, @brand, @manufacturer, @supplier, @expiration_date, @price)";

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@type", drink.Type);
                    cmd.Parameters.AddWithValue("@brand", drink.Brand);
                    cmd.Parameters.AddWithValue("@manufacturer", drink.Manufacturer);
                    cmd.Parameters.AddWithValue("@supplier", drink.Supplier);
                    cmd.Parameters.AddWithValue("@expiration_date", drink.ExpirationDate);
                    cmd.Parameters.AddWithValue("@price", drink.Price);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Оновити напій
        public void UpdateDrink(Drink drink)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = @"UPDATE Drinks SET
                                 type = @type,
                                 brand = @brand,
                                 manufacturer = @manufacturer,
                                 supplier = @supplier,
                                 expiration_date = @expiration_date,
                                 price = @price
                                 WHERE id = @id";

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@type", drink.Type);
                    cmd.Parameters.AddWithValue("@brand", drink.Brand);
                    cmd.Parameters.AddWithValue("@manufacturer", drink.Manufacturer);
                    cmd.Parameters.AddWithValue("@supplier", drink.Supplier);
                    cmd.Parameters.AddWithValue("@expiration_date", drink.ExpirationDate);
                    cmd.Parameters.AddWithValue("@price", drink.Price);
                    cmd.Parameters.AddWithValue("@id", drink.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Видалити напій за id
        public void DeleteDrink(int id)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "DELETE FROM Drinks WHERE id = @id";

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
