using GTSport_DT.General;
using Npgsql;
using System.Collections.Generic;
using System.Data;
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

            FillDataSet();
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
            Owner owner = GetByFieldString(name, "ownname");

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

        /// <summary>Convert a record retrieved from the database to an entity object.</summary>
        /// <param name="dataReader">The database reader with the results from a database request.</param>
        /// <returns>A new entity with the data.</returns>
        protected override Owner RecordToEntity(NpgsqlDataReader dataReader)
        {
            Owner owner = new Owner();
            owner.PrimaryKey = dataReader.GetString(dataReader.GetOrdinal("ownkey"));
            owner.OwnerName = dataReader.GetString(dataReader.GetOrdinal("ownname"));
            owner.DefaultOwner = dataReader.GetBoolean(dataReader.GetOrdinal("owndefault"));

            return owner;
        }

        /// <summary>Updates the passed data row.</summary>
        /// <param name="dataRow">The data row to be updated.</param>
        /// <param name="entity">The entity to update from.</param>
        protected override void UpdateRow(ref DataRow dataRow, Owner entity)
        {
            dataRow["ownkey"] = entity.PrimaryKey;
            dataRow["ownname"] = entity.OwnerName;
            dataRow["owndefault"] = entity.DefaultOwner;
        }
    }
}