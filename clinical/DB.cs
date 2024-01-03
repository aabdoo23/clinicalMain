using clinical.BaseClasses;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO.Packaging;
using System.Linq;
using System.Windows;
using Package = clinical.BaseClasses.Package;

namespace clinical
{
    class DB
    {
        static MySqlConnection connection;
        public DB()
        {
            string connectionString = "server=localhost;database=clinical;user=root;password=root;";

            using (connection = new MySqlConnection(connectionString))
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connection Message: " + ex.Message);

                }
            }
        }

        /// <User>
        /// ////////////////////////////////////////////////////////////////////////////////////////////////
        /// </User>

        public static void InsertUser(User user)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    using (MySqlCommand cmd = new MySqlCommand("INSERT INTO User (userID, firstName, lastName, gender, hireDate, birthdate, address, phoneNumber, email, nationalID) VALUES (@userID, @firstName, @lastName, @gender, @hireDate, @birthdate, @address, @phoneNumber, @email, @nationalID)", connection))
                    {
                        cmd.Parameters.AddWithValue("@userID", user.UserID);
                        cmd.Parameters.AddWithValue("@firstName", user.FirstName);
                        cmd.Parameters.AddWithValue("@lastName", user.LastName);
                        cmd.Parameters.AddWithValue("@gender", user.Gender == "Male");
                        cmd.Parameters.AddWithValue("@hireDate", user.HireDate);
                        cmd.Parameters.AddWithValue("@birthdate", user.Birthdate);
                        cmd.Parameters.AddWithValue("@address", user.Address);
                        cmd.Parameters.AddWithValue("@phoneNumber", user.PhoneNumber);
                        cmd.Parameters.AddWithValue("@email", user.Email);
                        cmd.Parameters.AddWithValue("@nationalID", user.NationalID);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception e) { }
            }

        }

        public static void DeleteUser(int userID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand("DELETE FROM User WHERE userID = @userID", connection))
                    {
                        cmd.Parameters.AddWithValue("@userID", userID);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public static List<User> GetAllUserswRecordsByDate(DateTime date)
        {
            List<User> users = new List<User>();
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand("SELECT DISTINCT u.* FROM User u JOIN AttendanceRecord ar ON u.userID = ar.userID WHERE DATE(ar.timeStamp) = DATE(@date)", connection))
                    {
                        cmd.Parameters.AddWithValue("@date", date);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                User user = MapUser(reader);

                                users.Add(user);
                            }
                        }

                    }
                }
                catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
            }

            return users;
        }
        public static List<User> GetAllUsers()
        {
            List<User> users = new List<User>();
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM User", connection))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                User user = MapUser(reader);

                                users.Add(user);
                            }
                        }

                    }
                }
                catch (Exception ex) { }
            }

            return users;
        }

        public static User GetUserById(int userID)
        {

            using (connection)
            {
                User user = null;
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM User WHERE userID = @userID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userID", userID);


                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            //MessageBox.Show(reader.Read().ToString());

                            if (reader.Read())
                            {
                                user = MapUser(reader);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return user;

            }

        }

        public static List<User> GetAllEmployees()
        {
            List<User> employees = new List<User>();
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM User WHERE SUBSTRING(userID, 1, 1) = '3'", connection))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                User user = MapUser(reader);
                                employees.Add(user);
                            }
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            }
            return employees;
        }

        public static List<User> GetAllPhysiotherapists()
        {
            List<User> physiotherapists = new List<User>();
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM User WHERE SUBSTRING(userID, 1, 1) = '2'", connection))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                User user = MapUser(reader);
                                physiotherapists.Add(user);
                            }
                        }
                    }
                }
                catch (Exception e) { }
            }
            return physiotherapists;
        }

        public static List<User> GetAllAdmins()
        {
            List<User> admins = new List<User>();
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM User WHERE SUBSTRING(userID, 1, 1) = '2'", connection))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                User user = MapUser(reader);
                                admins.Add(user);
                            }
                        }
                    }
                }
                catch (Exception e) { }
            }

            return admins;
        }


        private static User MapUser(MySqlDataReader reader)
        {
            return new User(
                Convert.ToInt32(reader["userID"]),
                reader["firstName"].ToString(),
                reader["lastName"].ToString(),
                reader["gender"].ToString(),
                Convert.ToDateTime(reader["hireDate"]),
                Convert.ToDateTime(reader["birthdate"]),
                reader["address"].ToString(),
                reader["phoneNumber"].ToString(),
                reader["email"].ToString(),
                reader["nationalID"].ToString()
            );
        }
        
        /// user work days
        ///////////////////////////////////////////////////////        
        ///

        public static List<int> GetUserWorkDaysById(int userID)
        {
            List<int> days=new List<int>();
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT workDay FROM userWorkDayRelation WHERE userID = @userID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userID", userID);


                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int d = Convert.ToInt32(reader);
                                days.Add(d);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return days;

            }
        }

        public static void updateUserWorkDays(int userID, List<int> days)
        {
            DeleteUserWorkDaysById(userID);
            foreach(int i in days)
            {
                InsertUserWorkDay(userID, i);

            }
        }

        public static void DeleteUserWorkDaysById(int userID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "Delete FROM userWorkDayRelation WHERE userID = @userID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userID", userID);

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }


            }
        }

        public static void InsertUserWorkDay(int userId, int day)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    using (MySqlCommand cmd = new MySqlCommand("INSERT INTO userWorkDayRelation (userID, workDay) VALUES (@userID, @day)", connection))
                    {
                        cmd.Parameters.AddWithValue("@userID", userId);
                        cmd.Parameters.AddWithValue("@day", day);
                         cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception e) {
                    MessageBox.Show(e.Message);
                }
            }

        }
        


        ///<Patient>
        /////////////////////////complete?////////////////////////////////
        ///</Patient>

        public static Patient MapPatient(MySqlDataReader reader)
        {
      
            Patient p = new Patient(
                Convert.ToInt32(reader["patientID"]),
                reader["firstName"].ToString(),
                reader["lastName"].ToString(),
                Convert.ToDateTime(reader["birthdate"]),
                reader["gender"].ToString(),
                reader["phoneNumber"].ToString(),
                reader["email"].ToString(),
                reader["address"].ToString(),
                Convert.ToInt32(reader["physicianID"]),
                Convert.ToBoolean(reader["referred"]),
                Convert.ToBoolean(reader["previouslyTreated"]),
                Convert.ToDouble(reader["height"]),
                Convert.ToDouble(reader["weight"]),
                Convert.ToDouble(reader["dueAmount"]),
                reader["referringName"].ToString(),
                reader["referringPN"].ToString(),
                Convert.ToInt32(reader["activePackageID"])

            );
            return p;
        } 
        public static Patient GetPatientById(int patientID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM Patient WHERE patientID = @patientID", connection))
                    {
                        cmd.Parameters.AddWithValue("@patientID", patientID);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                return MapPatient(reader);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (log, throw, etc.)
                    MessageBox.Show(ex.Message);
                }
            }

            return null;
        }
        public static List<Patient> GetAllPatients()
        {
            List<Patient> patients = new List<Patient>();
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM patient", connection))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Patient patient = MapPatient(reader);
                                patients.Add(patient);
                                //MessageBox.Show("done");

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (log, throw, etc.)
                    MessageBox.Show(ex.Message);

                }
            }

            return patients;
        }
        public static List<Patient> GetAllPatientsByPhysicianID(int physicianID)
        {
            List<Patient> patients = new List<Patient>();
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM Patient WHERE userID = @physicianID", connection))
                    {
                        cmd.Parameters.AddWithValue("@physicianID", physicianID);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Patient patient = MapPatient(reader);
                                patients.Add(patient);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (log, throw, etc.)
                    MessageBox.Show(ex.Message);
                }
            }

            return patients;
        }
        public static void DeletePatient(int patientID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand("DELETE FROM Patient WHERE patientID = @patientID", connection))
                    {
                        cmd.Parameters.AddWithValue("@patientID", patientID);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (log, throw, etc.)
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public static void InsertPatient(Patient patient)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand(
                        "INSERT INTO Patient (patientID, firstName, lastName, birthdate, gender, phoneNumber, email, address, referred, previouslyTreated, height, weight, dueAmount, physicianID, referringName, referringPN)" +
                        "VALUES (@patientID, @firstName, @lastName, @birthdate, @gender, @phoneNumber, @email, @address, @referred, @previouslyTreated, @height, @weight, @dueAmount, @physicianID, @referringName, @referringPN)", connection))
                    {
                        cmd.Parameters.AddWithValue("@patientID", patient.PatientID);
                        cmd.Parameters.AddWithValue("@firstName", patient.FirstName);
                        cmd.Parameters.AddWithValue("@lastName", patient.LastName);
                        cmd.Parameters.AddWithValue("@birthdate", patient.Birthdate);
                        cmd.Parameters.AddWithValue("@gender", patient.Gender == "Male");
                        cmd.Parameters.AddWithValue("@phoneNumber", patient.PhoneNumber);
                        cmd.Parameters.AddWithValue("@email", patient.Email);
                        cmd.Parameters.AddWithValue("@address", patient.Address);
                        cmd.Parameters.AddWithValue("@physicianID", patient.PhysicianID);
                        cmd.Parameters.AddWithValue("@referred", patient.Referred == true);
                        cmd.Parameters.AddWithValue("@previouslyTreated", patient.PreviouslyTreated == true);
                        cmd.Parameters.AddWithValue("@height", patient.Height);
                        cmd.Parameters.AddWithValue("@weight", patient.Weight);
                        cmd.Parameters.AddWithValue("@dueAmount", patient.DueAmount);
                        cmd.Parameters.AddWithValue("@referringName", patient.referringName);
                        cmd.Parameters.AddWithValue("@referringPN", patient.referringPN);


                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (log, throw, etc.)
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public static void UpdatePatient(Patient patient)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "UPDATE Patient SET firstName = @firstName, lastName = @lastName, birthdate = @birthdate, gender = @gender, phoneNumber=@phoneNumber," +
                        " email = @email, address = @address, referred = @referred, previouslyTreated = @previouslyTreated" +
                        ", height = @height, weight = @weight, dueAmount = @dueAmount, physicianID = @physicianID, referringName = @referringName, referringPN = @referringPN WHERE patientID = @patientID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@patientID", patient.PatientID);
                        command.Parameters.AddWithValue("@firstName", patient.FirstName);
                        command.Parameters.AddWithValue("@lastName", patient.LastName);
                        command.Parameters.AddWithValue("@birthdate", patient.Birthdate);
                        command.Parameters.AddWithValue("@gender", patient.Gender == "Male");
                        command.Parameters.AddWithValue("@phoneNumber", patient.PhoneNumber);
                        command.Parameters.AddWithValue("@email", patient.Email);
                        command.Parameters.AddWithValue("@address", patient.Address);
                        command.Parameters.AddWithValue("@physicianID", patient.PhysicianID);
                        command.Parameters.AddWithValue("@referred", patient.Referred == true);
                        command.Parameters.AddWithValue("@previouslyTreated", patient.PreviouslyTreated == true);
                        command.Parameters.AddWithValue("@height", patient.Height);
                        command.Parameters.AddWithValue("@weight", patient.Weight);
                        command.Parameters.AddWithValue("@dueAmount", patient.DueAmount);
                        command.Parameters.AddWithValue("@referringName", patient.referringName);
                        command.Parameters.AddWithValue("@referringPN", patient.referringPN);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Updated.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static List<Patient> GetAllPatientsToday()
        {
            List<Patient> patientsToday = new List<Patient>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT DISTINCT PatientID FROM Visit WHERE DATE(TimeStamp) = CURDATE()";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                patientsToday.Add(GetPatientById(Convert.ToInt32(reader["PatientID"])));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }

            return patientsToday;
        }

        public static List<Injury> GetAllInjuriesByPatientID(int patientID)
        {
            List<Injury> injuries = new List<Injury>();

            using (connection)
            {
                connection.Open();

                string query = "SELECT pir.patientID, pir.injuryID, i.injuryID, i.injuryName, i.injuryLocation, i.severity, i.description " +
                               "FROM patientInjuryRelation pir " +
                               "JOIN Injury i ON pir.injuryID = i.injuryID";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Injury injury = new Injury
                            (
                                Convert.ToInt32(reader["injuryID"]),
                                Convert.ToString(reader["injuryName"]),
                                Convert.ToString(reader["injuryLocation"]),
                                Convert.ToInt32(reader["severity"]),
                                Convert.ToString(reader["description"])
                            );

                            injuries.Add(injury);
                        }
                    }
                }
            }

            return injuries;
        

    }
        public static void InsertPatientInjuries(int injuryID, int patientID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand(
                        "INSERT INTO patientInjuryRelation (injuryID, patientID) " +
                        "VALUES (@chronicID, @chronicDiseaseName)",
                        connection))
                    {
                        
                           cmd.Parameters.AddWithValue("@chronicID", injuryID);
                            cmd.Parameters.AddWithValue("@chronicDiseaseName", patientID);

                            cmd.ExecuteNonQuery();
                        

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public static List<ChronicDisease> GetAllChronicDiseasesByPatientID(int patientID)
        {
            List<ChronicDisease> chronicDiseases = new List<ChronicDisease>();

            using (connection)
            {
                connection.Open();

                string query = "SELECT pc.patientID, pc.chronicID, cd.chronicDiseaseID, cd.chronicDiseaseName, cd.description " +
                               "FROM patientChronicRelation pc " +
                               "JOIN ChronicDisease cd ON pc.chronicID = cd.chronicDiseaseID";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ChronicDisease chronicDisease = new ChronicDisease(
                                Convert.ToInt32(reader["chronicDiseaseID"]),
                                Convert.ToString(reader["chronicDiseaseName"]),
                                Convert.ToString(reader["description"])

                            );

                            chronicDiseases.Add(chronicDisease);
                        }
                    }
                }
            }

            return chronicDiseases;
        }
        public static void InsertPatientChronicDiseases(int chronicID, int patientID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand(
                        "INSERT INTO patientChronicRelation (chronicID, patientID) " +
                        "VALUES (@chronicID, @chronicDiseaseName)",
                        connection))
                    {
                        
                            cmd.Parameters.AddWithValue("@chronicID", chronicID);
                            cmd.Parameters.AddWithValue("@chronicDiseaseName", patientID);

                            cmd.ExecuteNonQuery();
                        

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        ///ChronicDisease
        ////////////////////////////////////////////////////////
        ///

        public static ChronicDisease GetChronicDiseaseById(int chronicDiseaseID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM ChronicDisease WHERE chronicDiseaseID = @chronicDiseaseID", connection))
                    {
                        cmd.Parameters.AddWithValue("@chronicDiseaseID", chronicDiseaseID);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapChronicDisease(reader);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (log, throw, etc.)
                    MessageBox.Show(ex.Message);
                }
            }

            return null;
        }

        public static List<ChronicDisease> GetAllChronicDiseases()
        {
            List<ChronicDisease> chronicDiseases = new List<ChronicDisease>();
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM ChronicDisease", connection))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ChronicDisease chronicDisease = MapChronicDisease(reader);
                                chronicDiseases.Add(chronicDisease);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (log, throw, etc.)
                    MessageBox.Show(ex.Message);
                }
            }

            return chronicDiseases;
        }
        public static void DeleteChronicDisease(int chronicDiseaseID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand("DELETE FROM ChronicDisease WHERE chronicDiseaseID = @chronicDiseaseID", connection))
                    {
                        cmd.Parameters.AddWithValue("@chronicDiseaseID", chronicDiseaseID);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (log, throw, etc.)
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public static void DeletePatientsChronicDiseases(int patientID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand("DELETE FROM patientChronicRelation WHERE patientID = @chronicDiseaseID", connection))
                    {
                        cmd.Parameters.AddWithValue("@chronicDiseaseID", patientID);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (log, throw, etc.)
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public static void DeletePatientsInjuries(int patientID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand("DELETE FROM patientInjuryRelation WHERE patientID = @chronicDiseaseID", connection))
                    {
                        cmd.Parameters.AddWithValue("@chronicDiseaseID", patientID);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (log, throw, etc.)
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public static void InsertChronicDisease(ChronicDisease chronicDisease)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand(
                        "INSERT INTO ChronicDisease (chronicDiseaseID, chronicDiseaseName, description) " +
                        "VALUES (@chronicDiseaseID, @chronicDiseaseName, @description)",
                        connection))
                    {
                        cmd.Parameters.AddWithValue("@chronicDiseaseID", chronicDisease.ChronicDiseaseID);
                        cmd.Parameters.AddWithValue("@chronicDiseaseName", chronicDisease.ChronicDiseaseName);
                        cmd.Parameters.AddWithValue("@description", chronicDisease.Description);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (log, throw, etc.)
                    MessageBox.Show(ex.Message);
                }
            }
        }



        internal static void updateChronicDisease(ChronicDisease ch)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand(
                        "UPDATE ChronicDisease SET chronicDiseaseName= @chronicDiseaseName, description=@description WHERE chronicDiseaseID=@chronicDiseaseID",
                    connection))
                    {
                        cmd.Parameters.AddWithValue("@chronicDiseaseID", ch.ChronicDiseaseID);
                        cmd.Parameters.AddWithValue("@chronicDiseaseName", ch.ChronicDiseaseName);
                        cmd.Parameters.AddWithValue("@description", ch.Description);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (log, throw, etc.)
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private static ChronicDisease MapChronicDisease(MySqlDataReader reader)
        {
            return new ChronicDisease(
                reader.GetInt32("chronicDiseaseID"),
                reader.GetString("chronicDiseaseName"),
                reader.IsDBNull("description") ? null : reader.GetString("description")
            );
        }



        /// exercise
        ///////////////////////////////////////////////////////
        ///

        public static Exercise GetExerciseById(int exerciseID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM Exercise WHERE exerciseID = @exerciseID", connection))
                    {
                        cmd.Parameters.AddWithValue("@exerciseID", exerciseID);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapExercise(reader);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (log, throw, etc.)
                    MessageBox.Show(ex.Message);
                }
            }

            return null;
        }

        public static List<Exercise> GetAllExercises()
        {
            List<Exercise> exercises = new List<Exercise>();
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM Exercise", connection))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Exercise exercise = MapExercise(reader);
                                exercises.Add(exercise);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (log, throw, etc.)
                    MessageBox.Show(ex.Message);
                }
            }

            return exercises;
        }

        public static void DeleteExercise(int exerciseID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand("DELETE FROM Exercise WHERE exerciseID = @exerciseID", connection))
                    {
                        cmd.Parameters.AddWithValue("@exerciseID", exerciseID);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (log, throw, etc.)
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public static void InsertExercise(Exercise exercise)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand(
                        "INSERT INTO Exercise (exerciseID, exerciseName, explanationLink, notes) " +
                        "VALUES (@exerciseID, @exerciseName, @explanationLink, @notes)",
                        connection))
                    {
                        cmd.Parameters.AddWithValue("@exerciseID", exercise.ExerciseID);
                        cmd.Parameters.AddWithValue("@exerciseName", exercise.ExerciseName);
                        cmd.Parameters.AddWithValue("@explanationLink", exercise.ExplanationLink);
                        cmd.Parameters.AddWithValue("@notes", exercise.Notes);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (log, throw, etc.)
                    MessageBox.Show(ex.Message);
                }
            }
        }


        public static void UpdateExercise(Exercise exercise)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand(
                        "UPDATE Exercise SET exerciseName=@exerciseName, explanationLink = @explanationLink, notes=@notes WHERE exerciseID=@exerciseID " ,connection))
                    {
                        cmd.Parameters.AddWithValue("@exerciseID", exercise.ExerciseID);
                        cmd.Parameters.AddWithValue("@exerciseName", exercise.ExerciseName);
                        cmd.Parameters.AddWithValue("@explanationLink", exercise.ExplanationLink);
                        cmd.Parameters.AddWithValue("@notes", exercise.Notes);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (log, throw, etc.)
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private static Exercise MapExercise(MySqlDataReader reader)
        {
            return new Exercise(
                reader.GetInt32("exerciseID"),
                reader.GetString("exerciseName"),
                reader.IsDBNull("explanationLink") ? null : reader.GetString("explanationLink"),
                reader.IsDBNull("notes") ? null : reader.GetString("notes")
            );
        }

        ///treatment plan
        ////////////////////////////////////////////////////////////
        ///


        public static void InsertTreatmentPlan(TreatmentPlan treatmentPlan)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "INSERT INTO TreatmentPlan (planID, planName, planTime, injuryID, price, notes) " +
                                   "VALUES (@planID, @planName, @planTime, @injuryID, @price, @notes)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@planID", treatmentPlan.PlanID);
                        command.Parameters.AddWithValue("@planName", treatmentPlan.PlanName);
                        command.Parameters.AddWithValue("@planTime", treatmentPlan.PlanTimeInWeeks);
                        command.Parameters.AddWithValue("@injuryID", treatmentPlan.InjuryID);
                        command.Parameters.AddWithValue("@price", treatmentPlan.Price);
                        command.Parameters.AddWithValue("@notes", treatmentPlan.Notes);

                        command.ExecuteNonQuery();

                        MessageBox.Show("TreatmentPlan inserted successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static void UpdateTreatmentPlan(TreatmentPlan treatmentPlan)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "UPDATE TreatmentPlan SET planName = @planName, planTime = @planTime, " +
                                   "injuryID = @injuryID, price = @price, notes = @notes " +
                                   "WHERE planID = @planID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@planID", treatmentPlan.PlanID);
                        command.Parameters.AddWithValue("@planName", treatmentPlan.PlanName);
                        command.Parameters.AddWithValue("@planTime", treatmentPlan.PlanTimeInWeeks);
                        command.Parameters.AddWithValue("@injuryID", treatmentPlan.InjuryID);
                        command.Parameters.AddWithValue("@price", treatmentPlan.Price);
                        command.Parameters.AddWithValue("@notes", treatmentPlan.Notes);

                        command.ExecuteNonQuery();

                        MessageBox.Show("TreatmentPlan updated successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static void DeleteTreatmentPlan(int planID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "DELETE FROM TreatmentPlan WHERE planID = @planID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@planID", planID);

                        command.ExecuteNonQuery();

                        MessageBox.Show("TreatmentPlan deleted successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static TreatmentPlan GetTreatmentPlanByID(int planID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM TreatmentPlan WHERE planID = @planID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@planID", planID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new TreatmentPlan(
                                    Convert.ToInt32(reader["planID"]),
                                    reader["planName"].ToString(),
                                    Convert.ToInt32(reader["planTime"]),
                                    Convert.ToDouble(reader["price"]),
                                    reader["notes"].ToString(),
                                    Convert.ToInt32(reader["injuryID"])
                                );
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return null;
            }
        }

        public static List<TreatmentPlan> GetAllTreatmentPlans()
        {
            List<TreatmentPlan> treatmentPlans = new List<TreatmentPlan>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM TreatmentPlan";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TreatmentPlan treatmentPlan = new TreatmentPlan(
                                    Convert.ToInt32(reader["planID"]),
                                    reader["planName"].ToString(),
                                    Convert.ToInt32(reader["planTime"]),
                                    Convert.ToDouble(reader["price"]),
                                    reader["notes"].ToString(),
                                    Convert.ToInt32(reader["injuryID"])
                                );

                                treatmentPlans.Add(treatmentPlan);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return treatmentPlans;
            }
        }


        /// chatRoom
        ////////////////////////////////////////////////////////////////
        ///
        public static void InsertChatRoom(ChatRoom chatRoom)
        {
            using (connection)
            {
                try
                {
                    if (GetChatRoomByID(chatRoom.ChatRoomID) != null) { return; }
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "INSERT INTO ChatRoom (chatRoomID, firstUserID, secondUserID, chatRoomName,LastVisit) " +
                                   "VALUES (@chatRoomID, @firstUserID, @secondUserID, @chatRoomName,@lastvisit)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@chatRoomID", chatRoom.ChatRoomID);
                        command.Parameters.AddWithValue("@firstUserID", chatRoom.FirstUserID);
                        command.Parameters.AddWithValue("@secondUserID", chatRoom.SecondUserID);
                        command.Parameters.AddWithValue("@chatRoomName", chatRoom.ChatRoomName);
                        command.Parameters.AddWithValue("@lastvisit", chatRoom.LastVisit);

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Insertion error, " + ex.Message);
                }
            }
        }

        public static void DeleteChatRoom(int chatRoomID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "DELETE FROM ChatRoom WHERE chatRoomID = @chatRoomID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@chatRoomID", chatRoomID);

                        command.ExecuteNonQuery();

                        MessageBox.Show("ChatRoom deleted successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static void UpdateLastVisitChatRoom(int chatRoomID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "UPDATE chatRoom SET LastVisit = @LastVisit WHERE chatRoomID = @chatRoomID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@chatRoomID", chatRoomID);
                        command.Parameters.AddWithValue("@LastVisit", DateTime.Now);

                        command.ExecuteNonQuery();

                        //MessageBox.Show("ChatRoom deleted successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }


        public static ChatRoom GetChatRoomByID(int chatRoomID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM ChatRoom WHERE chatRoomID = @chatRoomID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@chatRoomID", chatRoomID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new ChatRoom(
                                    Convert.ToInt32(reader["chatRoomID"]),
                                    Convert.ToInt32(reader["firstUserID"]),
                                    Convert.ToInt32(reader["secondUserID"]),
                                    reader["chatRoomName"].ToString(),
                                    Convert.ToDateTime(reader["LastVisit"])

                                );
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return null;
            }
        }

        public static List<ChatRoom> GetAllChatRooms()
        {
            List<ChatRoom> chatRooms = new List<ChatRoom>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM ChatRoom";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ChatRoom chatRoom = new ChatRoom(
                                    Convert.ToInt32(reader["chatRoomID"]),
                                    Convert.ToInt32(reader["firstUserID"]),
                                    Convert.ToInt32(reader["secondUserID"]),
                                    reader["chatRoomName"].ToString(),
                                    Convert.ToDateTime(reader["LastVisit"])

                                );

                                chatRooms.Add(chatRoom);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return chatRooms;
            }
        }

        public static List<ChatRoom> GetChatRoomsByUserID(int userID)
        {
            List<ChatRoom> chatRooms = new List<ChatRoom>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM ChatRoom WHERE firstUserID = @userID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userID", userID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ChatRoom chatRoom = new ChatRoom(
                                    Convert.ToInt32(reader["chatRoomID"]),
                                    Convert.ToInt32(reader["firstUserID"]),
                                    Convert.ToInt32(reader["secondUserID"]),
                                    reader["chatRoomName"].ToString(),
                                    Convert.ToDateTime(reader["LastVisit"])

                                );

                                chatRooms.Add(chatRoom);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return chatRooms;
            }
        }

        /// chatMessage
        ////////////////////////////////////////////////////////////////
        ///

        public static void InsertChatMessage(ChatMessage chatMessage)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "INSERT INTO ChatMessage (messageID, senderID, chatRoomID, messageContent, timeStamp) " +
                                   "VALUES (@messageID, @senderID, @chatRoomID, @messageContent, @timeStamp)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@messageID", chatMessage.MessageID);
                        command.Parameters.AddWithValue("@senderID", chatMessage.SenderID);
                        command.Parameters.AddWithValue("@chatRoomID", chatMessage.ChatRoomID);
                        command.Parameters.AddWithValue("@messageContent", chatMessage.MessageContent);
                        command.Parameters.AddWithValue("@timeStamp", chatMessage.TimeStamp);

                        command.ExecuteNonQuery();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static void DeleteChatMessage(int messageID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "DELETE FROM ChatMessage WHERE messageID = @messageID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@messageID", messageID);

                        command.ExecuteNonQuery();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static ChatMessage GetChatMessageByID(int messageID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM ChatMessage WHERE messageID = @messageID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@messageID", messageID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new ChatMessage(
                                    Convert.ToInt32(reader["messageID"]),
                                    Convert.ToInt32(reader["senderID"]),
                                    Convert.ToInt32(reader["chatRoomID"]),
                                    reader["messageContent"].ToString(),
                                    Convert.ToDateTime(reader["timeStamp"])
                                );
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return null;
            }
        }

        //public static List<ChatMessage> GetAllChatMessages()
        //{
        //    List<ChatMessage> chatMessages = new List<ChatMessage>();

        //    using (connection)
        //    {
        //        try
        //        {
        //            if (connection.State == ConnectionState.Closed) connection.Open();

        //            string query = "SELECT * FROM ChatMessage";

        //            using (MySqlCommand command = new MySqlCommand(query, connection))
        //            {
        //                using (MySqlDataReader reader = command.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        ChatMessage chatMessage = new ChatMessage(
        //                            Convert.ToInt32(reader["messageID"]),
        //                            Convert.ToInt32(reader["senderID"]),
        //                            Convert.ToInt32(reader["chatRoomID"]),
        //                            reader["messageContent"].ToString(),
        //                            Convert.ToDateTime(reader["timeStamp"])
        //                        );

        //                        chatMessages.Add(chatMessage);
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show($"Error: {ex.Message}");
        //        }

        //        return chatMessages;
        //    }
        //}

        public static List<ChatMessage> GetAllChatMessagesByRoomID(int chatRoomID)
        {
            List<ChatMessage> chatMessages = new List<ChatMessage>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM ChatMessage WHERE chatRoomID = @chatRoomID ORDER BY timeStamp ASC;";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@chatRoomID", chatRoomID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ChatMessage chatMessage = new ChatMessage(
                                    Convert.ToInt32(reader["messageID"]),
                                    Convert.ToInt32(reader["senderID"]),
                                    Convert.ToInt32(reader["chatRoomID"]),
                                    reader["messageContent"].ToString(),
                                    Convert.ToDateTime(reader["timeStamp"])
                                );

                                chatMessages.Add(chatMessage);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return chatMessages;
            }
        }

        public static List<ChatMessage> GetAllUnreadMessagesByRoomID(int chatRoomID)
        {
            List<ChatMessage> chatMessages = new List<ChatMessage>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                     string query = "SELECT cm.* FROM ChatMessage cm JOIN chatRoom cr ON cm.chatroomID = cr.chatroomID WHERE cm.timeStamp >= NOW() - INTERVAL 0.7 SECOND AND cr.chatroomID = @chatRoomID ORDER BY timeStamp ASC;";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@chatRoomID", chatRoomID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ChatMessage chatMessage = new ChatMessage(
                                    Convert.ToInt32(reader["messageID"]),
                                    Convert.ToInt32(reader["senderID"]),
                                    Convert.ToInt32(reader["chatRoomID"]),
                                    reader["messageContent"].ToString(),
                                    Convert.ToDateTime(reader["timeStamp"])
                                );

                                chatMessages.Add(chatMessage);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return chatMessages;
            }
        }


        /// chatGroup
        ////////////////////////////////////////////////////////////////////
        ///

        public static void InsertChatGroup(ChatGroup chatGroup)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "INSERT INTO ChatGroup (chatGroupID, usersIDs, chatGroupName) " +
                                   "VALUES (@chatGroupID, @usersIDs, @chatGroupName)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@chatGroupID", chatGroup.ChatGroupID);
                        command.Parameters.AddWithValue("@usersIDs", string.Join(",", chatGroup.UsersIDs));
                        command.Parameters.AddWithValue("@chatGroupName", chatGroup.ChatGroupName);

                        command.ExecuteNonQuery();

                        MessageBox.Show("ChatGroup inserted successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static void DeleteChatGroup(int chatGroupID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "DELETE FROM ChatGroup WHERE chatGroupID = @chatGroupID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@chatGroupID", chatGroupID);

                        command.ExecuteNonQuery();

                        MessageBox.Show("ChatGroup deleted successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static ChatGroup GetChatGroupByID(int chatGroupID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM ChatGroup WHERE chatGroupID = @chatGroupID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@chatGroupID", chatGroupID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                List<int> usersIDs = reader["usersIDs"].ToString().Split(',').Select(int.Parse).ToList();

                                return new ChatGroup(
                                    Convert.ToInt32(reader["chatGroupID"]),
                                    usersIDs,
                                    reader["chatGroupName"].ToString()
                                );
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return null;
            }
        }

        public static List<ChatGroup> GetAllChatGroups()
        {
            List<ChatGroup> chatGroups = new List<ChatGroup>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM ChatGroup";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                List<int> usersIDs = reader["usersIDs"].ToString().Split(',').Select(int.Parse).ToList();

                                ChatGroup chatGroup = new ChatGroup(
                                    Convert.ToInt32(reader["chatGroupID"]),
                                    usersIDs,
                                    reader["chatGroupName"].ToString()
                                );

                                chatGroups.Add(chatGroup);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return chatGroups;
            }
        }

        public List<ChatGroup> GetChatGroupsByUserID(int userID)
        {
            List<ChatGroup> chatGroups = new List<ChatGroup>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT c.* FROM ChatGroup c " +
                                   "JOIN ChatGroupRelation cr ON c.chatGroupID = cr.chatGroupID " +
                                   "WHERE cr.userID = @userID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userID", userID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                List<int> usersIDs = reader["usersIDs"].ToString().Split(',').Select(int.Parse).ToList();

                                ChatGroup chatGroup = new ChatGroup(
                                    Convert.ToInt32(reader["chatGroupID"]),
                                    usersIDs,
                                    reader["chatGroupName"].ToString()
                                );

                                chatGroups.Add(chatGroup);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return chatGroups;
            }
        }


        /// packages
        /////////////////////////////////////////////////////////////////////////
        ///
        public static List<Package> GetAllPackages()
        {
            List<Package> packages = new List<Package>();

            using (connection)
            {
                connection.Open();

                string query = "SELECT * FROM Package";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Package package = new Package(
                                Convert.ToInt32(reader["PackageID"]),
                                Convert.ToString(reader["PackageName"]),
                                Convert.ToInt32(reader["NumberOfSessions"]),
                                Convert.ToDouble(reader["Price"]),
                                Convert.ToString(reader["Description"])
                                );

                            packages.Add(package);
                        }
                    }
                }
            }

            return packages;
        }

        public static Package GetPackageById(int packageId)
        {
            using (connection)
            {
                connection.Open();

                string query = "SELECT * FROM Package WHERE PackageID = @PackageID";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@PackageID", packageId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Package(
                                Convert.ToInt32(reader["PackageID"]),
                                Convert.ToString(reader["PackageName"]),
                                Convert.ToInt32(reader["NumberOfSessions"]),
                                Convert.ToDouble(reader["Price"]),
                                Convert.ToString(reader["Description"])
                            );
                        }
                    }
                }
            }

            return null; // Package not found
        }

        public static void InsertPackage(BaseClasses.Package package)
        {
            using (connection)
            {
                connection.Open();

                string query = "INSERT INTO Package (PackageID, PackageName, NumberOfSessions, Price, Description) " +
                               "VALUES (@PackageID, @PackageName, @NumberOfSessions, @Price, @Description)";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@PackageID", package.PackageID);
                    cmd.Parameters.AddWithValue("@PackageName", package.PackageName);
                    cmd.Parameters.AddWithValue("@NumberOfSessions", package.NumberOfSessions);
                    cmd.Parameters.AddWithValue("@Price", package.Price);
                    cmd.Parameters.AddWithValue("@Description", package.Description);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void UpdatePackage(BaseClasses.Package package)
        {
            using (connection)
            {
                connection.Open();

                string query = "UPDATE Package SET PackageName=@PackageName,NumberOfSessions=@NumberOfSessions,Price=@Price, Description=@Description  WHERE PackageID=@PackageID ";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@PackageID", package.PackageID);
                    cmd.Parameters.AddWithValue("@PackageName", package.PackageName);
                    cmd.Parameters.AddWithValue("@NumberOfSessions", package.NumberOfSessions);
                    cmd.Parameters.AddWithValue("@Price", package.Price);
                    cmd.Parameters.AddWithValue("@Description", package.Description);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        public static void DeletePackage(int packageId)
        {
            using (connection)
            {
                connection.Open();

                string query = "DELETE FROM Package WHERE PackageID = @PackageID";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@PackageID", packageId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// drug
        /////////////////////////////////////////////////////////////////////////
        ///
        public static void InsertDrug(Drug drug)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "INSERT INTO Drug (drugName, normalDosage, notes) " +
                                   "VALUES (@drugName, @normalDosage, @notes)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@drugName", drug.DrugName);
                        command.Parameters.AddWithValue("@normalDosage", drug.NormalDosage);
                        command.Parameters.AddWithValue("@notes", drug.Notes);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Drug inserted successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static void UpdateDrug(Drug drug)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "UPDATE Drug SET drugName = @drugName, normalDosage = @normalDosage, notes = @notes " +
                                   "WHERE drugID = @drugID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@drugID", drug.DrugID);
                        command.Parameters.AddWithValue("@drugName", drug.DrugName);
                        command.Parameters.AddWithValue("@normalDosage", drug.NormalDosage);
                        command.Parameters.AddWithValue("@notes", drug.Notes);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Drug updated successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static void DeleteDrug(int drugID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "DELETE FROM Drug WHERE drugID = @drugID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@drugID", drugID);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Drug deleted successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static Drug GetDrugByID(int drugID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM Drug WHERE drugID = @drugID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@drugID", drugID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Drug(
                                    Convert.ToInt32(reader["drugID"]),
                                    reader["drugName"].ToString(),
                                    reader["normalDosage"].ToString(),
                                    reader["notes"].ToString()
                                );
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return null;
            }
        }

        public static List<Drug> GetAllDrugs()
        {
            List<Drug> drugs = new List<Drug>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM Drug";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Drug drug = new Drug(
                                    Convert.ToInt32(reader["drugID"]),
                                    reader["drugName"].ToString(),
                                    reader["normalDosage"].ToString(),
                                    reader["notes"].ToString()
                                );

                                drugs.Add(drug);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return drugs;
            }
        }

        /// injury
        /////////////////////////////////////////////////////////////////////////
        ///
        public static void InsertInjury(Injury injury)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "INSERT INTO Injury (injuryID, injuryName, injuryLocation, severity) " +
                                   "VALUES (@injuryID, @injuryName, @injuryLocation, @severity)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@injuryID", injury.InjuryID);
                        command.Parameters.AddWithValue("@injuryName", injury.InjuryName);
                        command.Parameters.AddWithValue("@injuryLocation", injury.InjuryLocation);
                        command.Parameters.AddWithValue("@severity", injury.Severity);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Injury inserted successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static void UpdateInjury(Injury injury)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "UPDATE Injury SET injuryName = @injuryName, " +
                                   "injuryLocation = @injuryLocation, severity = @severity " +
                                   "WHERE injuryID = @injuryID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@injuryID", injury.InjuryID);
                        command.Parameters.AddWithValue("@injuryName", injury.InjuryName);
                        command.Parameters.AddWithValue("@injuryLocation", injury.InjuryLocation);
                        command.Parameters.AddWithValue("@severity", injury.Severity);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Injury updated successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static void DeleteInjury(int injuryID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "DELETE FROM Injury WHERE injuryID = @injuryID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@injuryID", injuryID);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Injury deleted successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static Injury GetInjuryByID(int injuryID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM Injury WHERE injuryID = @injuryID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@injuryID", injuryID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Injury(
                                    Convert.ToInt32(reader["injuryID"]),
                                    reader["injuryName"].ToString(),
                                    reader["injuryLocation"].ToString(),
                                    Convert.ToInt32(reader["severity"]),
                                    reader["description"].ToString()

                                );
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return null;
            }
        }

        public static List<Injury> GetAllInjuries()
        {
            List<Injury> injuries = new List<Injury>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM Injury";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Injury injury = new Injury(
                                    Convert.ToInt32(reader["injuryID"]),
                                    reader["injuryName"].ToString(),
                                    reader["injuryLocation"].ToString(),
                                    Convert.ToInt32(reader["severity"]),
                                    reader["description"].ToString()

                                );

                                injuries.Add(injury);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return injuries;
            }
        }


        ///equipment
        ////////////////////////////////////////////////////////////////////////////
        ///
        public static void InsertEquipment(Equipment equipment)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "INSERT INTO Equipment (equipmentID, equipmentName, equipmentFunction, latestMaintenanceDate, toCheck, roomID) " +
                                   "VALUES (@equipmentID, @equipmentName, @equipmentFunction, @latestMaintenanceDate, @toCheck, @roomID)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@equipmentID", equipment.EquipmentID);
                        command.Parameters.AddWithValue("@equipmentName", equipment.EquipmentName);
                        command.Parameters.AddWithValue("@equipmentFunction", equipment.Function);
                        command.Parameters.AddWithValue("@latestMaintenanceDate", equipment.LatestMaintenanceDate);
                        command.Parameters.AddWithValue("@toCheck", equipment.ToCheck);
                        command.Parameters.AddWithValue("@roomID", equipment.RoomID);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Equipment inserted successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static void UpdateEquipment(Equipment equipment)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "UPDATE Equipment SET equipmentName = @equipmentName, equipmentFunction = @equipmentFunction, " +
                                   "latestMaintenanceDate = @latestMaintenanceDate, toCheck = @toCheck, roomID = @roomID " +
                                   "WHERE equipmentID = @equipmentID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@equipmentID", equipment.EquipmentID);
                        command.Parameters.AddWithValue("@equipmentName", equipment.EquipmentName);
                        command.Parameters.AddWithValue("@equipmentFunction", equipment.Function);
                        command.Parameters.AddWithValue("@latestMaintenanceDate", equipment.LatestMaintenanceDate);
                        command.Parameters.AddWithValue("@toCheck", equipment.ToCheck);
                        command.Parameters.AddWithValue("@roomID", equipment.RoomID);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Equipment updated successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static void DeleteEquipment(int equipmentID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "DELETE FROM Equipment WHERE equipmentID = @equipmentID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@equipmentID", equipmentID);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Equipment deleted successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static Equipment GetEquipmentByID(int equipmentID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM Equipment WHERE equipmentID = @equipmentID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@equipmentID", equipmentID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Equipment(
                                    Convert.ToInt32(reader["equipmentID"]),
                                    reader["equipmentName"].ToString(),
                                    reader["equipmentFunction"].ToString(),
                                    Convert.ToDateTime(reader["latestMaintenanceDate"]),
                                    Convert.ToBoolean(reader["toCheck"]),
                                    Convert.ToInt32(reader["roomID"])
                                );
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return null;
            }
        }

        public static List<Equipment> GetAllEquipment()
        {
            List<Equipment> equipmentList = new List<Equipment>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM Equipment";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Equipment equipment = new Equipment(
                                    Convert.ToInt32(reader["equipmentID"]),
                                    reader["equipmentName"].ToString(),
                                    reader["equipmentFunction"].ToString(),
                                    Convert.ToDateTime(reader["latestMaintenanceDate"]),
                                    Convert.ToBoolean(reader["toCheck"]),
                                    Convert.ToInt32(reader["roomID"])
                                );

                                equipmentList.Add(equipment);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return equipmentList;
            }
        }


        /// room 
        /////////////////////////////////////////////////////////////////////
        ///
        public static void InsertRoom(Room room)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "INSERT INTO Room (roomID, roomNumber) VALUES (@roomID, @roomNumber)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@roomID", room.RoomID);
                        command.Parameters.AddWithValue("@roomNumber", room.RoomNumber);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Room inserted successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static void UpdateRoom(Room room)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "UPDATE Room SET roomNumber = @roomNumber WHERE roomID = @roomID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@roomID", room.RoomID);
                        command.Parameters.AddWithValue("@roomNumber", room.RoomNumber);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Room updated successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static void DeleteRoom(int roomID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "DELETE FROM Room WHERE roomID = @roomID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@roomID", roomID);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Room deleted successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static Room GetRoomByID(int roomID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM Room WHERE roomID = @roomID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@roomID", roomID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Room(
                                    Convert.ToInt32(reader["roomID"]),
                                    reader["roomNumber"].ToString());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return null;
            }
        }

        public static List<Room> GetAllRooms()
        {
            List<Room> roomList = new List<Room>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM Room";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Room room = new Room(
                                    Convert.ToInt32(reader["roomID"]),
                                    reader["roomNumber"].ToString()
                                );

                                roomList.Add(room);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return roomList;
            }
        }
        public static List<Equipment> GetEquipmentByRoomID(int roomID)
        {
            List<Equipment> equipmentList = new List<Equipment>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM Equipment WHERE roomID = @roomID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@roomID", roomID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Equipment equipment = new Equipment(
                                    Convert.ToInt32(reader["equipmentID"]),
                                    reader["equipmentName"].ToString(),
                                    reader["equipmentFunction"].ToString(),
                                    Convert.ToDateTime(reader["latestMaintenanceDate"]),
                                    Convert.ToBoolean(reader["toCheck"]),
                                    Convert.ToInt32(reader["roomID"])
                                );

                                equipmentList.Add(equipment);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }

            return equipmentList;
        }

        /// attendanceRecord
        /////////////////////////////////////////////////////////////////////
        ///
        public static void InsertAttendanceRecord(AttendanceRecord attendanceRecord)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "INSERT INTO AttendanceRecord (recordID, timeStamp, userID, present) " +
                                   "VALUES (@recordID, @timeStamp, @userID, @isPresent)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@recordID", attendanceRecord.RecordID);
                        command.Parameters.AddWithValue("@timeStamp", attendanceRecord.TimeStamp);
                        command.Parameters.AddWithValue("@userID", attendanceRecord.UserID);
                        command.Parameters.AddWithValue("@isPresent", attendanceRecord.IsPresent);

                        command.ExecuteNonQuery();

                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        public static void UpdateAttendanceRecord(AttendanceRecord attendanceRecord)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "UPDATE AttendanceRecord SET timeStamp = @timeStamp, " +
                                   "userID = @userID, present = @isPresent WHERE recordID = @recordID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@recordID", attendanceRecord.RecordID);
                        command.Parameters.AddWithValue("@timeStamp", attendanceRecord.TimeStamp);
                        command.Parameters.AddWithValue("@userID", attendanceRecord.UserID);
                        command.Parameters.AddWithValue("@isPresent", attendanceRecord.IsPresent);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Attendance record updated successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static void DeleteAttendanceRecord(int recordID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "DELETE FROM AttendanceRecord WHERE recordID = @recordID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@recordID", recordID);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Attendance record deleted successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static AttendanceRecord GetAttendanceRecordByID(int recordID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM AttendanceRecord WHERE recordID = @recordID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@recordID", recordID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new AttendanceRecord(
                                    Convert.ToInt32(reader["recordID"]),
                                    Convert.ToDateTime(reader["timeStamp"]),
                                    Convert.ToInt32(reader["userID"]),
                                    Convert.ToBoolean(reader["present"])
                                );
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return null;
            }
        }

        public static List<AttendanceRecord> GetAllAttendanceRecords()
        {
            List<AttendanceRecord> attendanceRecords = new List<AttendanceRecord>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM AttendanceRecord";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AttendanceRecord attendanceRecord = new AttendanceRecord(
                                    Convert.ToInt32(reader["recordID"]),
                                    Convert.ToDateTime(reader["timeStamp"]),
                                    Convert.ToInt32(reader["userID"]),
                                    Convert.ToBoolean(reader["present"])
                                );

                                attendanceRecords.Add(attendanceRecord);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return attendanceRecords;
            }
        }

        public static List<AttendanceRecord> GetAttendanceRecordsByDate(DateTime date)
        {
            List<AttendanceRecord> attendanceRecords = new List<AttendanceRecord>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM AttendanceRecord WHERE DATE(timeStamp) = DATE(@date)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@date", date);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AttendanceRecord attendanceRecord = new AttendanceRecord(
                                    Convert.ToInt32(reader["recordID"]),
                                    Convert.ToDateTime(reader["timeStamp"]),
                                    Convert.ToInt32(reader["userID"]),
                                    Convert.ToBoolean(reader["present"])
                                );

                                attendanceRecords.Add(attendanceRecord);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return attendanceRecords;
            }
        }
        public static List<AttendanceRecord> GetUserAttendanceRecords(int userID)
        {
            List<AttendanceRecord> attendanceRecords = new List<AttendanceRecord>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM AttendanceRecord WHERE userID = @userId";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userId", userID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AttendanceRecord attendanceRecord = new AttendanceRecord(
                                    Convert.ToInt32(reader["recordID"]),
                                    Convert.ToDateTime(reader["timeStamp"]),
                                    Convert.ToInt32(reader["userID"]),
                                    Convert.ToBoolean(reader["present"])
                                );

                                attendanceRecords.Add(attendanceRecord);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return attendanceRecords;
            }
        }

        

        ///visit
        /////////////////////////////////////////////////////////////////////
        ///
        public static void InsertVisit(Visit visit)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "INSERT INTO Visit (visitID, userID, patientID, packageID, timeStamp, roomID, type, therapistNotes, height, weight) " +
                                   "VALUES (@visitID, @userID, @patientID, @packageID, @timeStamp, @roomID, @type, @therapistNotes, @height, @weight)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@visitID", visit.VisitID);
                        command.Parameters.AddWithValue("@userID", visit.PhysiotherapistID);
                        command.Parameters.AddWithValue("@patientID", visit.PatientID);
                        command.Parameters.AddWithValue("@packageID", visit.PackageID);
                        command.Parameters.AddWithValue("@timeStamp", visit.TimeStamp);
                        command.Parameters.AddWithValue("@roomID", visit.RoomID);
                        command.Parameters.AddWithValue("@type", visit.Type);
                        command.Parameters.AddWithValue("@therapistNotes", visit.TherapistNotes);
                        command.Parameters.AddWithValue("@height", visit.Height);
                        command.Parameters.AddWithValue("@weight", visit.Weight);
                        command.ExecuteNonQuery();

                        MessageBox.Show("Visit record inserted successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static void UpdateVisit(Visit visit)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "UPDATE Visit SET userID = @userID, patientID = @patientID, " +
                                   "packageID = @packageID, timeStamp = @timeStamp, roomID = @roomID, " +
                                   "type = @type, therapistNotes = @therapistNotes, height = @height , weight = @weight WHERE visitID = @visitID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@visitID", visit.VisitID);
                        command.Parameters.AddWithValue("@userID", visit.PhysiotherapistID);
                        command.Parameters.AddWithValue("@patientID", visit.PatientID);
                        command.Parameters.AddWithValue("@packageID", visit.PackageID);
                        command.Parameters.AddWithValue("@timeStamp", visit.TimeStamp);
                        command.Parameters.AddWithValue("@roomID", visit.RoomID);
                        command.Parameters.AddWithValue("@type", visit.Type);
                        command.Parameters.AddWithValue("@therapistNotes", visit.TherapistNotes);
                        command.Parameters.AddWithValue("@height", visit.Height);
                        command.Parameters.AddWithValue("@weight", visit.Weight);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Updated.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static void DeleteVisit(int visitID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "DELETE FROM Visit WHERE visitID = @visitID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@visitID", visitID);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Visit record deleted successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static Visit GetVisitByID(int visitID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM Visit WHERE visitID = @visitID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@visitID", visitID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Visit(
                                    Convert.ToInt32(reader["visitID"]),
                                    Convert.ToInt32(reader["userID"]),
                                    Convert.ToInt32(reader["patientID"]),
                                    Convert.ToInt32(reader["packageID"]),
                                    Convert.ToDateTime(reader["timeStamp"]),
                                    Convert.ToInt32(reader["roomID"]),
                                    reader["type"].ToString(),
                                    reader["therapistNotes"].ToString(),
                                    Convert.ToDouble(reader["height"]),
                                    Convert.ToDouble(reader["weight"])
                                );
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return null;
            }
        }

        public static List<Visit> GetAllVisits()
        {
            List<Visit> visitList = new List<Visit>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM Visit";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Visit visit = new Visit(
                                    Convert.ToInt32(reader["visitID"]),
                                    Convert.ToInt32(reader["userID"]),
                                    Convert.ToInt32(reader["patientID"]),
                                    Convert.ToInt32(reader["packageID"]),
                                    Convert.ToDateTime(reader["timeStamp"]),
                                    Convert.ToInt32(reader["roomID"]),
                                    reader["type"].ToString(),
                                    reader["therapistNotes"].ToString(),
                                    Convert.ToDouble(reader["height"]),
                                    Convert.ToDouble(reader["weight"])
                                );

                                visitList.Add(visit);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return visitList;
            }
        }
        public static List<Visit> GetAllFutureVisits()
        {
            List<Visit> visitList = new List<Visit>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM Visit WHERE timeStamp >= CURDATE() ORDER BY timeStamp ASC";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Visit visit = new Visit(
                                    Convert.ToInt32(reader["visitID"]),
                                    Convert.ToInt32(reader["userID"]),
                                    Convert.ToInt32(reader["patientID"]),
                                    Convert.ToInt32(reader["packageID"]),
                                    Convert.ToDateTime(reader["timeStamp"]),
                                    Convert.ToInt32(reader["roomID"]),
                                    reader["type"].ToString(),
                                    reader["therapistNotes"].ToString(),
                                    Convert.ToDouble(reader["height"]),
                                    Convert.ToDouble(reader["weight"])
                                );

                                visitList.Add(visit);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return visitList;
            }
        }
        public static List<Visit> GetPatientPrevVisits(int patientID)
        {
            List<Visit> visitList = new List<Visit>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM Visit WHERE patientID=@patientID AND timeStamp < CURDATE()";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@patientID", patientID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Visit visit = new Visit(
                                    Convert.ToInt32(reader["visitID"]),
                                    Convert.ToInt32(reader["userID"]),
                                    Convert.ToInt32(reader["patientID"]),
                                    Convert.ToInt32(reader["packageID"]),
                                    Convert.ToDateTime(reader["timeStamp"]),
                                    Convert.ToInt32(reader["roomID"]),
                                    reader["type"].ToString(),
                                    reader["therapistNotes"].ToString(),
                                    Convert.ToDouble(reader["height"]),
                                    Convert.ToDouble(reader["weight"])
                                );

                                visitList.Add(visit);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return visitList;
            }
        }
        public static List<Visit> GetPatientUpcomingVisits(int patientID)
        {
            List<Visit> visitList = new List<Visit>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM Visit WHERE patientID=@patientID AND timeStamp > CURDATE()";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@patientID", patientID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Visit visit = new Visit(
                                    Convert.ToInt32(reader["visitID"]),
                                    Convert.ToInt32(reader["userID"]),
                                    Convert.ToInt32(reader["patientID"]),
                                    Convert.ToInt32(reader["packageID"]),
                                    Convert.ToDateTime(reader["timeStamp"]),
                                    Convert.ToInt32(reader["roomID"]),
                                    reader["type"].ToString(),
                                    reader["therapistNotes"].ToString(),
                                    Convert.ToDouble(reader["height"]),
                                    Convert.ToDouble(reader["weight"])
                                );

                                visitList.Add(visit);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return visitList;
            }
        }
        public static List<Visit> GetPatientTodayVisits(int patientID)
        {
            List<Visit> visitList = new List<Visit>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM Visit WHERE patientID=@patientID AND DATE(timeStamp) = CURDATE()";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@patientID", patientID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Visit visit = new Visit(
                                    Convert.ToInt32(reader["visitID"]),
                                    Convert.ToInt32(reader["userID"]),
                                    Convert.ToInt32(reader["patientID"]),
                                    Convert.ToInt32(reader["packageID"]),
                                    Convert.ToDateTime(reader["timeStamp"]),
                                    Convert.ToInt32(reader["roomID"]),
                                    reader["type"].ToString(),
                                    reader["therapistNotes"].ToString(),
                                    Convert.ToDouble(reader["height"]),
                                    Convert.ToDouble(reader["weight"])
                                );

                                visitList.Add(visit);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return visitList;
            }
        }
        public static List<Visit> GetAllVisitsByPhysicianID(int physicianID)
        {
            List<Visit> visitsForPhysician = new List<Visit>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT v.* FROM Visit v " +
                                   "INNER JOIN User u ON v.userID = u.userID " +
                                   "WHERE u.userID = @physicianID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@physicianID", physicianID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Visit visit = new Visit(
                                    Convert.ToInt32(reader["visitID"]),
                                    Convert.ToInt32(reader["userID"]),
                                    Convert.ToInt32(reader["patientID"]),
                                    Convert.ToInt32(reader["packageID"]),
                                    Convert.ToDateTime(reader["timeStamp"]),
                                    Convert.ToInt32(reader["roomID"]),
                                    reader["type"].ToString(),
                                    reader["therapistNotes"].ToString(),
                                    Convert.ToDouble(reader["height"]),
                                    Convert.ToDouble(reader["weight"])
                                );

                                visitsForPhysician.Add(visit);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }

            return visitsForPhysician;
        }
        public static List<Visit> GetTodayVisits()
        {
            List<Visit> todayVisits = new List<Visit>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM Visit WHERE DATE(timeStamp) = CURDATE()";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Visit visit = new Visit(
                                    Convert.ToInt32(reader["visitID"]),
                                    Convert.ToInt32(reader["userID"]),
                                    Convert.ToInt32(reader["patientID"]),
                                    Convert.ToInt32(reader["packageID"]),
                                    Convert.ToDateTime(reader["timeStamp"]),
                                    Convert.ToInt32(reader["roomID"]),
                                    reader["type"].ToString(),
                                    reader["therapistNotes"].ToString(),
                                    Convert.ToDouble(reader["height"]),
                                    Convert.ToDouble(reader["weight"])
                                );

                                todayVisits.Add(visit);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }

            return todayVisits;
        }
        public static List<Visit> GetVisitsByDay(DateTime day)
        {
            List<Visit> todayVisits = new List<Visit>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM Visit WHERE CONVERT(DATE, timeStamp) = @ProvidedDate";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            command.Parameters.AddWithValue("@ProvidedDate", day.Date);

                            while (reader.Read())
                            {
                                Visit visit = new Visit(
                                    Convert.ToInt32(reader["visitID"]),
                                    Convert.ToInt32(reader["userID"]),
                                    Convert.ToInt32(reader["patientID"]),
                                    Convert.ToInt32(reader["packageID"]),
                                    Convert.ToDateTime(reader["timeStamp"]),
                                    Convert.ToInt32(reader["roomID"]),
                                    reader["type"].ToString(),
                                    reader["therapistNotes"].ToString(),
                                    Convert.ToDouble(reader["height"]),
                                    Convert.ToDouble(reader["weight"])
                                );

                                todayVisits.Add(visit);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }

            return todayVisits;
        }
        public static List<Visit> GetTodayPhysicianVisits(int physicianID)
        {
            List<Visit> todayPhysicianVisits = new List<Visit>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT v.* FROM Visit v " +
                                   "INNER JOIN User u ON v.userID = u.userID " +
                                   "WHERE u.userID = @physicianID AND DATE(v.timeStamp) = CURDATE()";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@physicianID", physicianID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Visit visit = new Visit(
                                    Convert.ToInt32(reader["visitID"]),
                                    Convert.ToInt32(reader["userID"]),
                                    Convert.ToInt32(reader["patientID"]),
                                    Convert.ToInt32(reader["packageID"]),
                                    Convert.ToDateTime(reader["timeStamp"]),
                                    Convert.ToInt32(reader["roomID"]),
                                    reader["type"].ToString(),
                                    reader["therapistNotes"].ToString(),
                                    Convert.ToDouble(reader["height"]),
                                    Convert.ToDouble(reader["weight"])
                                );

                                todayPhysicianVisits.Add(visit);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }

            return todayPhysicianVisits;
        }
        public static List<Visit> GetPhysicianVisitsOnDate(int physicianID, DateTime date)
        {
            List<Visit> physicianVisitsOnDate = new List<Visit>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT v.* FROM Visit v " +
                                   "INNER JOIN User u ON v.userID = u.userID " +
                                   "WHERE u.userID = @physicianID AND DATE(v.timeStamp) = @visitDate";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@physicianID", physicianID);
                        command.Parameters.AddWithValue("@visitDate", date.ToString("yyyy-MM-dd"));

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Visit visit = new Visit(
                                    Convert.ToInt32(reader["visitID"]),
                                    Convert.ToInt32(reader["userID"]),
                                    Convert.ToInt32(reader["patientID"]),
                                    Convert.ToInt32(reader["packageID"]),
                                    Convert.ToDateTime(reader["timeStamp"]),
                                    Convert.ToInt32(reader["roomID"]),
                                    reader["type"].ToString(),
                                    reader["therapistNotes"].ToString(),
                                    Convert.ToDouble(reader["height"]),
                                    Convert.ToDouble(reader["weight"])
                                );

                                physicianVisitsOnDate.Add(visit);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }

            return physicianVisitsOnDate;
        }


        /// evaluationTest
        ///////////////////////////////////////////////////////////////////
        ///
        public static List<EvaluationTest> GetAllTests()
        {
            List<EvaluationTest> tests = new List<EvaluationTest>();

            using (connection)
            {
                connection.Open();

                string query = "SELECT * FROM evaluationTest";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            EvaluationTest test = new EvaluationTest(
                                Convert.ToInt32(reader["testID"]),
                                Convert.ToString(reader["testName"]),
                                Convert.ToString(reader["testDescription"])
                            
                                );

                            tests.Add(test);
                        }
                    }
                }
            }

            return tests;
        }

        public static EvaluationTest GetTestById(int testId)
        {
            using (connection)
            {
                connection.Open();

                string query = "SELECT * FROM evaluationTest WHERE testID = @TestID";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@TestID", testId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new EvaluationTest(
                                Convert.ToInt32(reader["testID"]),
                                Convert.ToString(reader["testName"]),
                                Convert.ToString(reader["testDescription"])
                            );
                        }
                    }
                }
            }

            return null; // Test not found
        }

        public static void InsertTest(EvaluationTest test)
        {
            using (connection)
            {
                connection.Open();

                string query = "INSERT INTO evaluationTest (testID, testName, testDescription) " +
                               "VALUES (@TestID, @TestName, @TestDescription)";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@TestID", test.TestID);
                    cmd.Parameters.AddWithValue("@TestName", test.TestName);
                    cmd.Parameters.AddWithValue("@TestDescription", test.TestDescription);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void UpdateTest(EvaluationTest test)
        {
            using (connection)
            {
                connection.Open();

                string query = "UPDATE evaluationTest SET testName=@TestName, testDescription=@TestDescription WHERE testID=@TestID ";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@TestID", test.TestID);
                    cmd.Parameters.AddWithValue("@TestName", test.TestName);
                    cmd.Parameters.AddWithValue("@TestDescription", test.TestDescription);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        public static void DeleteTest(int testId)
        {
            using (connection)
            {
                connection.Open();

                string query = "DELETE FROM evaluationTest WHERE testID = @TestID";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@TestID", testId);

                    cmd.ExecuteNonQuery();
                }
            }
        }



        ///Test feedback
        /////////////////////////////////////////////////////////////////
        ///
        public static void InsertFeedback(EvaluationTestFeedBack feedback)
        {
            using (connection)
            {
                connection.Open();

                string query = "INSERT INTO testFeedBack (testFeedBackID, severity, visitID, patientID, testID, notes, timeStamp) " +
                               "VALUES (@TestFeedbackID, @Severity, @VisitID, @PatientID, @TestID, @Notes, @TimeStamp)";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@TestFeedbackID", feedback.TestFeedBackID);
                    cmd.Parameters.AddWithValue("@Severity", feedback.Severity);
                    cmd.Parameters.AddWithValue("@VisitID", feedback.VisitID);
                    cmd.Parameters.AddWithValue("@PatientID", feedback.PatientID);
                    cmd.Parameters.AddWithValue("@TestID", feedback.TestID);
                    cmd.Parameters.AddWithValue("@Notes", feedback.Notes);
                    cmd.Parameters.AddWithValue("@TimeStamp", feedback.TimeStamp);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<EvaluationTestFeedBack> GetAllFeedback()
        {
            List<EvaluationTestFeedBack> visitFeedbackList = new List<EvaluationTestFeedBack>();

            using (connection)
            {
                connection.Open();

                string query = "SELECT * FROM testFeedBack";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            EvaluationTestFeedBack feedback = new EvaluationTestFeedBack(
                                Convert.ToInt32(reader["testFeedBackID"]),
                                Convert.ToInt32(reader["severity"]),
                                Convert.ToInt32(reader["visitID"]),
                                Convert.ToInt32(reader["patientID"]),
                                Convert.ToInt32(reader["testID"]),
                                reader["notes"].ToString(),
                                Convert.ToDateTime(reader["timeStamp"])


                            );

                            visitFeedbackList.Add(feedback);
                        }
                    }
                }
            }

            return visitFeedbackList;
        }

        public static List<EvaluationTestFeedBack> GetFeedbackByPatient(int patientId)
        {
            List<EvaluationTestFeedBack> visitFeedbackList = new List<EvaluationTestFeedBack>();

            using (connection)
            {
                connection.Open();

                string query = "SELECT * FROM testFeedBack WHERE patientID = @VisitID";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@VisitID", patientId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            EvaluationTestFeedBack feedback = new EvaluationTestFeedBack(
                                Convert.ToInt32(reader["testFeedBackID"]),
                                Convert.ToInt32(reader["severity"]),
                                Convert.ToInt32(reader["visitID"]),
                                Convert.ToInt32(reader["patientID"]),
                                Convert.ToInt32(reader["testID"]),
                                reader["notes"].ToString(),
                                Convert.ToDateTime(reader["timeStamp"])
                            );

                            visitFeedbackList.Add(feedback);
                        }
                    }
                }
            }

            return visitFeedbackList;
        }

        public static List<EvaluationTestFeedBack> GetFeedbackByVisit(int visitId)
        {
            List<EvaluationTestFeedBack> visitFeedbackList = new List<EvaluationTestFeedBack>();

            using (connection)
            {
                connection.Open();

                string query = "SELECT * FROM testFeedBack WHERE visitID = @VisitID";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@VisitID", visitId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            EvaluationTestFeedBack feedback = new EvaluationTestFeedBack(
                                Convert.ToInt32(reader["testFeedBackID"]),
                                Convert.ToInt32(reader["severity"]),
                                Convert.ToInt32(reader["visitID"]),
                                Convert.ToInt32(reader["patientID"]),
                                Convert.ToInt32(reader["testID"]),
                                reader["notes"].ToString(),
                                Convert.ToDateTime(reader["timeStamp"])
                            );

                            visitFeedbackList.Add(feedback);
                        }
                    }
                }
            }

            return visitFeedbackList;
        }

        public static void DeleteAllVisitFeedback(int visitID)
        {
            using (connection)
            {
                connection.Open();

                string query = "DELETE FROM testFeedBack WHERE visitID = @TestFeedbackID";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@TestFeedbackID", visitID);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void DeleteFeedback(int feedbackId)
        {
            using (connection)
            {
                connection.Open();

                string query = "DELETE FROM testFeedBack WHERE testFeedBackID = @TestFeedbackID";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@TestFeedbackID", feedbackId);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        ///prescription
        ////////////////////////////////////////////////////////////////
        ///
        public static void InsertPrescription(Prescription prescription)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "INSERT INTO Prescription (prescriptionID, timeStamp, patientID, userID, visitID) " +
                                   "VALUES (@prescriptionID, @timeStamp, @patientID, @userID, @visitID)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@prescriptionID", prescription.PrescriptionID);
                        command.Parameters.AddWithValue("@timeStamp", prescription.TimeStamp);
                        command.Parameters.AddWithValue("@patientID", prescription.PatientID);
                        command.Parameters.AddWithValue("@userID", prescription.UserID);
                        command.Parameters.AddWithValue("@visitID", prescription.VisitID);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Prescription record inserted successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static void UpdatePrescription(Prescription prescription)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "UPDATE Prescription SET timeStamp = @timeStamp, " +
                                   "patientID = @patientID, userID = @userID, visitID = @visitID " +
                                   "WHERE prescriptionID = @prescriptionID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@prescriptionID", prescription.PrescriptionID);
                        command.Parameters.AddWithValue("@timeStamp", prescription.TimeStamp);
                        command.Parameters.AddWithValue("@patientID", prescription.PatientID);
                        command.Parameters.AddWithValue("@userID", prescription.UserID);
                        command.Parameters.AddWithValue("@visitID", prescription.VisitID);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Prescription record updated successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static void DeletePrescription(int prescriptionID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "DELETE FROM Prescription WHERE prescriptionID = @prescriptionID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@prescriptionID", prescriptionID);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Prescription record deleted successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static Prescription GetPrescriptionByID(int prescriptionID)
        {
            using (connection)
            {
                Prescription toReturn=new Prescription();

                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM Prescription WHERE prescriptionID = @prescriptionID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@prescriptionID", prescriptionID);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                toReturn=new Prescription(
                                    Convert.ToInt32(reader["prescriptionID"]),
                                    Convert.ToDateTime(reader["timeStamp"]),
                                    Convert.ToInt32(reader["patientID"]),
                                    Convert.ToInt32(reader["userID"]),
                                    Convert.ToInt32(reader["visitID"])

                                );
                            }
                        }
                    }
                    toReturn.IssuedExercisesIDs = GetIssuedExercisesByPrescriptionID(prescriptionID);

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return null;
            }
        }

        public static List<Prescription> GetAllPrescriptions()
        {
            List<Prescription> prescriptionList = new List<Prescription>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM Prescription";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Prescription prescription = new Prescription(
                                    Convert.ToInt32(reader["prescriptionID"]),
                                    Convert.ToDateTime(reader["timeStamp"]),
                                    Convert.ToInt32(reader["patientID"]),
                                    Convert.ToInt32(reader["userID"]),
                                    Convert.ToInt32(reader["visitID"])
                                );

                                prescriptionList.Add(prescription);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return prescriptionList;
            }
        }

        public static List<Prescription> GetAllPrescriptionsByPatientID(int patientID)
        {
            List<Prescription> prescriptionList = new List<Prescription>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM Prescription WHERE patientID=@patientID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@patientID", patientID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Prescription prescription = new Prescription(
                                    Convert.ToInt32(reader["prescriptionID"]),
                                    Convert.ToDateTime(reader["timeStamp"]),
                                    Convert.ToInt32(reader["patientID"]),
                                    Convert.ToInt32(reader["userID"]),
                                    Convert.ToInt32(reader["visitID"])
                                );

                                prescriptionList.Add(prescription);
                            }
                        }
                    }
                    foreach(Prescription p in prescriptionList)
                    {
                        p.IssuedDrugsIDs = GetIssuedDrugsByPrescriptionID(p.PrescriptionID);
                    }

                }
                catch (Exception ex)
                {

                    MessageBox.Show($"Error: {ex.Message}");
                }

                return prescriptionList;
            }
        }
        public static  List<Prescription> GetAllPrescriptionsByVisitID(int visitID)
        {
            List<Prescription> prescriptionList = new List<Prescription>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM Prescription WHERE visitID=@visitID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@visitID", visitID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Prescription prescription = new Prescription(
                                    Convert.ToInt32(reader["prescriptionID"]),
                                    Convert.ToDateTime(reader["timeStamp"]),
                                    Convert.ToInt32(reader["patientID"]),
                                    Convert.ToInt32(reader["userID"]),
                                    Convert.ToInt32(reader["visitID"])
                                );

                                prescriptionList.Add(prescription);
                            }
                        }
                    }
                    foreach (Prescription p in prescriptionList)
                    {
                        p.IssuedDrugsIDs = GetIssuedDrugsByPrescriptionID(p.PrescriptionID);
                    }
                }

                catch (Exception ex)
                {

                    MessageBox.Show($"Error: {ex.Message}");
                }

                return prescriptionList;
            }
        }



        ///medicalRecord
        ////////////////////////////////////////////////////////////////////////
        ///
        public static void InsertMedicalRecord(MedicalRecord medicalRecord)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "INSERT INTO MedicalRecord (recordID, type, timeStamp, report, images, patientID, physicianNotes) " +
                                   "VALUES (@recordID, @type, @timeStamp, @report, @images, @patientID, @physicianNotes)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@recordID", medicalRecord.RecordID);
                        command.Parameters.AddWithValue("@type", medicalRecord.Type);
                        command.Parameters.AddWithValue("@timeStamp", medicalRecord.TimeStamp);
                        command.Parameters.AddWithValue("@report", medicalRecord.Report);
                        command.Parameters.AddWithValue("@images", string.Join(",", medicalRecord.Images));
                        command.Parameters.AddWithValue("@patientID", medicalRecord.PatientID);
                        command.Parameters.AddWithValue("@physicianNotes", medicalRecord.PhysicianNotes);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Medical record inserted successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static void UpdateMedicalRecord(MedicalRecord medicalRecord)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "UPDATE MedicalRecord SET type = @type, " +
                                   "timeStamp = @timeStamp, report = @report, images = @images, " +
                                   "patientID = @patientID, physicianNotes = @physicianNotes " +
                                   "WHERE recordID = @recordID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@recordID", medicalRecord.RecordID);
                        command.Parameters.AddWithValue("@type", medicalRecord.Type);
                        command.Parameters.AddWithValue("@timeStamp", medicalRecord.TimeStamp);
                        command.Parameters.AddWithValue("@report", medicalRecord.Report);
                        command.Parameters.AddWithValue("@images", string.Join(",", medicalRecord.Images));
                        command.Parameters.AddWithValue("@patientID", medicalRecord.PatientID);
                        command.Parameters.AddWithValue("@physicianNotes", medicalRecord.PhysicianNotes);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Medical record updated successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static void DeleteMedicalRecord(int recordID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "DELETE FROM MedicalRecord WHERE recordID = @recordID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@recordID", recordID);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Medical record deleted successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static MedicalRecord GetMedicalRecordByID(int recordID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM MedicalRecord WHERE recordID = @recordID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@recordID", recordID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new MedicalRecord(
                                    Convert.ToInt32(reader["recordID"]),
                                    reader["type"].ToString(),
                                    Convert.ToDateTime(reader["timeStamp"]),
                                    reader["report"].ToString(),
                                    new List<string>(reader["images"].ToString().Split(',')),
                                    Convert.ToInt32(reader["patientID"]),
                                    reader["physicianNotes"].ToString()
                                );
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return null;
            }
        }

        public static List<MedicalRecord> GetAllMedicalRecords()
        {
            List<MedicalRecord> medicalRecordList = new List<MedicalRecord>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM MedicalRecord";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MedicalRecord medicalRecord = new MedicalRecord(
                                    Convert.ToInt32(reader["recordID"]),
                                    reader["type"].ToString(),
                                    Convert.ToDateTime(reader["timeStamp"]),
                                    reader["report"].ToString(),
                                    new List<string>(reader["images"].ToString().Split(',')),
                                    Convert.ToInt32(reader["patientID"]),
                                    reader["physicianNotes"].ToString()
                                );

                                medicalRecordList.Add(medicalRecord);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return medicalRecordList;
            }
        }

        

        public static List<MedicalRecord> GetAllPatientRecords(int patientID)
        {
            List<MedicalRecord> patientMedicalRecords = new List<MedicalRecord>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM MedicalRecord WHERE patientID = @patientID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@patientID", patientID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MedicalRecord medicalRecord = new MedicalRecord(
                                    Convert.ToInt32(reader["recordID"]),
                                    reader["type"].ToString(),
                                    Convert.ToDateTime(reader["timeStamp"]),
                                    reader["report"].ToString(),
                                    new List<string>(reader["images"].ToString().Split(',')),
                                    Convert.ToInt32(reader["patientID"]),
                                    reader["physicianNotes"].ToString()
                                );

                                patientMedicalRecords.Add(medicalRecord);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return patientMedicalRecords;
            }
        }


        ///issueDrug
        ///////////////////////////////////////////////////////////////////////////
        ///
        public static void InsertIssueDrug(IssueDrug issueDrug)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "INSERT INTO IssueDrug (issueID, drugID, prescriptionID, patientID, frequency, notes) " +
                                   "VALUES (@issueID, @drugID, @prescriptionID, @patientID, @frequency, @notes)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@issueID", issueDrug.IssueID);
                        command.Parameters.AddWithValue("@drugID", issueDrug.DrugID);
                        command.Parameters.AddWithValue("@prescriptionID", issueDrug.PrescriptionID);
                        command.Parameters.AddWithValue("@patientID", issueDrug.PatientID);
                        command.Parameters.AddWithValue("@frequency", issueDrug.Frequency);
                        command.Parameters.AddWithValue("@notes", issueDrug.Notes);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Issue drug record inserted successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static void UpdateIssueDrug(IssueDrug issueDrug)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "UPDATE IssueDrug SET drugID = @drugID, " +
                                   "prescriptionID = @prescriptionID, patientID = @patientID, " +
                                   "frequency = @frequency, notes = @notes " +
                                   "WHERE issueID = @issueID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@issueID", issueDrug.IssueID);
                        command.Parameters.AddWithValue("@drugID", issueDrug.DrugID);
                        command.Parameters.AddWithValue("@prescriptionID", issueDrug.PrescriptionID);
                        command.Parameters.AddWithValue("@patientID", issueDrug.PatientID);
                        command.Parameters.AddWithValue("@frequency", issueDrug.Frequency);
                        command.Parameters.AddWithValue("@notes", issueDrug.Notes);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Issue drug record updated successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static void DeleteIssueDrug(int issueID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "DELETE FROM IssueDrug WHERE issueID = @issueID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@issueID", issueID);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Issue drug record deleted successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static IssueDrug GetIssueDrugByID(int issueID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM IssueDrug WHERE issueID = @issueID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@issueID", issueID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new IssueDrug(
                                    Convert.ToInt32(reader["issueID"]),
                                    Convert.ToInt32(reader["drugID"]),
                                    Convert.ToInt32(reader["prescriptionID"]),
                                    Convert.ToInt32(reader["patientID"]),
                                    reader["frequency"].ToString(),
                                    reader["notes"].ToString()
                                );
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return null;
            }
        }

        public static List<IssueDrug> GetAllIssueDrugs()
        {
            List<IssueDrug> issueDrugList = new List<IssueDrug>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM IssueDrug";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                IssueDrug issueDrug = new IssueDrug(
                                    Convert.ToInt32(reader["issueID"]),
                                    Convert.ToInt32(reader["drugID"]),
                                    Convert.ToInt32(reader["prescriptionID"]),
                                    Convert.ToInt32(reader["patientID"]),
                                    reader["frequency"].ToString(),
                                    reader["notes"].ToString()
                                );

                                issueDrugList.Add(issueDrug);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return issueDrugList;
            }
        }

        public static List<IssueDrug> GetIssuedDrugsByPrescriptionID(int prescriptionID)
        {
            List<IssueDrug> issuedDrugsByPrescriptionID = new List<IssueDrug>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM IssueDrug WHERE prescriptionID = @prescriptionID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@prescriptionID", prescriptionID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                IssueDrug issueDrug = new IssueDrug(
                                    Convert.ToInt32(reader["issueID"]),
                                    Convert.ToInt32(reader["drugID"]),
                                    Convert.ToInt32(reader["prescriptionID"]),
                                    Convert.ToInt32(reader["patientID"]),
                                    reader["frequency"].ToString(),
                                    reader["notes"].ToString()
                                );

                                issuedDrugsByPrescriptionID.Add(issueDrug);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return issuedDrugsByPrescriptionID;
            }
        }

        public static List<IssueDrug> GetIssuedDrugsByPatientID(int patientID)
        {
            List<IssueDrug> issuedDrugsByPatientID = new List<IssueDrug>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM IssueDrug WHERE patientID = @patientID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@patientID", patientID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                IssueDrug issueDrug = new IssueDrug(
                                    Convert.ToInt32(reader["issueID"]),
                                    Convert.ToInt32(reader["drugID"]),
                                    Convert.ToInt32(reader["prescriptionID"]),
                                    Convert.ToInt32(reader["patientID"]),
                                    reader["frequency"].ToString(),
                                    reader["notes"].ToString()
                                );

                                issuedDrugsByPatientID.Add(issueDrug);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return issuedDrugsByPatientID;
            }
        }

        public static List<IssueDrug> GetAllIssuesOfDrug(int drugID)
        {
            List<IssueDrug> allIssuesOfDrug = new List<IssueDrug>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM IssueDrug WHERE drugID = @drugID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@drugID", drugID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                IssueDrug issueDrug = new IssueDrug(
                                    Convert.ToInt32(reader["issueID"]),
                                    Convert.ToInt32(reader["drugID"]),
                                    Convert.ToInt32(reader["prescriptionID"]),
                                    Convert.ToInt32(reader["patientID"]),
                                    reader["frequency"].ToString(),
                                    reader["notes"].ToString()
                                );

                                allIssuesOfDrug.Add(issueDrug);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return allIssuesOfDrug;
            }
        }


        ///issueExercise
        /////////////////////////////////////////////////////////////////////////
        ///
        public static void InsertIssueExercise(IssueExercise issueExercise)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "INSERT INTO IssueExercise (issueID, exerciseID, patientID, prescriptionID, frequency, notes) " +
                                   "VALUES (@issueID, @exerciseID, @patientID, @prescriptionID, @frequency, @notes)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@issueID", issueExercise.IssueID);
                        command.Parameters.AddWithValue("@exerciseID", issueExercise.ExerciseID);
                        command.Parameters.AddWithValue("@patientID", issueExercise.PatientID);
                        command.Parameters.AddWithValue("@prescriptionID", issueExercise.PrescriptionID);
                        command.Parameters.AddWithValue("@frequency", issueExercise.Frequency);
                        command.Parameters.AddWithValue("@notes", issueExercise.Notes);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Issue exercise record inserted successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static void UpdateIssueExercise(IssueExercise issueExercise)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "UPDATE IssueExercise SET exerciseID = @exerciseID, " +
                                   "patientID = @patientID, prescriptionID = @prescriptionID, " +
                                   "frequency = @frequency, notes = @notes " +
                                   "WHERE issueID = @issueID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@issueID", issueExercise.IssueID);
                        command.Parameters.AddWithValue("@exerciseID", issueExercise.ExerciseID);
                        command.Parameters.AddWithValue("@patientID", issueExercise.PatientID);
                        command.Parameters.AddWithValue("@prescriptionID", issueExercise.PrescriptionID);
                        command.Parameters.AddWithValue("@frequency", issueExercise.Frequency);
                        command.Parameters.AddWithValue("@notes", issueExercise.Notes);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Issue exercise record updated successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static void DeleteIssueExercise(int issueID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "DELETE FROM IssueExercise WHERE issueID = @issueID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@issueID", issueID);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Issue exercise record deleted successfully.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        public static IssueExercise GetIssueExerciseByID(int issueID)
        {
            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM IssueExercise WHERE issueID = @issueID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@issueID", issueID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new IssueExercise(
                                    Convert.ToInt32(reader["issueID"]),
                                    Convert.ToInt32(reader["exerciseID"]),
                                    Convert.ToInt32(reader["patientID"]),
                                    Convert.ToInt32(reader["prescriptionID"]),
                                    reader["frequency"].ToString(),
                                    reader["notes"].ToString()
                                );
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return null;
            }
        }

        public static List<IssueExercise> GetIssuedExercisesByPrescriptionID(int prescriptionID)
        {
            List<IssueExercise> issuedExercises = new List<IssueExercise>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM IssueExercise WHERE prescriptionID = @prescriptionID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@prescriptionID", prescriptionID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                IssueExercise issueExercise = new IssueExercise(
                                    Convert.ToInt32(reader["issueID"]),
                                    Convert.ToInt32(reader["exerciseID"]),
                                    Convert.ToInt32(reader["patientID"]),
                                    Convert.ToInt32(reader["prescriptionID"]),
                                    reader["frequency"].ToString(),
                                    reader["notes"].ToString()
                                );

                                issuedExercises.Add(issueExercise);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return issuedExercises;
            }
        }

        public static List<IssueExercise> GetIssuedExercisesByPatientID(int patientID)
        {
            List<IssueExercise> issuedExercises = new List<IssueExercise>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM IssueExercise WHERE patientID = @patientID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@patientID", patientID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                IssueExercise issueExercise = new IssueExercise(
                                    Convert.ToInt32(reader["issueID"]),
                                    Convert.ToInt32(reader["exerciseID"]),
                                    Convert.ToInt32(reader["patientID"]),
                                    Convert.ToInt32(reader["prescriptionID"]),
                                    reader["frequency"].ToString(),
                                    reader["notes"].ToString()
                                );

                                issuedExercises.Add(issueExercise);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return issuedExercises;
            }
        }

        public static List<IssueExercise> GetAllIssuesOfExercise(int exerciseID)
        {
            List<IssueExercise> allIssuesOfExercise = new List<IssueExercise>();

            using (connection)
            {
                try
                {
                    if (connection.State == ConnectionState.Closed) connection.Open();

                    string query = "SELECT * FROM IssueExercise WHERE exerciseID = @exerciseID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@exerciseID", exerciseID);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                IssueExercise issueExercise = new IssueExercise(
                                    Convert.ToInt32(reader["issueID"]),
                                    Convert.ToInt32(reader["exerciseID"]),
                                    Convert.ToInt32(reader["patientID"]),
                                    Convert.ToInt32(reader["prescriptionID"]),
                                    reader["frequency"].ToString(),
                                    reader["notes"].ToString()
                                );

                                allIssuesOfExercise.Add(issueExercise);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }

                return allIssuesOfExercise;
            }
        }

        
    }
}
