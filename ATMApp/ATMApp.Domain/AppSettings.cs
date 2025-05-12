namespace ATMApp.Domain
{
    public class AppSettings
    {
        public ConnectionStrings connectionStrings {  get; set; } = new ConnectionStrings();
    }

    public class ConnectionStrings
    {
        public string SqlConnectionString { get; set; } = string.Empty;
    }
}

