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
    public class StaffTDGW
    {
        private string connectionString = "";

        public StaffTDGW()
        {
            connectionString = "Data Source=dbsys.cs.vsb.cz\\STUDENT;Initial Catalog=ELL0014;User ID=ELL0014;Password=g56m04zD2hEj30CB;Encrypt=False";
        }

        public List<StaffDTO> GetAllStaff()
        {
            DataTable result = new DataTable();
            string query = "SELECT staff_id, first_name, last_name, username, password " +
                            "FROM staff";
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

            List<StaffDTO> staffs = new List<StaffDTO>();
            foreach (DataRow row in result.Rows)
            {
                StaffDTO staff = new StaffDTO();
                staff.StaffID = Convert.ToInt32(row["staff_id"]);
                staff.FirstName = row["first_name"].ToString();
                staff.LastName = row["last_name"].ToString();
                staff.Username = row["username"].ToString();
                staff.Password = row["password"].ToString();
                staffs.Add(staff);
            }
            return staffs;
        }

        public StaffDTO GetStaff(int staffID)
        {
            DataTable result = new DataTable();
            string query = "SELECT staff_id, first_name, last_name, username, password " +
                            "FROM staff " +
                            "WHERE staff_id = @staffID";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@staffID", staffID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        result.Load(reader);
                    }
                }
            }

            StaffDTO staff = new StaffDTO();
            foreach (DataRow row in result.Rows)
            {
                staff.StaffID = Convert.ToInt32(row["staff_id"]);
                staff.FirstName = row["first_name"].ToString();
                staff.LastName = row["last_name"].ToString();
                staff.Username = row["username"].ToString();
                staff.Password = row["password"].ToString();
            }
            return staff;
        }

        public StaffDTO GetStaffByUsername(string username)
        {
            DataTable result = new DataTable();
            string query = "SELECT staff_id, first_name, last_name, username, password " +
                            "FROM staff " +
                            "WHERE username = @username";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        result.Load(reader);
                    }
                }
            }

            StaffDTO staff = new StaffDTO();
            foreach (DataRow row in result.Rows)
            {
                staff.StaffID = Convert.ToInt32(row["staff_id"]);
                staff.FirstName = row["first_name"].ToString();
                staff.LastName = row["last_name"].ToString();
                staff.Username = row["username"].ToString();
                staff.Password = row["password"].ToString();
            }
            return staff;
        }

        public void InsertStaff(StaffDTO staff)
        {
            string query = "INSERT INTO staff (first_name, last_name, username, password) " +
                            "VALUES (@firstName, @lastName, @username, @password)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@firstName", staff.FirstName);
                    command.Parameters.AddWithValue("@lastName", staff.LastName);
                    command.Parameters.AddWithValue("@username", staff.Username);
                    command.Parameters.AddWithValue("@password", staff.Password);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateStaff(StaffDTO staff)
        {
            string query = "UPDATE staff " +
                            "SET first_name = @firstName, last_name = @lastName, username = @username, password = @password " +
                            "WHERE staff_id = @staffID";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@staffID", staff.StaffID);
                    command.Parameters.AddWithValue("@firstName", staff.FirstName);
                    command.Parameters.AddWithValue("@lastName", staff.LastName);
                    command.Parameters.AddWithValue("@username", staff.Username);
                    command.Parameters.AddWithValue("@password", staff.Password);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteStaff(int staffID)
        {
            string query = "DELETE FROM staff " +
                            "WHERE staff_id = @staffID";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@staffID", staffID);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
