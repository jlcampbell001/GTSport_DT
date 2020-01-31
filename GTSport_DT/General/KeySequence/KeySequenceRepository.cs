using Npgsql;
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
            updateCommand = "UPDATE keysequence SET lastkeyvalue = @keyvalue WHERE tablename = @name";
            insertCommand = "INSERT INTO keysequence(tablename, lastkeyvalue) VALUES (@name, @keyvalue)";
        }

        protected override void AddParameters(ref NpgsqlCommand cmd, KeySequence saveRecord)
        {
            cmd.Parameters.AddWithValue("name", saveRecord.TableName);
            cmd.Parameters.AddWithValue("keyvalue", saveRecord.LastKeyValue);
        }

        protected override KeySequence RecordToEntity(NpgsqlDataReader dataReader)
        {
            KeySequence keySequence = new KeySequence();
            keySequence.TableName = dataReader.GetString(0);
            keySequence.LastKeyValue = dataReader.GetInt32(1);

            return keySequence;
        }
    }
}