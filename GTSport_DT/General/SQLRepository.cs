using Npgsql;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace GTSport_DT.General
{
    /// <summary>The base class for to control data to and from a table.</summary>
    /// <typeparam name="T">The entity for the table.</typeparam>
    public abstract class SQLRespository<T>
    {
        /// <summary>The table name. Put it in capitals as a preference.</summary>
        public string tableName = "";

        /// <summary>The get list order by field if the list is requested to be ordered.</summary>
        protected string getListOrderByField = "";

        /// <summary>The primary key field for the table.</summary>
        protected string idField = "";

        /// <summary>
        /// <para>The insert command to add a record to the table.</para>
        /// <para>Should be setup as follows:</para>
        /// <para>INSERT INTO table name(field list) VALUES (@param list)</para>
        /// </summary>
        protected string insertCommand = "";

        /// <summary>The NPGSQL connection to the database.</summary>
        protected NpgsqlConnection npgsqlConnection;

        /// <summary>The name of the the field in the entity for the primary key.</summary>
        protected string primaryKeyObject = "PrimaryKey";

        /// <summary>
        /// <para>The update command to update a record in the table.</para>
        /// <para>Should be setup as follows:</para>
        /// <para>
        /// "UPDATE table name SET field1 = @param1, field2 = @param2 WHERE primary key = @pk"
        /// </para>
        /// </summary>
        protected string updateCommand = "";

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="SQLRespository{T}"/> class.</para>
        /// <para>Fill in the following fields:</para>
        /// <para>tableName.</para>
        /// <para>idField.</para>
        /// <para>primaryKeyObject (Optional as is defaults to PrimaryKey).</para>
        /// <para>updateCommand.</para>
        /// <para>insertCommand.</para>
        /// <para>getListOrderByField.</para>
        /// </summary>
        /// <param name="npgsqlConnection">The NPGSQL connection.</param>
        /// <exception cref="ArgumentNullException">npgsqlConnection</exception>
        protected SQLRespository(NpgsqlConnection npgsqlConnection)
        {
            this.npgsqlConnection = npgsqlConnection ?? throw new ArgumentNullException(nameof(npgsqlConnection));
        }

        /// <summary>Will delete the record from the table with the related id.</summary>
        /// <param name="id">The primary key to delete.</param>
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

        /// <summary>Retrieves an record from the table that is related to the passed id.</summary>
        /// <param name="id">The primary key to look up.</param>
        /// <returns>The entity for the found record or null if it was not found.</returns>
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

        /// <summary>Gets a list of all the records in the table.</summary>
        /// <param name="orderedList"></param>
        /// <returns>A list of entities from the table.</returns>
        public virtual List<T> GetList(Boolean orderedList = false)
        {
            List<T> records = new List<T>();

            var cmd = new NpgsqlCommand();

            cmd.Connection = npgsqlConnection;
            cmd.CommandText = "SELECT * FROM " + tableName;

            if (orderedList)
            {
                if (String.IsNullOrWhiteSpace(getListOrderByField))
                {
                    getListOrderByField = idField;
                }

                cmd.CommandText = cmd.CommandText + " ORDER BY " + getListOrderByField;
            }

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

        /// <summary>Gets the highest key value from the table.</summary>
        /// <returns>The highest key.</returns>
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

        /// <summary>
        /// Saves an entity as a record to the table.
        /// <para>
        /// If a record exists the Update command will be used other wise the Insert command will be used.
        /// </para>
        /// <para>
        /// Related Variables: <br/>
        /// - updateCommand <br/> - insertCommand
        /// </para>
        /// </summary>
        /// <param name="saveEntity">The entity to record to the table.</param>
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

        /// <summary>Adds parameters to a SQL command object based on the passed entity.</summary>
        /// <param name="cmd">The SQL command object to update.</param>
        /// <param name="entity">The entity to get data from.</param>
        protected abstract void AddParameters(ref NpgsqlCommand cmd, T entity);

        /// <summary>Convert a record retrieved from the database to an entity object.</summary>
        /// <param name="dataReader">The database reader with the results from a database request.</param>
        /// <returns>A new entity with the data.</returns>
        protected abstract T RecordToEntity(NpgsqlDataReader dataReader);
    }
}