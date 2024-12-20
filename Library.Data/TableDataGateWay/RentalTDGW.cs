using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Library.Data.DataTransferObject;

namespace Library.Data.TableDataGateWay
{
    public class RentalTDGW
    {
        private string connectionString = "";

        public RentalTDGW()
        {
            connectionString = "Data Source=dbsys.cs.vsb.cz\\STUDENT;Initial Catalog=ELL0014;User ID=ELL0014;Password=g56m04zD2hEj30CB;Encrypt=False";
        }

        public List<RentalDTO> GetAllRentals()
        {
            DataTable result = new DataTable();
            string query = "SELECT rental_id, customer_id, book_id, date_rented, date_returned " +
                            "FROM rental";
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

            List<RentalDTO> rentals = new List<RentalDTO>();
            foreach (DataRow row in result.Rows)
            {
                RentalDTO rental = new RentalDTO();
                rental.RentalID = Convert.ToInt32(row["rental_id"]);
                rental.CustomerID = Convert.ToInt32(row["customer_id"]);
                rental.BookID = Convert.ToInt32(row["book_id"]);
                rental.DateRented = row["date_rented"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["date_rented"]);
                rental.DateReturned = row["date_returned"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["date_returned"]);

                rentals.Add(rental);
            }
            return rentals;
        }

        public RentalDTO GetRental(int rentalID)
        {
            DataTable result = new DataTable();
            string query = "SELECT rental_id, customer_id, book_id, ¨date_rented, date_returned " +
                            "FROM rental " +
                            "WHERE rental_id = @rentalID";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@rentalID", rentalID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        result.Load(reader);
                    }
                }
            }

            RentalDTO rental = new RentalDTO();
            rental.RentalID = Convert.ToInt32(result.Rows[0]["rental_id"]);
            rental.CustomerID = Convert.ToInt32(result.Rows[0]["customer_id"]);
            rental.BookID = Convert.ToInt32(result.Rows[0]["book_id"]);
            rental.DateRented = result.Rows[0]["date_rented"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(result.Rows[0]["date_rented"]);
            rental.DateReturned = result.Rows[0]["date_returned"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(result.Rows[0]["date_returned"]);
            return rental;
        }

        public void InsertRental(RentalDTO rental)
        {
            string query = "INSERT INTO rental (customer_id, book_id, date_rented, date_returned) " +
                            "VALUES (@customerID, @bookID, @dateRented, null)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@customerID", rental.CustomerID);
                    command.Parameters.AddWithValue("@bookID", rental.BookID);
                    command.Parameters.AddWithValue("@dateRented", rental.DateRented);
                    //command.Parameters.AddWithValue("@dateReturned", rental.DateReturned);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateRental(RentalDTO rental)
        {
            string query = "UPDATE rental " +
                            "SET customer_id = @customerID, book_id = @bookID, date_rented = @dateRented, date_returned = @dateReturned " +
                            "WHERE rental_id = @rentalID";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@customerID", rental.CustomerID);
                    command.Parameters.AddWithValue("@bookID", rental.BookID);
                    command.Parameters.AddWithValue("@dateRented", rental.DateRented);
                    command.Parameters.AddWithValue("@dateReturned", rental.DateReturned);
                    command.Parameters.AddWithValue("@rentalID", rental.RentalID);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateReturnedDate(int rentalID, DateTime? returnDate)
        {
            string query = "UPDATE rental " +
                            "SET date_returned = @returnDate " +
                            "WHERE rental_id = @rentalID";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@returnDate", returnDate);
                    command.Parameters.AddWithValue("@rentalID", rentalID);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteRental(int rentalID)
        {
            string query = "DELETE FROM rental " +
                            "WHERE rental_id = @rentalID";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@rentalID", rentalID);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
