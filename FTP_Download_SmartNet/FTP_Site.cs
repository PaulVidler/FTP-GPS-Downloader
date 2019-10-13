namespace FTP_Download_SmartNet
{
    class FTP_Site
    {
        public string Host { get; set; }
        public string User { get; set; }
        public string Password { get; set; }

        public FTP_Site(string host, string user, string password)
        {
            this.Host = host;
            this.User = user;
            this.Password = password;
        }

    }
}
