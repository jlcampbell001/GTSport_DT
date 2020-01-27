using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace GTSport_DT_Testing.General
{
    public abstract class TestBase
    {
        protected static string cs = "Host=localhost;Port=5433;Username=GTSport;Password=GTSport;Database=GTSport_Test";
        protected static NpgsqlConnection con;

    }
}
