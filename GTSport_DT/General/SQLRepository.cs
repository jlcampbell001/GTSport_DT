using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
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

        /// <summary>The NPGSQL connection to the database.</summary>
        protected NpgsqlConnection npgsqlConnection;
        protected NpgsqlDataAdapter dataAdapter;

        /// <summary>The name of the field in the entity for the primary key.</summary>
        protected string primaryKeyObject = "PrimaryKey";

        private DataSet dataSet = new DataSet();

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

        protected void SetupAdapter()
        {
            dataAdapter = new NpgsqlDataAdapter("Select * FROM " + tableName + " ORDER BY " + idField, npgsqlConnection);

            NpgsqlCommandBuilder builder = new NpgsqlCommandBuilder(dataAdapter);

            var insertCmd = builder.GetInsertCommand(true);
            insertCmd.Connection = npgsqlConnection;
            //insertCmd.CommandText = insertCommand;
            dataAdapter.InsertCommand = insertCmd;

            var updateCmd = builder.GetUpdateCommand(true);
            updateCmd.Connection = npgsqlConnection;
            //updateCmd.CommandText = updateCommand;
            dataAdapter.UpdateCommand = updateCmd;

            var deleteCmd = builder.GetDeleteCommand(true);
            deleteCmd.Connection = npgsqlConnection;
            //deleteCmd.CommandText = "DELETE FROM " + tableName + " WHERE " + idField + " = @pk";
            //deleteCmd.Parameters.Add("pk", NpgsqlTypes.NpgsqlDbType.Varchar, 255, sourceColumn: idField);
            dataAdapter.DeleteCommand = deleteCmd;
        }

        /// <summary>Will delete the record from the table with the related id.</summary>
        /// <param name="id">The primary key to delete.</param>
        public virtual void Delete(string id)
        {
            if (dataSet.Tables[tableName] == null) 
            {
                FillDataSet();
            }

            DataTable dataTable = dataSet.Tables[tableName];

            DataRow foundRow = dataTable.Rows.Find(id);

            if (foundRow != null)
            {
                foundRow.Delete();
            }            
        }

        public virtual void Flush()
        {
            if (dataAdapter == null)
            {
                SetupAdapter();
            }

            if (dataSet.HasChanges())
            {
                dataAdapter.Update(dataSet, tableName);
            }

            FillDataSet();
        }

        public virtual void Refresh()
        {
            FillDataSet();
        }

        public virtual void DeleteAndFlush(string id)
        {
            Delete(id);

            Flush();
        }

        /// <summary>Retrieves a record from the table that is related to the passed id.</summary>
        /// <param name="id">The primary key to look up.</param>
        /// <returns>The entity for the found record or null if it was not found.</returns>
        public virtual T GetById(string id)
        {
            return GetByFieldString(id, idField);
        }

        /// <summary>  Retrieves a record from the table that has a match with the  passed string value in the field name passed.</summary>
        /// <param name="searchValue">The search value.</param>
        /// <param name="fieldName">Name of the field to match.</param>
        /// <returns>The entity record found or null if not found.</returns>
        public virtual T GetByFieldString(string searchValue, string fieldName)
        {
            T dataEntity = default;

            var cmd = new NpgsqlCommand();

            cmd.Connection = npgsqlConnection;
            cmd.CommandText = "SELECT * FROM " + tableName + " WHERE " + fieldName + " = @searchValue";

            cmd.Parameters.AddWithValue("searchValue", searchValue);
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
            List<T> records = GetListForFieldString(orderedList: orderedList);

            return records;
        }

        /// <summary>
        ///   <para>
        ///  The a list of entities where the value of the field name matches the search value.</para>
        ///   <para>If fieldName is not passed it will get all of the entities in the table instead.</para>
        ///   <para>If the orderByField is not passed it will default to the classes getListOrderByField if filled or the idField.</para>
        /// </summary>
        /// <param name="fieldName">Name of the field to search.</param>
        /// <param name="searchValue">The search value.</param>
        /// <param name="orderByField">The order by field.</param>
        /// <param name="orderedList">if set to <c>true</c> [ordered list].</param>
        /// <returns>A list of entities found in the table that matches the choices made.</returns>
        public virtual List<T> GetListForFieldString(string searchValue = "", string fieldName = "", string orderByField = "", Boolean orderedList = false)
        {
            if (String.IsNullOrWhiteSpace(orderByField))
            {
                orderByField = getListOrderByField;
            }

            List<T> records = new List<T>();

            var cmd = new NpgsqlCommand();

            cmd.Connection = npgsqlConnection;
            cmd.CommandText = "SELECT * FROM " + tableName;

            if (!String.IsNullOrWhiteSpace(fieldName))
            {
                cmd.CommandText += " WHERE " + fieldName + " = @searchValue ";
                cmd.Parameters.AddWithValue("searchValue", searchValue);
                cmd.Prepare();
            }

            if (orderedList)
            {
                if (String.IsNullOrWhiteSpace(orderByField))
                {
                    orderByField = idField;
                }

                cmd.CommandText += " ORDER BY " + orderByField;
            }
            else
            {
                cmd.CommandText += " ORDER BY " + idField;
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
            if (dataSet.Tables[tableName] == null)
            {
                FillDataSet();
            }

            DataTable dataTable = dataSet.Tables[tableName];

            PropertyInfo propertyInfo = saveEntity.GetType().GetProperty(primaryKeyObject);
            string id = propertyInfo.GetValue(saveEntity).ToString();

            DataRow saveRow = dataTable.Rows.Find(id);

            if (saveRow == null)
            {
                saveRow = dataTable.NewRow();
                UpdateRow(ref saveRow, saveEntity);
                dataTable.Rows.Add(saveRow);
            } else
            {
                UpdateRow(ref saveRow, saveEntity);
            }
        }

        public virtual void SaveAndFlush(T saveEntity)
        {
            Save(saveEntity);

            Flush();
        }

        /// <summary>Convert a record retrieved from the database to an entity object.</summary>
        /// <param name="dataReader">The database reader with the results from a database request.</param>
        /// <returns>A new entity with the data.</returns>
        protected abstract T RecordToEntity(NpgsqlDataReader dataReader);

        protected abstract void UpdateRow(ref DataRow dataRow, T entity);

        protected void FillDataSet()
        {
            if (dataAdapter == null)
            {
                SetupAdapter();
            }

            dataAdapter.FillSchema(dataSet, SchemaType.Source, tableName);
            dataAdapter.Fill(dataSet, tableName);
        }
    }
}