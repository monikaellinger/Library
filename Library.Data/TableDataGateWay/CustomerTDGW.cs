using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;
using Library.Data.DataTransferObject;

namespace Library.Data.TableDataGateWay
{
    public class CustomerTDGW
    {
        private string connectionString = "";

        public CustomerTDGW()
        {
            connectionString = "Data Source=dbsys.cs.vsb.cz\\STUDENT;Initial Catalog=ELL0014;User ID=ELL0014;Password=g56m04zD2hEj30CB;Encrypt=False";
        }

        public List<CustomerDTO> GetAllCustomers()
        {
            DataTable result = new DataTable();
            string query = "SELECT customer_id, first_name, last_name, email " +
                            "FROM customer";
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

            List<CustomerDTO> customers = new List<CustomerDTO>();
            foreach (DataRow row in result.Rows)
            {
                CustomerDTO customer = new CustomerDTO();
                customer.CustomerID = Convert.ToInt32(row["customer_id"]);
                customer.FirstName = row["first_name"].ToString();
                customer.LastName = row["last_name"].ToString();
                customer.Email = row["email"].ToString();
                customers.Add(customer);
            }
            return customers;
        }

        public CustomerDTO GetCustomer(int customerID)
        {
            DataTable result = new DataTable();
            string query = "SELECT customer_id, first_name, last_name, email " +
                            "FROM customer " +
                            "WHERE customer_id = @customerID";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@customerID", customerID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        result.Load(reader);
                    }
                }
            }

            CustomerDTO customer = new CustomerDTO();
            foreach (DataRow row in result.Rows)
            {
                customer.CustomerID = Convert.ToInt32(row["customer_id"]);
                customer.FirstName = row["first_name"].ToString();
                customer.LastName = row["last_name"].ToString();
                customer.Email = row["email"].ToString();
            }
            return customer;
        }

        public void InsertCustomer(CustomerDTO customer)
        {
            string query = "INSERT INTO customer (first_name, last_name, email) " +
                            "VALUES (@firstName, @lastName, @email)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@firstName", customer.FirstName);
                    command.Parameters.AddWithValue("@lastName", customer.LastName);
                    command.Parameters.AddWithValue("@email", customer.Email);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateCustomer(CustomerDTO customer)
        {
            string query = "UPDATE customer " +
                            "SET first_name = @firstName, last_name = @lastName, email = @email " +
                            "WHERE customer_id = @customerID";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@firstName", customer.FirstName);
                    command.Parameters.AddWithValue("@lastName", customer.LastName);
                    command.Parameters.AddWithValue("@email", customer.Email);
                    command.Parameters.AddWithValue("@customerID", customer.CustomerID);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteCustomer(int customerID)
        {
            string query = "DELETE FROM customer " +
                            "WHERE customer_id = @customerID";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@customerID", customerID);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
