namespace TheNewPanelists.ServiceLayer.Authentication 
{
    interface IAuthenticationService
    {
        bool validateRequest();
        bool SqlGenerator();
        string CreateOTP();
        void SendEmail(string otp);
    }
}