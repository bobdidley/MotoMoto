using MySql.Data.MySqlClient;

namespace TheNewPanelists.ServiceLayer.Logging 
{
    class ArchiveService : IArchiveService 
    { 
        private string operation {get; set;}
        private List<Dictionary<string, string>> log {get; set;}
        private DateTime localDate{get;}

        public ArchiveService(string operation, List<Dictionary<string, string>> log) {
            this.operation = operation;
            this.log = log;
            this.localDate = DateTime.Now;
        }
        public List<string> SqlGenerator()
        {
            if (this.operation == "BUILD") 
            {
                this.operation = "INSERT";
                return BuildArchiveTable();
            }
            else if (this.operation == "INSERT")
            {
                return InsertArchiveInformation();
            }
            List<string> nullList = new List<string>();
            return nullList;
        }
        public List<string> BuildArchiveTable() 
        { 
            List<string> createTable = new List<string>();
            // string localdateDay = this.localDate.Date.ToString("d");
            // Console.WriteLine(localdateDay);
            // localdateDay = localdateDay.Replace("/","_");
            // string query = "CREATE TABLE IF NOT EXISTS "+localdateDay+" (logId INT NOT NULL, categoryName VARCHAR(100) NOT NULL, levelName VARCHAR(50) NOT NULL, "+
            //                 "timeStamp DATETIME NOT NULL, userID INT NOT NULL, DSCRIPTION VARCHAR(1000) NOT NULL, "+
            //                 "CONSTRAINT Log_PK PRIMARY KEY (logId)) ENGINE=InnoDB;";
            // createTable.Add(query);
            return createTable;
        }
        private List<string> InsertArchiveInformation() 
        {   
            List<string> storeArchive = new List<String>();

            string localdateDay = this.localDate.Date.ToString("d");

            localdateDay = localdateDay.Replace("/","_");

            for (int i = 0; i < log.Count; i++) {
                string query = @"INSERT INTO "+localdateDay+" VALUES ("+log[i]["logId"]+", '"+
                log[i]["levelName"]+"', '"+log[i]["categoryName"]+"', '"+log[i]["timeStamp"]+"', "+log[i]["userID"]+", '"+
                log[i]["DSCRIPTION"]+"');";
                Console.WriteLine(query);
                storeArchive.Add(query);
            }
            
            return storeArchive;
        }
    }
}