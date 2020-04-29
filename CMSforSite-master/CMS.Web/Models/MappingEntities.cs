using CMS.Domain;
using CMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CMS.Web.Models
{
   public class MappingEntities
    {
        public string Connection = ConfigurationManager.ConnectionStrings["Dal"].ToString();
        public Categories GetCategoryById(Guid id)
        {
            var category = new Categories();
            string queryString =
            "SELECT *  FROM Categories WHERE ID = @ID";
            using (SqlConnection connection =
          new SqlConnection(Connection))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@ID", id);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                       
                        category.id = new Guid(reader["Id"].ToString());
                        category.Name = reader["Name"].ToString();
                        var album = reader["Album_id"].ToString();
                        if (!string.IsNullOrWhiteSpace(album))
                        {
                            category.Album = GetAlbum(new Guid(album));
                        }
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }

            }
            return category;
        }

        private Albums GetAlbum(Guid id)
        {
            var album = new Albums();
            string queryString =
            "SELECT *  FROM Albums WHERE ID = @ID";
            using (SqlConnection connection =
          new SqlConnection(Connection))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@ID", id);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        album.id = new Guid(reader["Id"].ToString());
                        album.Name = reader["Name"].ToString();

                       
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }

            }
            return album;
        }

        public Posts GetPostsById(Guid id)
        {
            var posts = new Posts();
            string queryString =
            "SELECT *  FROM Posts WHERE ID = @ID";
            using (SqlConnection connection =
          new SqlConnection(Connection))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@ID", id);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        posts.id = new Guid(reader["Id"].ToString());
                        posts.Title = reader["Title"].ToString();
                        var album = reader["Album_id"].ToString();
                        if (!string.IsNullOrWhiteSpace(album))
                            posts.Album = GetAlbum(new Guid(reader["Album_id"].ToString()));
                    

                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }

            }
            return posts;
        }

    }

}