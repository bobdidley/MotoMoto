namespace TheNewPanelists.MotoMoto.Models
{
    public class NotificationSystemRequestModel
    {
        public string? username { get; set; }  
        public string? notificationType { get; set; }
        //public string? notificationType { get; set; } 
        //public string? password { get; }

        public NotificationSystemRequestModel() {}

        public NotificationSystemRequestModel(string username, string type)
        {
            this.username = username;
            this.notificationType = type;
            //this.notificationType = type;
            //this.password = password;
        }
        
    }
}
