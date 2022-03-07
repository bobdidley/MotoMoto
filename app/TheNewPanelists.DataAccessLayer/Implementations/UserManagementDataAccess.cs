using System.Linq;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Text;
using TheNewPanelists.ServiceLayer.Logging;


namespace TheNewPanelists.DataAccessLayer
{
    public class UserManagementDataAccess : IDataAccess
    {
        private string? query { get; set; }
        private MySqlConnection? mySqlConnection = null;

        public UserManagementDataAccess() {}

        public UserManagementDataAccess(string query)
        {
            this.query = query;
        }

        private void BuildTempUser()
        {
            // Hides password
            Console.WriteLine("Please Enter Your MariaDB Username:");
            string? username = Console.ReadLine();
            Console.WriteLine($"Please Enter the password for {username}:");
            StringBuilder input = new StringBuilder();
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter) break;
                if (key.Key == ConsoleKey.Backspace && input.Length > 0) input.Remove(input.Length - 1, 1);
                else if (key.Key != ConsoleKey.Backspace) input.Append(key.KeyChar);
            }
            string pass = input.ToString();
            // Console.WriteLine(pass);
            // Console.WriteLine(System.Environment.UserName);

            MySqlConnection tempMySqlConnection = new MySqlConnection($"server=localhost;user={username};password={pass}");
            // MySqlConnection tempMySqlConnection = new MySqlConnection($"server=localhost;user={user};password={pass}");
            try
            {
                tempMySqlConnection.Open();
                // MySqlCommand cmd1 = new MySqlCommand("DROP USER IF EXISTS 'tempuser'@'localhost';", tempMySqlConnection);
                MySqlCommand cmd2 = new MySqlCommand("CREATE USER IF NOT EXISTS 'tempuser'@'localhost' IDENTIFIED BY '123';", tempMySqlConnection);
                MySqlCommand cmd3 = new MySqlCommand("GRANT ALL PRIVILEGES ON *.* TO 'tempuser'@'localhost' WITH GRANT OPTION;", tempMySqlConnection);
                MySqlCommand cmd4 = new MySqlCommand("FLUSH PRIVILEGES;", tempMySqlConnection);
                // MySqlCommand cmd4 = new MySqlCommand("SHOW DATABASE LIKE logs_MM_test;", tempMySqlConnection);
                // MySqlCommand cmd5 = new MySqlCommand("CREATE DATABASE IF NOT EXISTS logs_MM_test;", tempMySqlConnection);

                Console.WriteLine("Connection Open...");
                // cmd1.ExecuteNonQuery();
                Console.WriteLine("DROP");
                cmd2.ExecuteNonQuery();
                Console.WriteLine("GRANT");
                cmd3.ExecuteNonQuery();
                Console.WriteLine("FLUSH");
                cmd4.ExecuteNonQuery();
                Console.WriteLine("CREATE");
                // cmd5.ExecuteNonQuery();
                Console.WriteLine("Temp User Created...");
                tempMySqlConnection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exited Program with Exit " + e.Message);
            }
            EstablishMariaDBConnection();
        }

        public bool EstablishMariaDBConnection()
        {
            Dictionary<string, string> informationLog = new Dictionary<string, string>();

            //string? databaseName = Console.ReadLine();
            // MySqlConnection mySqlConnection;
            // This is a hardcoded string, it will be different based on your naming
            // Need to generalize the database name or create a new database and run the restore sql file on it
            
            /** ROOT CONNECTION PASSWORD IS DIFFERENT FOR EVERYONE!!! PLEASE CHANGE*/

            string connectionString = $"server=localhost;user=root;database=motomotousermanagement;port=3306;password=123456;";

            try
            {
                mySqlConnection = new MySqlConnection(connectionString);
                mySqlConnection.Open();
                
                Console.WriteLine("Connection open");

                // Console.WriteLine("Close");
                //mySqlConnection.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("ERROR - Creating new user...");
                BuildTempUser();
            }
             
            return false;
        }

        public bool SelectAccount()
        {
            if (!EstablishMariaDBConnection()) Console.WriteLine("Connection failed to open...");
            else Console.WriteLine("Connection opened...");

            MySqlCommand command = new MySqlCommand(this.query, mySqlConnection);
            if (command.ExecuteNonQuery() == 1)
            {
                mySqlConnection!.Close();
                Console.WriteLine("Connection closed...");
                return true;
            } 
            else
            {
                mySqlConnection!.Close();
                Console.WriteLine("Connection closed...");
                return false;
            }
            
        }
        public Dictionary<string, string> GetAccountInformation()
        {
            if (!EstablishMariaDBConnection())
            {
                Console.WriteLine("Connection failed to open...");
                return new Dictionary<string, string>();
            }
            else Console.WriteLine("Connection opened...");

            MySqlCommand command = new MySqlCommand(this.query, this.mySqlConnection);

            MySqlDataReader myReader;
            myReader = command.ExecuteReader();

            Dictionary<string, string> accountInfo = new Dictionary<string, string>();
            while (myReader.Read())
            {
                accountInfo.Add("typeName", myReader.GetString("typeName"));
                accountInfo.Add("userId", myReader.GetString("userId"));
                accountInfo.Add("username", myReader.GetString("username"));
                accountInfo.Add("password", myReader.GetString("password"));
                accountInfo.Add("email", myReader.GetString("email"));
            }
            return accountInfo;
        }
    }
}