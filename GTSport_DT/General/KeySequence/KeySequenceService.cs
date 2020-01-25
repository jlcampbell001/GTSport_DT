using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("GTSport_DT_Testing")]
namespace GTSport_DT.General.KeySequence
{
    class KeySequenceService
    {
        private NpgsqlConnection npgsqlConnection;

        private KeySequenceRepository keySequenceRepository;

        public KeySequenceService(NpgsqlConnection npgsqlConnection)
        {
            this.npgsqlConnection = npgsqlConnection ?? throw new ArgumentNullException(nameof(npgsqlConnection));

            keySequenceRepository = new KeySequenceRepository(npgsqlConnection);
        }

        public String GetNextKey(string tableName, String keyPrefix)
        {
            int keyValue = GetNextKeyValue(tableName);

            string primaryKey = keyPrefix + keyValue.ToString().PadLeft(9, '0');

            return primaryKey;
        }

        public void ResetKeyValue(string tableName, int keyValue)
        {
            KeySequence keySequence = GetCreateKeySequence(tableName);

            keySequence.LastKeyValue = keyValue;

            keySequenceRepository.Save(keySequence);
        }

        private int GetNextKeyValue(string tableName)
        {
            KeySequence keySequence = GetCreateKeySequence(tableName);

            keySequence.LastKeyValue++;

            keySequenceRepository.Save(keySequence);

            return keySequence.LastKeyValue;
        }

        private KeySequence GetCreateKeySequence(string tableName)
        {
            KeySequence keySequence = keySequenceRepository.GetById(tableName);

            if (keySequence == null)
            {
                keySequence = new KeySequence();
                keySequence.TableName = tableName;
                keySequence.LastKeyValue = 0;
            }

            return keySequence;
        }
    }
}
