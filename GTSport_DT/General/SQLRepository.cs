using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GTSport_DT.General
{
    public abstract class SQLRespository<T>
    {
        protected NpgsqlConnection npgsqlConnection;
        
        protected string tableName = "";
        protected string idField = "";
        protected string primaryKeyObject = "PrimaryKey";
        protected string updateCommand = "";
        protected string insertCommand = "";

        protected SQLRespository(NpgsqlConnection npgsqlConnection)
        {
            this.npgsqlConnection = npgsqlConnection ?? throw new ArgumentNullException(nameof(npgsqlConnection));
        }
        protected abstract T RecordToObject(NpgsqlDataReader dataReader);
        protected abstract void AddParameters(ref NpgsqlCommand cmd, T saveRecord);

        public virtual void Save(T saveRecord)
        {
            var cmd = new NpgsqlCommand();

            cmd.Connection = npgsqlConnection;
            AddParameters(ref cmd, saveRecord);

            PropertyInfo propertyInfo = saveRecord.GetType().GetProperty(primaryKeyObject);
            string id = propertyInfo.GetValue(saveRecord).ToString();

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

        public virtual void Delete(string id)
        {
            var cmd = new NpgsqlCommand();
            cmd.Connection = npgsqlConnection;

            cmd.CommandText = "DELETE FROM " + tableName + " where " + idField + " = @pk";

            cmd.Parameters.AddWithValue("pk", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();
        }

        public virtual T GetById(string id)
        {
            T dataObject = default;

            var cmd = new NpgsqlCommand();

            cmd.Connection = npgsqlConnection;
            cmd.CommandText = "SELECT * FROM " + tableName + " WHERE " + idField + " = @pk";

            cmd.Parameters.AddWithValue("pk", id);
            cmd.Prepare();

            NpgsqlDataReader dataReader = cmd.ExecuteReader();

            if (dataReader.Read())
            {
                dataObject = RecordToObject(dataReader);
            }

            dataReader.Close();

            return dataObject;
        }

        public virtual List<T> GetList()
        {
            List<T> records = new List<T>();

            var cmd = new NpgsqlCommand();

            cmd.Connection = npgsqlConnection;
            cmd.CommandText = "SELECT * FROM " + tableName;

            NpgsqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                T dataObject = RecordToObject(dataReader);

                records.Add(dataObject);
            }

            dataReader.Close();

            return records;
        }
    }
}
