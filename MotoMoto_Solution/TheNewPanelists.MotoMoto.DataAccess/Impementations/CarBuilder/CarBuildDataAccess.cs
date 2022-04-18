﻿using System;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using TheNewPanelists.MotoMoto.Models;
using System.Data;
using System.Data.SqlClient;

namespace TheNewPanelists.MotoMoto.DataAccess.Implementations.CarBuilder
{
    public class CarBuildDataAccess : IDataAccess
    {

        private MySqlConnection? mySqlConnection { get; set; }

        private string _connectionString = "server=localhost;user=dev_moto;database=dev_UM;port=3306;password=motomoto;"; //write config so this only appears once

        public CarBuildDataAccess() { }

        public CarBuildDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool EstablishMariaDBConnection()
        {
            try
            {
                mySqlConnection = new MySqlConnection(_connectionString);
                mySqlConnection.Open();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        private bool ExecuteQuery(MySqlCommand command)
        {
            if (command.ExecuteNonQuery() == 1)
            {
                mySqlConnection!.Close();
                return true;
            }
            mySqlConnection!.Close();
            return false;
        }

        public bool InsertNewDataStoreCarTypeEntity(DataStoreCarType carType)
        {
            if (!EstablishMariaDBConnection())
            {
                throw new NullReferenceException();
            }

            using (MySqlCommand command = new MySqlCommand())
            {
                command.Transaction = mySqlConnection!.BeginTransaction();
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                command.Connection = mySqlConnection!;
                command.CommandType = CommandType.Text;

                command.CommandText = $"INSERT INTO USER (carID, make, model, country, year)" +     // Do not pass carID 
                                      $"VALUES (@v0, @v1, @v2, @v3, @v4)";
                var parameters = new MySqlParameter[5];
                parameters[0] = new MySqlParameter("@v0", carType!.carID);     // Should be removed because you do not need this if auto-incrementing
                parameters[1] = new MySqlParameter("@v1", carType!.make);
                parameters[2] = new MySqlParameter("@v2", carType!.model);
                parameters[3] = new MySqlParameter("@v3", carType!.country);
                parameters[4] = new MySqlParameter("@v4", carType!.year);

                command.Parameters.AddRange(parameters);
                return (ExecuteQuery(command));
            }
        }
    }
}
