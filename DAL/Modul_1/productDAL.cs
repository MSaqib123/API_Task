using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DML;

namespace DAL.Modul_1
{
    public static class productDAL
    {
        public static clsProduct InsertProduct(clsProduct product)
        {
            using (SqlConnection connection = new SqlConnection(Connection.GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand("spInsertProduct", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Name", product.Name);
                    command.Parameters.AddWithValue("@Price", product.Price);
                    command.Parameters.AddWithValue("@Description", product.Description);
                    command.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                    command.Parameters.AddWithValue("@CreateBy", product.CreateBy);
                    command.Parameters.AddWithValue("@CreateOn", product.CreateOn);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            return product;
        }

        public static clsProduct UpdateProduct(clsProduct product)
        {
            using (SqlConnection connection = new SqlConnection(Connection.GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand("spUpdateProduct", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", product.Id);
                    command.Parameters.AddWithValue("@Name", product.Name);
                    command.Parameters.AddWithValue("@Price", product.Price);
                    command.Parameters.AddWithValue("@Description", product.Description);
                    command.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                    command.Parameters.AddWithValue("@ModifiedBy", product.ModifiedBy);
                    command.Parameters.AddWithValue("@ModifiedOn", product.ModifiedOn);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            return product;
        }

        public static clsProduct GetProductById(int id)
        {
            using (SqlConnection connection = new SqlConnection(Connection.GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand("spGetProductById", connection))
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

        public static List<clsProduct> GetAllProducts()
        {
            List<clsProduct> products = new List<clsProduct>();

            using (SqlConnection connection = new SqlConnection(Connection.GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand("spGetAllProducts", connection))
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

        private static clsProduct MapProductFromDataReader(SqlDataReader reader)
        {
            return new clsProduct
            {
                //Id = (int)reader["Id"],
                //Name = reader["Name"].ToString(),
                //Price = (decimal)reader["Price"],
                //CategoryId = (int)reader["CategoryId"]

                Id = reader.IsDBNull(reader.GetOrdinal("Id")) ? default(int) : (int)reader["Id"],
                Name = reader["Name"].ToString(),
                Price =reader.IsDBNull(reader.GetOrdinal("CreateBy")) ? default(decimal) : (decimal)reader["Price"],
                Description = reader["Description"].ToString(),
                CategoryId = reader.IsDBNull(reader.GetOrdinal("CatId")) ? default(int) : (int)reader["CatId"],
                CreateBy = reader.IsDBNull(reader.GetOrdinal("CreateBy")) ? default(int) : (int)reader["CreateBy"],
                CreateOn = reader.IsDBNull(reader.GetOrdinal("CreateOn")) ? default(DateTime) : (DateTime)reader["CreateOn"],
                ModifiedBy = reader.IsDBNull(reader.GetOrdinal("ModifiedBy")) ? default(int) : (int)reader["ModifiedBy"],
                ModifiedOn = reader.IsDBNull(reader.GetOrdinal("ModifiedOn")) ? default(DateTime) : (DateTime)reader["ModifiedOn"]
            };
        }

    }
}
