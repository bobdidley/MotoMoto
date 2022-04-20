﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewPanelists.MotoMoto.DataAccess
{
    public class DirectMessageDataAccess
    {
        private string _connectionString = "server=localhost;user=root;database=motomoto_um;port=3306;password=password;";
        
        public bool IsValidUser(string username)
        {
            MySqlConnection connection = new MySqlConnection(_connectionString);
            try
            {
                connection.Open();
                Console.WriteLine("Connection Open");
                string query = "SELECT * FROM Profile p WHERE p.username = '" + username + "'";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows) 
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return false;
            }
            finally 
            {
                connection.Close();
            }

        }

        private List<int> getSenderReceiverID(string sender, string receiver)
        {
            MySqlConnection connection = new MySqlConnection(_connectionString);
            List<int> senderReceiverId = new List<int>();
            try
            {
                connection.Open();
                Console.WriteLine("Connection Open");
                string getSenderUserIdQuery = "SELECT userId FROM Profile p WHERE p.username = '" + sender + "'";
                MySqlCommand cmd = new MySqlCommand(getSenderUserIdQuery, connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    senderReceiverId.Add(reader.GetInt32(0));
                    reader.Close();
                }

                string getReceiverUserIdQuery = "SELECT userId FROM Profile p WHERE p.username = '" + receiver + "'";
                cmd = new MySqlCommand(getReceiverUserIdQuery, connection);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    senderReceiverId.Add(reader.GetInt32(0));
                    reader.Close();
                }
                reader.Close();
   
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return senderReceiverId;
        }

        private bool NoDuplicateMessage(string sender, string receiver)
        {
            List<int> ids = getSenderReceiverID(sender, receiver);
            if (ids.Count != 2)
            {
                return false;
            }
            int senderId = ids.ElementAt(0);
            int receiverId = ids.ElementAt(1);
            MySqlConnection connection = new MySqlConnection(_connectionString);
            try
            {
                connection.Open();
                Console.WriteLine("Connection Open");
                string query = "SELECT * FROM MESSAGEHISTORY mh WHERE (mh.senderId = '" + senderId + "' AND mh.receiverId = '" + receiverId + "') OR (mh.senderId = '" + receiverId + "' AND mh.receiverId = '" + senderId + "');";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    return false;
                }
                reader.Close();
                return true;
            }
            catch(Exception ex)
            {
                Console.Write(ex.Message);
                return false;
            }
            finally
            {
                
                connection.Close();
            }
        }

        public bool CreateNewDirectMessage(string sender, string receiver)
        {
            List<int> ids = getSenderReceiverID(sender, receiver);
            if(ids.Count != 2)
            {
                return false;
            }
            int senderId = ids.ElementAt(0);
            int receiverId = ids.ElementAt(1);
            if (!NoDuplicateMessage(sender, receiver))
            {
                return false;
            }

            MySqlConnection connection = new MySqlConnection(_connectionString);
            try
            {
                connection.Open();
                Console.WriteLine("Connection Open");
                string query = "INSERT INTO MESSAGEHISTORY (senderID, receiverID, request) VALUES (" + senderId + "," + receiverId + ", true);";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                return true;
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }
            

        }
            
        public bool SendMessage(string sender, string receiver, string message)
        {
            int messageHistoryId = -1;
            List<int> ids = getSenderReceiverID(sender, receiver);
            if (ids.Count != 2)
            {
                return false;
            }
            int senderId = ids.ElementAt(0);
            int receiverId = ids.ElementAt(1);

            MySqlConnection connection = new MySqlConnection(_connectionString);
            try
            {
                connection.Open();
                Console.WriteLine("Connection Open");
                string query = "SELECT messageHistoryId FROM MESSAGEHISTORY mh WHERE senderID = '" + senderId + "' AND receiverID = '" + receiverId + "';";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    messageHistoryId = reader.GetInt32(0);
                    reader.Close();
                }
                else
                {
                    //missing a history imput
                    return false;
                }

                query = "INSERT INTO MESSAGES (userHistoryId,messageHistoryId, messages) VALUES ('" + sender + "','" + messageHistoryId + "' , '" + message + "');";
                cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
    
        public List<string> GetMessageHistory(string sender)
        {
            List<string> userMessaged = new List<string>();
            List<int> ids = getSenderReceiverID(sender, "");
            int senderId = ids.ElementAt(0);

            MySqlConnection connection = new MySqlConnection(_connectionString);
            try
            {
                connection.Open();
                Console.WriteLine("Connection Open");
                string query = "SELECT username, request FROM Profile p INNER JOIN MESSAGEHISTORY mh ON p.userID = mh.receiverId WHERE mh.senderID = '" + senderId + "';";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetBoolean(1) == false)
                    {
                        userMessaged.Add(reader.GetString(0));
                    }
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            finally
            {
                connection.Close();
            }
            return userMessaged;
        }

        public List<List<string>> GetMessages(string sender, string receiver)
        {  
            List<List<string>>allMessages = new List<List<string>>();
            List<string>messages = new List<string>();

            List<int> ids = getSenderReceiverID(sender, receiver);
            int senderId = ids.ElementAt(0);
            int receiverId = ids.ElementAt(1);
            MySqlConnection connection = new MySqlConnection(_connectionString);
            try
            {
                connection.Open();
                string query = "SELECT P.USERNAME, M.MESSAGES,M.TIMESTAMP FROM MESSAGES M " +
                    "INNER JOIN MESSAGEHISTORY MH ON M.MESSAGEHISTORYID = MH.MESSAGEHISTORYID " +
                    "INNER JOIN PROFILE P ON MH.SENDERID = P.USERID " +
                    "WHERE MH.SENDERID = '" + senderId + "'AND MH.RECEIVERID = '" + receiverId +"'" +
                    "UNION  " +
                    "SELECT P.USERNAME, M.MESSAGES,M.TIMESTAMP FROM MESSAGES M " +
                    "INNER JOIN MESSAGEHISTORY MH ON M.MESSAGEHISTORYID = MH.MESSAGEHISTORYID " +
                    "INNER JOIN PROFILE P ON MH.SENDERID = P.USERID " +
                    "WHERE MH.SENDERID = '" + receiverId + "'" + " AND MH.RECEIVERID = '" + senderId + "' " +
                    "ORDER BY TIMESTAMP;";
                MySqlCommand cmd = new MySqlCommand( query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    messages.Add(reader.GetString(0));
                    messages.Add(reader.GetString(1));
                    messages.Add(reader.GetString(2));
                    allMessages.Add(messages);
                    messages = new List<string>();
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                connection.Close();
            }
            return allMessages;

        }
        
        public List<string>GetRequest(string currentUser)
        {
            List<string> requests = new List<string>();
            List<int> ids = getSenderReceiverID(currentUser, "");
            int currentUserId = ids.ElementAt(0);

            MySqlConnection connection = new MySqlConnection(_connectionString);
            try
            {
                connection.Open();
                string query = "SELECT P.USERNAME FROM MESSAGEHISTORY MH " +
                    "INNER JOIN PROFILE P ON P.USERID = MH.SENDERID " +
                    "WHERE MH.RECEIVERID = '" + currentUserId + "' AND MH.REQUEST = TRUE;";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    requests.Add(reader.GetString(0)); 
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                connection.Close();
            }
            return requests;

        }
   
        public bool AcceptRequest(string sender, string receiver)
        {
            List<int> ids = getSenderReceiverID(sender, receiver);
            if (ids.Count != 2)
            {
                return false;
            }
            int senderId = ids.ElementAt(0);
            int receiverId = ids.ElementAt(1);

            MySqlConnection connection = new MySqlConnection(_connectionString);
            try
            {
                connection.Open();
                string query = "UPDATE MESSAGEHISTORY mh SET mh.request = false WHERE mh.senderID = '" + senderId + "' AND mh.receiverID = '"+receiverId + "';";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();

                query = "INSERT INTO MESSAGEHISTORY (senderId, receiverId, request) VALUES ('" + receiverId + "','" + senderId + "', false);";
                cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
            finally
            {
                connection.Close();
            }

        }

        public bool DeclineRequest(string sender, string receiver)
        {
            List<int> ids = getSenderReceiverID(sender, receiver);
            if (ids.Count != 2)
            {
                return false;
            }
            int senderId = ids.ElementAt(0);
            int receiverId = ids.ElementAt(1);

            MySqlConnection connection = new MySqlConnection(_connectionString);
            try
            {
                connection.Open();
                string query = "DELETE FROM MESSAGEHISTORY WHERE senderId = '" + senderId + "' AND receiverId = '" + receiverId + "';";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}