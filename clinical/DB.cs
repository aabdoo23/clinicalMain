using clinical.BaseClasses;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;

namespace clinical
{
    class DB
    {
        static MySqlConnection connection;
        public DB()
        {
            string connectionString = "server=localhost;database=clinical;user=root;password=root;";

            // Create MySqlConnection
            using (connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connection Message: " +ex.Message);

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
                    connection.Open();

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

        public static void DeleteUser(string userID)
        {
            using (connection)
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand("DELETE FROM User WHERE userID = @userID", connection))
                    {
                        cmd.Parameters.AddWithValue("@userID", userID);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex) { }
            }
        }

        public static List<User> GetAllUsers()
        {
            List<User> users = new List<User>();
            using (connection)
            {
                try
                {
                    connection.Open();
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
                    connection.Open();

                    string query = "SELECT * FROM User WHERE userID = @userID";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userID", userID);


                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            //MessageBox.Show(reader.Read().ToString());

                            if (reader.Read())
                            {
                                MessageBox.Show(reader["userID"].ToString());

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
                    connection.Open();
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
                    connection.Open();
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
                    connection.Open();
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


        ///<Patient>
        /////////////////////////complete?////////////////////////////////
        ///</Patient>

        public static Patient MapPatient(MySqlDataReader reader)
        {
            List<int> chronicDiseases = new List<int>();
            //string cIds = reader.GetString("chronicDiseasesIDs");
            //string[] sc = (cIds.Split(", "));
            //foreach (string i in sc)
            //{
            //    i.Trim();
            //    chronicDiseases.Add(Convert.ToInt16(i));

            //}

            List<int> injuries = new List<int>();
            //string iIds = reader.GetString("previousInjuriesIDs");
            //string[] si = (cIds.Split(", "));
            //foreach (string i in si)
            //{
            //    i.Trim();
            //    chronicDiseases.Add(Convert.ToInt16(i));

            //}




            return new Patient(
                Convert.ToInt32(reader["patientID"]),
                reader["firstName"].ToString(),
                reader["lastName"].ToString(),
                Convert.ToDateTime(reader["birthdate"]),
                reader["gender"].ToString(),
                reader["phoneNumber"].ToString(),
                reader["email"].ToString(),
                reader["address"].ToString(),
                chronicDiseases,
                injuries,
                Convert.ToInt32(reader["physicianID"]),
                Convert.ToBoolean(reader["referred"]),
                Convert.ToBoolean(reader["previouslyTreated"]),
                Convert.ToDouble(reader["height"]),
                Convert.ToDouble(reader["weight"]),
                Convert.ToDouble(reader["dueAmount"]),
                reader["referringName"].ToString(),
                reader["referringPN"].ToString()

            );

        }
        public static Patient GetPatientById(int patientID)
        {
            using (connection)
            {
                try
                {
                    connection.Open();
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
                    connection.Open();

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
                    connection.Open();
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
                    connection.Open();
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
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand(
                        "INSERT INTO Patient (patientID, firstName, lastName, birthdate, gender, phoneNumber, email, address, chronicDiseasesIDs, previousInjuriesIDs, referred, previouslyTreated, height, weight, dueAmount, physicianID, referringName, referringPN)" +
                        "VALUES (@patientID, @firstName, @lastName, @birthdate, @gender, @phoneNumber, @email, @address, @chronicDiseasesIDs, @previousInjuriesIDs, @referred, @previouslyTreated, @height, @weight, @dueAmount, @physicianID, @referringName, @referringPN)", connection))
                    {
                        cmd.Parameters.AddWithValue("@patientID",           patient.PatientID);
                        cmd.Parameters.AddWithValue("@firstName",           patient.FirstName);
                        cmd.Parameters.AddWithValue("@lastName",            patient.LastName);
                        cmd.Parameters.AddWithValue("@birthdate",           patient.Birthdate);
                        cmd.Parameters.AddWithValue("@gender",              patient.Gender=="Male");
                        cmd.Parameters.AddWithValue("@phoneNumber",         patient.PhoneNumber);
                        cmd.Parameters.AddWithValue("@email",               patient.Email);
                        cmd.Parameters.AddWithValue("@address",             patient.Address);
                        cmd.Parameters.AddWithValue("@chronicDiseasesIDs",  patient.chronics());
                        cmd.Parameters.AddWithValue("@previousInjuriesIDs", patient.injuries());
                        cmd.Parameters.AddWithValue("@physicianID",         patient.PhysicianID);
                        cmd.Parameters.AddWithValue("@referred",            patient.Referred==true);
                        cmd.Parameters.AddWithValue("@previouslyTreated",   patient.PreviouslyTreated==true);
                        cmd.Parameters.AddWithValue("@height",              patient.Height);
                        cmd.Parameters.AddWithValue("@weight",              patient.Weight);
                        cmd.Parameters.AddWithValue("@dueAmount",           patient.DueAmount);
                        cmd.Parameters.AddWithValue("@referringName",       patient.referringName);
                        cmd.Parameters.AddWithValue("@referringPN",         patient.referringPN);


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


        ///ChronicDisease
        ////////////////////////////////////////////////////////
        ///

        public static ChronicDisease GetChronicDiseaseById(int chronicDiseaseID)
        {
            using (connection)
            {
                try
                {
                    connection.Open();
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
                    connection.Open();
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
                    connection.Open();
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

        public static void InsertChronicDisease(ChronicDisease chronicDisease)
        {
            using (connection)
            {
                try
                {
                    connection.Open();
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
                    connection.Open();
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
                    Console.WriteLine(ex.Message);
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
                    connection.Open();
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
                    Console.WriteLine(ex.Message);
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
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand("DELETE FROM Exercise WHERE exerciseID = @exerciseID", connection))
                    {
                        cmd.Parameters.AddWithValue("@exerciseID", exerciseID);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (log, throw, etc.)
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public static void InsertExercise(Exercise exercise)
        {
            using (connection)
            {
                try
                {
                    connection.Open();
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
                    Console.WriteLine(ex.Message);
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


        public static TreatmentPlan GetTreatmentPlanById(int planID)
        {
            using (connection)
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM TreatmentPlan WHERE planID = @planID", connection))
                    {
                        cmd.Parameters.AddWithValue("@planID", planID);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapTreatmentPlan(reader);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (log, throw, etc.)
                    Console.WriteLine(ex.Message);
                }
            }

            return null;
        }

        public static List<TreatmentPlan> GetAllTreatmentPlans()
        {
            List<TreatmentPlan> treatmentPlans = new List<TreatmentPlan>();
            using (connection)
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM TreatmentPlan", connection))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TreatmentPlan treatmentPlan = MapTreatmentPlan(reader);
                                treatmentPlans.Add(treatmentPlan);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (log, throw, etc.)
                    Console.WriteLine(ex.Message);
                }
            }

            return treatmentPlans;
        }

        public static void DeleteTreatmentPlan(int planID)
        {
            using (connection)
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand("DELETE FROM TreatmentPlan WHERE planID = @planID", connection))
                    {
                        cmd.Parameters.AddWithValue("@planID", planID);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (log, throw, etc.)
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public static void InsertTreatmentPlan(TreatmentPlan treatmentPlan)
        {
            using (connection)
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand(
                        "INSERT INTO TreatmentPlan (planID, planName, planTime, price, notes) " +
                        "VALUES (@planID, @planName, @planTime, @price, @notes)",
                        connection))
                    {
                        cmd.Parameters.AddWithValue("@planID", treatmentPlan.PlanID);
                        cmd.Parameters.AddWithValue("@planName", treatmentPlan.PlanName);
                        cmd.Parameters.AddWithValue("@planTime", treatmentPlan.PlanTimeInWeeks);
                        cmd.Parameters.AddWithValue("@price", treatmentPlan.Price);
                        cmd.Parameters.AddWithValue("@notes", treatmentPlan.Notes);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception (log, throw, etc.)
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static TreatmentPlan MapTreatmentPlan(MySqlDataReader reader)
        {
            return new TreatmentPlan(
                reader.GetInt32("planID"),
                reader.GetString("planName"),
                reader.GetInt32("planTime"),
                reader.IsDBNull("price") ? 0 : reader.GetDouble("price"),
                reader.IsDBNull("notes") ? null : reader.GetString("notes")
            );
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
                    connection.Open();

                    string query = "INSERT INTO ChatRoom (chatRoomID, firstUserID, secondUserID, chatRoomName) " +
                                   "VALUES (@chatRoomID, @firstUserID, @secondUserID, @chatRoomName)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@chatRoomID", chatRoom.ChatRoomID);
                        command.Parameters.AddWithValue("@firstUserID", chatRoom.FirstUserID);
                        command.Parameters.AddWithValue("@secondUserID", chatRoom.SecondUserID);
                        command.Parameters.AddWithValue("@chatRoomName", chatRoom.ChatRoomName);

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
                    connection.Open();

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

        public static ChatRoom GetChatRoomByID(int chatRoomID)
        {
            using (connection)
            {
                try
                {
                    connection.Open();

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
                                    reader["chatRoomName"].ToString()
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
                    connection.Open();

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
                                    reader["chatRoomName"].ToString()
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
                    connection.Open();

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
                                    reader["chatRoomName"].ToString()
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

    }


}
