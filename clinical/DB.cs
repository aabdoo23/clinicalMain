using clinical.BaseClasses;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinical
{
    class DB
    {
        static MySqlConnection connection;
        public DB()
        {
            string connectionString = "server=localhost;database=clinicalbaseclasses;user=root;password=root;";

            // Create MySqlConnection
            using (connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {

                }
            }
        }

        public static void AddAdminToTable(Admin admin)
        {

            // SQL query to insert a new admin
            string query = "INSERT INTO Admin (userID, userName, password, role) VALUES (@userID, @userName, @password, @role)";

            // Create MySqlConnection
            using (connection)
            {
                try
                {
                    connection.Open();

                    // Create MySqlCommand
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Add parameters to the query
                        command.Parameters.AddWithValue("@userID", admin.UserID);
                        command.Parameters.AddWithValue("@userName", admin.UserName);
                        command.Parameters.AddWithValue("@password", admin.Password);
                        command.Parameters.AddWithValue("@role", admin.Role);

                        // Execute the query
                        command.ExecuteNonQuery();

                        Console.WriteLine("Admin added successfully.");
                    }
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that occurred during the database operation
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        
        public static List<Patient> GetAllPatients()
        {
            List<Patient> patients = new List<Patient>();

            using (connection)
            {
                try
                {
                    connection.Open();

                    string query = "SELECT * FROM Patient";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Patient patient = new Patient(
                                    Convert.ToInt32(reader["PatientID"]),
                                    reader["Name"].ToString(),
                                    reader["Address"].ToString(),
                                    Convert.ToDateTime(reader["BirthDate"]),
                                    reader["PhoneNumber"].ToString(),
                                    reader["Gender"].ToString()
                                );

                                patients.Add(patient);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                return patients;
            }

        }

        public static void InsertPatient(Patient patient)
        {

            using (connection)
            {
                connection.Open();

                string insertQuery = "INSERT INTO Patient (PatientID, Name, Address, BirthDate, PhoneNumber, Gender) " +
                                     "VALUES (@PatientID, @Name, @Address, @BirthDate, @PhoneNumber, @Gender)";

                using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@PatientID", patient.PatientID);
                    command.Parameters.AddWithValue("@Name", patient.Name);
                    command.Parameters.AddWithValue("@Address", patient.Address);
                    command.Parameters.AddWithValue("@BirthDate", patient.BirthDate);
                    command.Parameters.AddWithValue("@PhoneNumber", patient.PhoneNumber);
                    command.Parameters.AddWithValue("@Gender", patient.Gender);

                    int rowsAffected = command.ExecuteNonQuery();

                    //if (rowsAffected > 0)
                    //{
                    //    Console.WriteLine("Patient inserted successfully!");
                    //}
                    //else
                    //{
                    //    Console.WriteLine("Failed to insert patient.");
                    //}
                }
            }
        }
    }
}
