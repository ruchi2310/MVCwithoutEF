using Microsoft.Data.SqlClient;
using MVCwithoutEF.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MVCwithoutEF.Data
{
    public class DataAccess
    {
        private readonly string _connectionString;

        // Implement Get single book by Id and Delete Book functionality
        public DataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Book> GetBooks()
        {
            var bookList = new List<Book>();
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {

                var sqlCommand = new SqlCommand("BookViewAll", sqlConnection);
                sqlConnection.Open();
                SqlDataReader rdr = sqlCommand.ExecuteReader();
                while (rdr.Read())
                {
                    var book = new Book();
                    book.Bookid = rdr.GetInt32("Bookid");
                    book.Title = rdr.GetString("Title");
                    book.Author = rdr.GetString("Author");
                    book.Price = rdr.GetInt32("Price");
                    bookList.Add(book);
                }
                sqlConnection.Close();
                return bookList;
            }
        }

        public void AddOrEditBook(Book book)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCmd = new SqlCommand("BookAddorEdit", sqlConnection);
                sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("Bookid", book.Bookid);
                sqlCmd.Parameters.AddWithValue("Title", book.Title);
                sqlCmd.Parameters.AddWithValue("Author", book.Author);
                sqlCmd.Parameters.AddWithValue("Price", book.Price);
                sqlCmd.ExecuteNonQuery();
            }
            
          


            

        }
        public void DeleteByid(int id)
        {
            using(SqlConnection sqlConnection=new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCmd = new SqlCommand("BookDeleteByid", sqlConnection);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("Bookid", id);
                sqlCmd.ExecuteNonQuery();
            }
        }
    }
}
