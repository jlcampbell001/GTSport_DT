using Npgsql;
using System.Data;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("GTSport_DT_Testing")]

namespace GTSport_DT.General.KeySequence
{
    internal class KeySequenceRepository : SQLRespository<KeySequence>
    {
        public KeySequenceRepository(NpgsqlConnection npgsqlConnection) : base(npgsqlConnection)
        {
            tableName = "KEYSEQUENCE";
            idField = "tablename";
            primaryKeyObject = "TableName";

            FillDataSet();
        }

        protected override KeySequence RecordToEntity(NpgsqlDataReader dataReader)
        {
            KeySequence keySequence = new KeySequence();
            keySequence.TableName = dataReader.GetString(dataReader.GetOrdinal("tablename"));
            keySequence.LastKeyValue = dataReader.GetInt32(dataReader.GetOrdinal("lastkeyvalue"));

            return keySequence;
        }

        protected override void UpdateRow(ref DataRow dataRow, KeySequence entity)
        {
            dataRow["tablename"] = entity.TableName;
            dataRow["lastkeyvalue"] = entity.LastKeyValue;
        }
    }
}