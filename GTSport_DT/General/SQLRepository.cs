using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GTSport_DT.General
{
    // --------------------------------------------------------------------------------
    /// <summary>
    /// The base class for to control data to and from a table.
    /// </summary>
    // --------------------------------------------------------------------------------
    public abstract class SQLRespository<T>
    {
        protected NpgsqlConnection npgsqlConnection;
        
        public string tableName = "";
        protected string idField = "";
        protected string primaryKeyObject = "PrimaryKey";
        protected string updateCommand = "";
        protected string insertCommand = "";

        protected SQLRespository(NpgsqlConnection npgsqlConnection)
        {
            this.npgsqlConnection = npgsqlConnection ?? throw new ArgumentNullException(nameof(npgsqlConnection));
        }

        // ********************************************************************************
        /// <summary>
        /// Convert a record retrieved from the database to an entity object.
        /// </summary>
        /// <param name="dataReader">The database reader with the results from a database request.</param>
        /// <returns>A new entity with the data.</returns>
        // ********************************************************************************
        protected abstract T RecordToEntity(NpgsqlDataReader dataReader);

        // ********************************************************************************
        /// <summary>
        /// Adds parameters to a SQL command object based on the passed entity.
        /// </summary>
        /// <param name="cmd">The SQL command object to update.</param>
        /// <param name="entity">The entity to get data from.</param>
        // ********************************************************************************
        protected abstract void AddParameters(ref NpgsqlCommand cmd, T entity);

        // ********************************************************************************
        /// <summary>
        /// Saves an entity as a record to the table.
        /// <para>If a record exists the Update command will be used other wise the Insert command will be used.</para>
        /// <para>Related Variables: </para>
        /// <list type="bullet">
        /// <item>
        /// updateCommand
        /// </item>
        /// <item>
        /// insertCommand
        /// </item>
        /// </summary>
        /// <param name="saveEntity">The entity to record to the table.</param>
        // ********************************************************************************
        public virtual void Save(T saveEntity)
        {
            var cmd = new NpgsqlCommand();

            cmd.Connection = npgsqlConnection;
            AddParameters(ref cmd, saveEntity);

            PropertyInfo propertyInfo = saveEntity.GetType().GetProperty(primaryKeyObject);
            string id = propertyInfo.GetValue(saveEntity).ToString();

            var findKey = GetById(id);

            if (findKey != null)
            {
                cmd.CommandText = updateCommand;
            }
            else
            {
                cmd.CommandText = insertCommand;
            }

            cmd.Prepare();
            
            cmd.ExecuteNonQuery();
        }

        // ********************************************************************************
        /// <summary>
        /// Will delete the record from the table with the related id.
        /// </summary>
        /// <param name="id">The primary key to delete.</param>
        // ********************************************************************************
        public virtual void Delete(string id)
        {
            var cmd = new NpgsqlCommand();
            cmd.Connection = npgsqlConnection;

            cmd.CommandText = "DELETE FROM " + tableName + " where " + idField + " = @pk";

            cmd.Parameters.AddWithValue("pk", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();
           
            cmd.Dispose();
        }

        // ********************************************************************************
        /// <summary>
        /// Retrieves an record from the table that is related to the passed id.
        /// </summary>
        /// <param name="id">The primary key to look up.</param>
        /// <returns>The entity for the found record or null if it was not found.</returns>
        // ********************************************************************************
        public virtual T GetById(string id)
        {
            T dataEntity = default;

            var cmd = new NpgsqlCommand();

            cmd.Connection = npgsqlConnection;
            cmd.CommandText = "SELECT * FROM " + tableName + " WHERE " + idField + " = @pk";

            cmd.Parameters.AddWithValue("pk", id);
            cmd.Prepare();

            NpgsqlDataReader dataReader = cmd.ExecuteReader();

            if (dataReader.Read())
            {
                dataEntity = RecordToEntity(dataReader);
            }

            dataReader.Close();
            cmd.Dispose();
            
            return dataEntity;
        }

        // ********************************************************************************
        /// <summary>
        /// Gets a list of all the records in the table.
        /// </summary>
        /// <returns>A list of entities from the table.</returns>
        // ********************************************************************************
        public virtual List<T> GetList()
        {
            List<T> records = new List<T>();

            var cmd = new NpgsqlCommand();

            cmd.Connection = npgsqlConnection;
            cmd.CommandText = "SELECT * FROM " + tableName;

            NpgsqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                T dataObject = RecordToEntity(dataReader);

                records.Add(dataObject);
            }

            dataReader.Close();
            cmd.Dispose();

            return records;
        }

        // ********************************************************************************
        /// <summary>
        /// Gets the highest key value from the table.
        /// </summary>
        /// <returns>The highest key.</returns>
        // ********************************************************************************
        public virtual string GetMaxKey()
        {
            string maxKey = "";

            var cmd = new NpgsqlCommand();

            cmd.Connection = npgsqlConnection;
            cmd.CommandText = "SELECT max(" + idField + ") as maxkey FROM " + tableName;

            NpgsqlDataReader dataReader = cmd.ExecuteReader();

            if (dataReader.Read())
            {
                maxKey = dataReader.GetString(0);
            }

            dataReader.Close();
            cmd.Dispose();

            return maxKey;
        }
    }
}
