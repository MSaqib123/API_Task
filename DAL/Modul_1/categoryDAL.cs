using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DML;

namespace DAL.Modul_1
{
    public static class categoryDAL
    {
        public static clsCategory InsertCategory(clsCategory category)
        {
            using (SqlConnection connection = new SqlConnection(Connection.GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand("spInsertCategory", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Name", category.Name);
                    command.Parameters.AddWithValue("@Description", category.Description);
                    command.Parameters.AddWithValue("@CreateBy", category.CreateBy);
                    command.Parameters.AddWithValue("@CreateOn", category.CreateOn);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return category;
        }

        public static clsCategory UpdateCategory(clsCategory category)
        {
            using (SqlConnection connection = new SqlConnection(Connection.GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand("spUpdateCategory", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", category.Id);
                    command.Parameters.AddWithValue("@Name", category.Name);
                    command.Parameters.AddWithValue("@Description", category.Description);
                    command.Parameters.AddWithValue("@ModifiedBy", category.ModifiedBy);
                    command.Parameters.AddWithValue("@ModifiedOn", category.ModifiedOn);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            return category;
        }

        public static clsCategory GetCategoryById(int id)
        {
            using (SqlConnection connection = new SqlConnection(Connection.GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand("spGetCategoryById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        return MapProductFromDataReader(reader);
                    }
                    connection.Close();

                    return null;
                }
            }
        }

        public static List<clsCategory> GetAllCategories()
        {
            List<clsCategory> categories = new List<clsCategory>();

            using (SqlConnection connection = new SqlConnection(Connection.GetConnectionString()))
            {
                using (SqlCommand command = new SqlCommand("spGetAllCategories", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        clsCategory category = MapProductFromDataReader(reader);
                        categories.Add(category);
                    }
                }
            }
            return categories;
        }

        private static clsCategory MapProductFromDataReader(SqlDataReader reader)
        {
            var c =  new clsCategory
            {
                //Id = (int)reader["Id"],
                //Name = reader["Name"].ToString(),
                //Description = reader["Description"].ToString(),
                //CreateBy = (int)reader["CreateBy"],
                //CreateOn = (DateTime) reader["CreateOn"],
                //ModifiedBy = (int)reader["ModifiedBy"],
                //ModifiedOn = (DateTime)reader["ModifiedOn"]
                Id = reader.IsDBNull(reader.GetOrdinal("Id")) ? default(int) : (int)reader["Id"],
                Name = reader["Name"].ToString(),
                Description = reader["Description"].ToString(),
                CreateBy = reader.IsDBNull(reader.GetOrdinal("CreateBy")) ? default(int) : (int)reader["CreateBy"],
                CreateOn = reader.IsDBNull(reader.GetOrdinal("CreateOn")) ? default(DateTime) : (DateTime)reader["CreateOn"],
                ModifiedBy = reader.IsDBNull(reader.GetOrdinal("ModifiedBy")) ? default(int) : (int)reader["ModifiedBy"],
                ModifiedOn = reader.IsDBNull(reader.GetOrdinal("ModifiedOn")) ? default(DateTime) : (DateTime)reader["ModifiedOn"]
            };
            return c;
        }

    }
}
