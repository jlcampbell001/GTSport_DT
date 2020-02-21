using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using static GTSport_DT.General.EncryptStrings;

namespace GTSport_DT.General
{
    public class ConnectionConfiguration
    {
        public ConnectionConfiguration()
        {
        }

        public ConnectionConfiguration(string host, string port, string userName, SecureString password, string database)
        {
            Host = host ?? throw new ArgumentNullException(nameof(host));
            Port = port ?? throw new ArgumentNullException(nameof(port));
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
            Password = password ?? throw new ArgumentNullException(nameof(password));
            Database = database ?? throw new ArgumentNullException(nameof(database));
        }

        public string Host { get; set; }
        public string Port { get; set; }
        public string UserName { get; set; }
        public SecureString Password { get; set; }
        public string Database { get; set; }

        public string GetConnectionString()
        {
            string cs = "Host = " + Host
                + "; Port = " + Port
                + "; Username = " + UserName
                + "; Password = " + ToInsecureString(Password)
                + "; Database = " + Database;
            return cs;
        }

        public Boolean IsEmpty()
        {
            Boolean empty = false;
            
            if (String.IsNullOrEmpty(Host) 
                || String.IsNullOrEmpty(Port)
                || String.IsNullOrEmpty(UserName)
                || String.IsNullOrEmpty(ToInsecureString(Password))
                || String.IsNullOrEmpty(Database))
            {
                empty = true;
            }

            return empty;
        }

        public void SaveConnectionConfig()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Clear();

            config.AppSettings.Settings.Add("Host", Host);
            config.AppSettings.Settings.Add("Port", Port);
            config.AppSettings.Settings.Add("Username", UserName);
            config.AppSettings.Settings.Add("Password",EncryptString(Password));
            config.AppSettings.Settings.Add("Database", Database);

            config.Save(ConfigurationSaveMode.Modified);
        }

        public void LoadConnectionConfig()
        {
            ConfigurationManager.RefreshSection("appSettings");

            Host = ConfigurationManager.AppSettings.Get("Host");
            Port = ConfigurationManager.AppSettings.Get("Port");
            UserName = ConfigurationManager.AppSettings.Get("Username");
            Password = DecryptString(ConfigurationManager.AppSettings.Get("Password"));
            Database = ConfigurationManager.AppSettings.Get("Database");
        }
    }
}
