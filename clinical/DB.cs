using clinical.BaseClasses;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;


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

                }
            }
        }

        public static void InsertEmployee(Employee employee)
        {

            using (connection)
            {
                connection.Open();

                string query = "INSERT INTO Employee (EmployeeID, Name, DateOfBirth, ContactInfo, Role, NationalID, HireDate) " +
                               "VALUES (@EmployeeID ,@Name, @DateOfBirth, @ContactInfo, @Role, @NationalID, @HireDate)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EmployeeID", employee.EmployeeID);
                    command.Parameters.AddWithValue("@Name", employee.Name);
                    command.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
                    command.Parameters.AddWithValue("@ContactInfo", employee.ContactInfo);
                    command.Parameters.AddWithValue("@Role", employee.Role);
                    command.Parameters.AddWithValue("@NationalID", employee.NationalID);
                    command.Parameters.AddWithValue("@HireDate", employee.HireDate);

                    command.ExecuteNonQuery();
                }
            }
        }

        public static List<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();

            using (connection)
            {
                connection.Open();

                string query = "SELECT * FROM Employee";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Employee employee = new Employee((int)reader["EmployeeID"],
                                reader["Name"].ToString(),
                                (DateTime)reader["DateOfBirth"],
                                reader["ContactInfo"].ToString(),
                                reader["Role"].ToString(),
                                reader["NationalID"].ToString(),
                                (DateTime)reader["HireDate"]);
                            

                            employees.Add(employee);
                        }
                    }
                }
            }

            return employees;
        }

        public static void InsertPhysioTherapist(PhysioTherapist physioTherapist)
        {
            using (connection)
            {
                connection.Open();

                string query = "INSERT INTO PhysioTherapist (PhysioTherapistID, Name, DateOfBirth, ContactInfo, NationalID, HireDate, Address) " +
                               "VALUES (@PhysioTherapistID, @Name, @DateOfBirth, @ContactInfo, @NationalID, @HireDate, @Address)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PhysioTherapistID", physioTherapist.PhysioTherapistID);
                    command.Parameters.AddWithValue("@Name", physioTherapist.Name);
                    command.Parameters.AddWithValue("@DateOfBirth", physioTherapist.DateOfBirth);
                    command.Parameters.AddWithValue("@ContactInfo", physioTherapist.ContactInfo);
                    command.Parameters.AddWithValue("@NationalID", physioTherapist.NationalID);
                    command.Parameters.AddWithValue("@HireDate", physioTherapist.HireDate);
                    command.Parameters.AddWithValue("@Address", physioTherapist.Address);

                    command.ExecuteNonQuery();
                }
            }
        }

        public static List<PhysioTherapist> GetAllPhysioTherapists()
        {
            List<PhysioTherapist> physiotherapists = new List<PhysioTherapist>();

            using (connection)
            {
                connection.Open();

                string query = "SELECT * FROM PhysioTherapist";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PhysioTherapist physiotherapist = new PhysioTherapist(
                                (int)reader["PhysioTherapistID"],
                                reader["Name"].ToString(),
                                (DateTime)reader["DateOfBirth"],
                                reader["ContactInfo"].ToString(),
                                reader["NationalID"].ToString(),
                                (DateTime)reader["HireDate"],
                                reader["Address"].ToString()
                            );

                            physiotherapists.Add(physiotherapist);
                        }
                    }
                }
            }

            return physiotherapists;
        }
    }
}
