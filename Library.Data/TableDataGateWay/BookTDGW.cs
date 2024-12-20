using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Data.SqlClient;
using Library.Data.DataTransferObject;
using System.Data;
using System.Diagnostics;

namespace Library.Data.TableDataGateWay
{
    public class BookTDGW
    {
        private string connectionString = "";

        public BookTDGW() 
        {
            connectionString = "Data Source=dbsys.cs.vsb.cz\\STUDENT;Initial Catalog=ELL0014;User ID=ELL0014;Password=g56m04zD2hEj30CB;Encrypt=False";
        }

        public List<BookDTO> GetAllBooks()
        {
            DataTable result = new DataTable();
            string query = "SELECT book_id, title, author, pages, year, currently_rented " +
                            "FROM book";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        result.Load(reader);
                    }
                }
            }

            List<BookDTO> books = new List<BookDTO>();
            foreach (DataRow row in result.Rows)
            {
                BookDTO book = new BookDTO();
                book.BookID = Convert.ToInt32(row["book_id"]);
                book.Title = row["title"].ToString();
                book.Author = row["author"].ToString();
                book.Pages = Convert.ToInt32(row["pages"]);
                book.Year = Convert.ToInt32(row["year"]);
                book.CurrentlyRented = Convert.ToBoolean(row["currently_rented"]);
                books.Add(book);
            }
            return books;
        }

        public BookDTO GetBook(int bookID)
        {
            DataTable result = new DataTable();
            string query = "SELECT book_id, title, author, pages, year, currently_rented " +
                            "FROM book " +
                            "WHERE book_id = @bookID";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@bookID", bookID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        result.Load(reader);
                    }
                }
            }

            BookDTO book = new BookDTO();
            foreach (DataRow row in result.Rows)
            {
                book.BookID = Convert.ToInt32(row["book_id"]);
                book.Title = row["title"].ToString();
                book.Author = row["author"].ToString();
                book.Pages = Convert.ToInt32(row["pages"]);
                book.Year = Convert.ToInt32(row["year"]);
                book.CurrentlyRented = Convert.ToBoolean(row["currently_rented"]);
            }
            return book;
        }

        public void InsertBook(BookDTO book)
        {
            string query = "INSERT INTO book (title, author, pages, year, currently_rented) " +
                            "VALUES (@title, @author, @pages, @year, @currently_rented)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@title", book.Title);
                    command.Parameters.AddWithValue("@author", book.Author);
                    command.Parameters.AddWithValue("@pages", book.Pages);
                    command.Parameters.AddWithValue("@year", book.Year);
                    if (book.CurrentlyRented)
                    {
                        command.Parameters.AddWithValue("@currently_rented", 1);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@currently_rented", 0);
                    }
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateBook(BookDTO book)
        {
            string query = "UPDATE book " +
                            "SET title = @title, author = @author, pages = @pages, year = @year, currently_rented = @currently_rented " +
                            "WHERE book_id = @bookID";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@bookID", book.BookID);
                    command.Parameters.AddWithValue("@title", book.Title);
                    command.Parameters.AddWithValue("@author", book.Author);
                    command.Parameters.AddWithValue("@pages", book.Pages);
                    command.Parameters.AddWithValue("@year", book.Year);
                    if (book.CurrentlyRented)
                    {
                        command.Parameters.AddWithValue("@currently_rented", 1);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@currently_rented", 0);
                    }
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteBook(int bookID)
        {
            string query = "DELETE FROM book " +
                            "WHERE book_id = @bookID";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@bookID", bookID);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
