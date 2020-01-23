using GTSport_DT.General;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("GTSport_DT_Testing")]
namespace GTSport_DT.Owners
{
    class OwnersRepository : SQLRespository<Owner>
    {
        public OwnersRepository(NpgsqlConnection npgsqlConnection) : base(npgsqlConnection)
        {
            tableName = "owners";
            idField = "ownkey";
            updateCommand = "UPDATE owners SET ownname = @name, owndefault = @default WHERE ownkey = @pk";
            insertCommand = "INSERT INTO owners(ownkey, ownname, owndefault) VALUES (@pk, @name, @default)";
        }
        protected override Owner RecordToObject(NpgsqlDataReader dataReader)
        {
            Owner owner = new Owner();
            owner.PrimaryKey = dataReader.GetString(0);
            owner.OwnerName = dataReader.GetString(1);
            owner.DefaultOwner = dataReader.GetBoolean(2);

            return owner;
        }

        protected override void AddParameters(ref NpgsqlCommand cmd, Owner saveRecord)
        {
            cmd.Parameters.AddWithValue("pk", saveRecord.PrimaryKey);
            cmd.Parameters.AddWithValue("name", saveRecord.OwnerName);
            cmd.Parameters.AddWithValue("default", saveRecord.DefaultOwner);
        }

        public Owner GetByName(string name)
        {
            Owner owner = null;

            var cmd = new NpgsqlCommand();

            cmd.Connection = npgsqlConnection;
            cmd.CommandText = "SELECT * FROM owners WHERE ownname = @name";

            cmd.Parameters.AddWithValue("name", name);
            cmd.Prepare();

            NpgsqlDataReader dataReader = cmd.ExecuteReader();

            if (dataReader.Read())
            {
                owner = RecordToObject(dataReader);
            }

            dataReader.Close();

            return owner;
        }

        public Owner GetDefaultOwner()
        {
            Owner owner = null;

            var cmd = new NpgsqlCommand();

            cmd.Connection = npgsqlConnection;
            cmd.CommandText = "SELECT * FROM owners WHERE owndefault = true";

            NpgsqlDataReader dataReader = cmd.ExecuteReader();

            if (dataReader.Read())
            {
                owner = RecordToObject(dataReader);
            }

            dataReader.Close();

            return owner;
        }

        public List<Owner> GetAllDefaultOwners()
        {
            List<Owner> owners = new List<Owner>();

            var cmd = new NpgsqlCommand();

            cmd.Connection = npgsqlConnection;
            cmd.CommandText = "SELECT * FROM owners WHERE owndefault = true";

            NpgsqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                var owner = RecordToObject(dataReader);

                owners.Add(owner);
            }

            dataReader.Close();

            return owners;
        }

        public string GetMaxKey()
        {
            string maxKey = "";

            var cmd = new NpgsqlCommand();

            cmd.Connection = npgsqlConnection;
            cmd.CommandText = "SELECT max(ownkey) as maxkey FROM owners";

            NpgsqlDataReader dataReader = cmd.ExecuteReader();

            if (dataReader.Read())
            {
                maxKey = dataReader.GetString(0);
            }

            dataReader.Close();

            return maxKey;
        }

    }
}
