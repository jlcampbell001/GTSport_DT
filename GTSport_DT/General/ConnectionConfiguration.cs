using System;
using System.Configuration;
using System.Security;
using static GTSport_DT.General.EncryptStrings;

namespace GTSport_DT.General
{
    /// <summary>A class to hold the connection configuration values.</summary>
    public class ConnectionConfiguration
    {
        /// <summary>Gets or sets the database.</summary>
        /// <value>The database.</value>
        public string Database { get; set; }

        /// <summary>Gets or sets the database host.</summary>
        /// <value>The host.</value>
        public string Host { get; set; }

        /// <summary>Gets or sets the password.</summary>
        /// <value>The password.</value>
        public SecureString Password { get; set; }

        /// <summary>Gets or sets the port.</summary>
        /// <value>The port.</value>
        public string Port { get; set; }

        /// <summary>Gets or sets the name of the user.</summary>
        /// <value>The name of the user.</value>
        public string UserName { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionConfiguration"/> class.
        /// </summary>
        public ConnectionConfiguration()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionConfiguration"/> class.
        /// </summary>
        /// <param name="host">The host to the database.</param>
        /// <param name="port">The port.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="database">The database name.</param>
        /// <exception cref="ArgumentNullException">
        /// host or port or userName or password or database
        /// </exception>
        public ConnectionConfiguration(string host, string port, string userName, SecureString password, string database)
        {
            Host = host ?? throw new ArgumentNullException(nameof(host));
            Port = port ?? throw new ArgumentNullException(nameof(port));
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
            Password = password ?? throw new ArgumentNullException(nameof(password));
            Database = database ?? throw new ArgumentNullException(nameof(database));
        }

        /// <summary>Gets the connection string to be used to connect to a database.</summary>
        /// <returns>The string to use to connect to a database.</returns>
        public string GetConnectionString()
        {
            string cs = "Host = " + Host
                + "; Port = " + Port
                + "; Username = " + UserName
                + "; Password = " + ToInsecureString(Password)
                + "; Database = " + Database;
            return cs;
        }

        /// <summary>Determines whether this instance is empty.</summary>
        /// <returns><c>true</c> if this instance is empty; otherwise, <c>false</c>.</returns>
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

        /// <summary>Loads the connection configuration from the configuration file..</summary>
        public void LoadConnectionConfig()
        {
            ConfigurationManager.RefreshSection("appSettings");

            Host = ConfigurationManager.AppSettings.Get("Host");
            Port = ConfigurationManager.AppSettings.Get("Port");
            UserName = ConfigurationManager.AppSettings.Get("Username");
            Password = DecryptString(ConfigurationManager.AppSettings.Get("Password"));
            Database = ConfigurationManager.AppSettings.Get("Database");
        }

        /// <summary>Saves the connection configuration to the configuration file..</summary>
        public void SaveConnectionConfig()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Clear();

            config.AppSettings.Settings.Add("Host", Host);
            config.AppSettings.Settings.Add("Port", Port);
            config.AppSettings.Settings.Add("Username", UserName);
            config.AppSettings.Settings.Add("Password", EncryptString(Password));
            config.AppSettings.Settings.Add("Database", Database);

            config.Save(ConfigurationSaveMode.Modified);
        }
    }
}