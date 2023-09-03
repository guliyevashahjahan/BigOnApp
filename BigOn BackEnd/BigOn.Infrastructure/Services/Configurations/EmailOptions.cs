namespace BigOn.Infrastructure.Services.Configurations
{
    public class EmailOptions
    {
        public string DisplayName { get; set; }
        public string SmtpServer { get; set; }
        public short SmtpPort { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }
    }
}
