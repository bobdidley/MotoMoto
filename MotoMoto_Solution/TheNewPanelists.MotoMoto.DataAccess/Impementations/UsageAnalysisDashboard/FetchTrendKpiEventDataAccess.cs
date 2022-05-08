using MySql.Data.MySqlClient;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using System;

namespace TheNewPanelists.MotoMoto.DataAccess
{
    public class FetchTrendKpiEventDataAccess : MariaDBConnectionBase, IFetchKpiDataAccess
    {
        private MySqlConnection? _mySqlConnection;
        private string _connectionString;

        public FetchTrendKpiEventDataAccess()
        {
            // Use App.config here
            // Development
            // _connectionString = "server=localhost;user=dev_moto;database=dev_UAD;port=3306;password=motomoto;";
            // Production
            _connectionString = "server=moto-moto.crd4iyvrocsl.us-west-1.rds.amazonaws.com;user=dev_moto;database=pro_moto;port=3306;password=motomoto;";
        }

        /// <summary>
        /// Refine the chart metrics from the MySqlDataReader into an IUsageAnalyticEntity 
        /// </summary>
        /// <param name="reader">MySqlDataReader</param>
        /// <param name="analyticModel">IUsageAnalyticModel</param>
        /// <returns>IUsageAnalyticEntity</returns>
        private IUsageAnalyticEntity RefineData(MySqlDataReader reader, IUsageAnalyticModel analyticModel)
        {
            if (reader.HasRows)
            {
                IEnumerable<IAxisDetailsEntity> metrics = new List<IAxisDetailsEntity>();
                try
                {
                    while (reader.Read())
                    {
                        string x = Convert.ToDateTime(reader["eventDate"]).ToString("MM/dd/yyyy");
                        string y = reader.GetInt32("eventTotal").ToString();
                        IAxisDetailsEntity metric = new DataStoreAxisDetails(x, y);
                        ((List<IAxisDetailsEntity>)metrics).Add(metric);
                    }
                    reader.Close();
                }
                catch (Exception)
                {
                    reader.Close();
                    throw new Exception("Failed to Add Result");
                }
                IUsageAnalyticEntity analytic = new DataStoreUsageAnalyticTrend(analyticModel.x_axis, analyticModel.y_axis, metrics);
                return analytic;
            }
            throw new Exception("No Rows Found");
        }

        /// <summary>
        /// Fetch the Trend Chart metrics from Event Analytics
        /// </summary>
        /// <param name="analyticModel">IUsageAnalyticModel</param>
        /// <returns>IUsageAnalyticEntity</returns>
        public IUsageAnalyticEntity FetchChartMetrics(IUsageAnalyticModel analyticModel)
        {
            // Establish MariaDB Connection
            try
            {
                if (_mySqlConnection != null && _mySqlConnection.State == System.Data.ConnectionState.Open)
                    _mySqlConnection.Close();
                _mySqlConnection = MariaDBConnectionBase.EstablishConnection(_connectionString);
                _mySqlConnection.Open();
            }
            catch (Exception)
            {
                throw new Exception("Failed to Process Request");
            }

            // Need Justin to update tables to test if this query works
            // This query selects past events which could be deleted, may be better to query for events 3 months forward
            // string commandText = "SELECT eventDate, COUNT(*) as eventTotal FROM EventDetails WHERE eventDate >= NOW() - INTERVAL 3 MONTH  AND eventDate < NOW() GROUP BY eventDate ORDER BY eventDate DESC;";
            string commandText = "SELECT str_to_date(eventDate,'%m/%d/%Y') AS eventDate, COUNT(*) AS eventTotal FROM EventDetails WHERE str_to_date(eventDate,'%m/%d/%Y') > NOW() GROUP BY eventDate ORDER BY eventDate DESC;";
            using (MySqlCommand command = new MySqlCommand(commandText, _mySqlConnection))
            {
                command.Transaction = _mySqlConnection.BeginTransaction();
                try
                {
                    IUsageAnalyticEntity result = RefineData(command.ExecuteReader(), analyticModel);
                    return result;
                    //foreach (var item in (List<IAxisDetailsEntity>)result.metricList)
                    //{
                    //Console.WriteLine(item.xData);
                    //Console.WriteLine(item.yData);
                    //}
                }
                catch (Exception)
                {
                    //Console.WriteLine(e.Message);
                    throw new Exception("Failed to Process Request");
                }
            }
        }
    }
}