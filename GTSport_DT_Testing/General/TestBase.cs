using GTSport_DT.General;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using static GTSport_DT.General.EncryptStrings;

namespace GTSport_DT_Testing.General
{
    public abstract class TestBase
    {
        protected static string cs = GetConnectionString();
        protected static NpgsqlConnection con;

        public static string GetConnectionString()
        {
            ConnectionConfiguration connectionConfiguration = new ConnectionConfiguration();

            connectionConfiguration.LoadConnectionConfig();
            
            connectionConfiguration.Database = "GTSport_Test";
            /*
            connectionConfiguration.Host = "";
            connectionConfiguration.Port = "";
            connectionConfiguration.UserName = "";
            connectionConfiguration.Password = ToSecureString("");
            connectionConfiguration.SaveConnectionConfig();
            */

            return connectionConfiguration.GetConnectionString();
        }
    }
}
