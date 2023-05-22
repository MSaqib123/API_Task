using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DML;

namespace DAL.Modul_1
{
    public class productDAL
    {
        public void InsertProduct(clsProduct product)
        {
            using (SqlConnection connection = new SqlConnection(Connection.GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand("spInsertProduct", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Name", product.Name);
                    command.Parameters.AddWithValue("@Price", product.Price);
                    command.Parameters.AddWithValue("@CategoryId", product.CategoryId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateProduct(clsProduct product)
        {
            using (SqlConnection connection = new SqlConnection(Connection.GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand("UpdateProduct", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", product.Id);
                    command.Parameters.AddWithValue("@Name", product.Name);
                    command.Parameters.AddWithValue("@Price", product.Price);
                    command.Parameters.AddWithValue("@CategoryId", product.CategoryId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public clsProduct GetProductById(int id)
        {
            using (SqlConnection connection = new SqlConnection(Connection.GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand("GetProductById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        return MapProductFromDataReader(reader);
                    }

                    return null;
                }
            }
        }

        public List<clsProduct> GetAllProducts()
        {
            List<clsProduct> products = new List<clsProduct>();

            using (SqlConnection connection = new SqlConnection(Connection.GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand("GetAllProducts", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        clsProduct product = MapProductFromDataReader(reader);
                        products.Add(product);
                    }
                }
            }
            return products;
        }

        private clsProduct MapProductFromDataReader(SqlDataReader reader)
        {
            return new clsProduct
            {
                Id = (int)reader["Id"],
                Name = reader["Name"].ToString(),
                Price = (decimal)reader["Price"],
                CategoryId = (int)reader["CategoryId"]
            };
        }

    }
}
