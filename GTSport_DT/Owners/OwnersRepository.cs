using GTSport_DT.General;
using Npgsql;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("GTSport_DT_Testing")]

namespace GTSport_DT.Owners
{
    /// <summary>The repository class for the owner table.</summary>
    public class OwnersRepository : SQLRespository<Owner>
    {
        /// <summary>Initializes a new instance of the <see cref="OwnersRepository"/> class.</summary>
        /// <param name="npgsqlConnection">The NPGSQL connection.</param>
        public OwnersRepository(NpgsqlConnection npgsqlConnection) : base(npgsqlConnection)
        {
            tableName = "OWNERS";
            idField = "ownkey";
            getListOrderByField = "ownname";
            updateCommand = "UPDATE owners SET ownname = @name, owndefault = @default WHERE ownkey = @pk";
            insertCommand = "INSERT INTO owners(ownkey, ownname, owndefault) VALUES (@pk, @name, @default)";
        }

        /// <summary>Gets a list of all the owners set to be the default owner.</summary>
        /// <returns>A list of owner entities that are set to default.</returns>
        public List<Owner> GetAllDefaultOwners()
        {
            List<Owner> owners = new List<Owner>();

            var cmd = new NpgsqlCommand();

            cmd.Connection = npgsqlConnection;
            cmd.CommandText = "SELECT * FROM owners WHERE owndefault = true";

            NpgsqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                var owner = RecordToEntity(dataReader);

                owners.Add(owner);
            }

            dataReader.Close();

            cmd.Dispose();

            return owners;
        }

        /// <summary>Gets an owner entity using a name.</summary>
        /// <param name="name">The name of a owner to look for.</param>
        /// <returns>An owner entity if found or null if not found.</returns>
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
                owner = RecordToEntity(dataReader);
            }

            dataReader.Close();

            cmd.Dispose();

            return owner;
        }

        /// <summary>Gets the first owner it finds that is set to be the default.</summary>
        /// <returns>The default owner found or null if one is not found.</returns>
        public Owner GetDefaultOwner()
        {
            Owner owner = null;

            var cmd = new NpgsqlCommand();

            cmd.Connection = npgsqlConnection;
            cmd.CommandText = "SELECT * FROM owners WHERE owndefault = true";

            NpgsqlDataReader dataReader = cmd.ExecuteReader();

            if (dataReader.Read())
            {
                owner = RecordToEntity(dataReader);
            }

            dataReader.Close();

            cmd.Dispose();
            return owner;
        }

        /// <summary>Adds the parameters.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="saveRecord">The save record.</param>
        protected override void AddParameters(ref NpgsqlCommand cmd, Owner saveRecord)
        {
            cmd.Parameters.AddWithValue("pk", saveRecord.PrimaryKey);
            cmd.Parameters.AddWithValue("name", saveRecord.OwnerName);
            cmd.Parameters.AddWithValue("default", saveRecord.DefaultOwner);
        }

        /// <summary>Convert a record retrieved from the database to an entity object.</summary>
        /// <param name="dataReader">The database reader with the results from a database request.</param>
        /// <returns>A new entity with the data.</returns>
        protected override Owner RecordToEntity(NpgsqlDataReader dataReader)
        {
            Owner owner = new Owner();
            owner.PrimaryKey = dataReader.GetString(0);
            owner.OwnerName = dataReader.GetString(1);
            owner.DefaultOwner = dataReader.GetBoolean(2);

            return owner;
        }
    }
}