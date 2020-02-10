using Npgsql;
using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("GTSport_DT_Testing")]

namespace GTSport_DT.General.KeySequence
{
    /// <summary>This is the service for the key sequence table.</summary>
    public class KeySequenceService
    {
        private KeySequenceRepository keySequenceRepository;
        private NpgsqlConnection npgsqlConnection;

        /// <summary>Initializes a new instance of the <see cref="KeySequenceService"/> class.</summary>
        /// <param name="npgsqlConnection">The NPGSQL connection.</param>
        /// <exception cref="ArgumentNullException">npgsqlConnection</exception>
        public KeySequenceService(NpgsqlConnection npgsqlConnection)
        {
            this.npgsqlConnection = npgsqlConnection ?? throw new ArgumentNullException(nameof(npgsqlConnection));

            keySequenceRepository = new KeySequenceRepository(npgsqlConnection);
        }

        /// <summary>Gets the next primary key for the passed table.</summary>
        /// <param name="tableName">The table to get the key for.</param>
        /// <param name="keyPrefix">The prefix used to create the primary key passed back.</param>
        /// <returns>A primary key for the table.</returns>
        public String GetNextKey(string tableName, String keyPrefix)
        {
            int keyValue = GetNextKeyValue(tableName);

            string primaryKey = keyPrefix + keyValue.ToString().PadLeft(9, '0');

            return primaryKey;
        }

        /// <summary>Sets the key value in the key sequence table for the passed table.</summary>
        /// <param name="tableName">The table to set the key value for.</param>
        /// <param name="keyValue">This is the key value to set to.</param>
        public void ResetKeyValue(string tableName, int keyValue)
        {
            KeySequence keySequence = GetCreateKeySequence(tableName);

            keySequence.LastKeyValue = keyValue;

            keySequenceRepository.SaveAndFlush(keySequence);
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

        private int GetNextKeyValue(string tableName)
        {
            KeySequence keySequence = GetCreateKeySequence(tableName);

            keySequence.LastKeyValue++;

            keySequenceRepository.SaveAndFlush(keySequence);

            return keySequence.LastKeyValue;
        }
    }
}