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
    // --------------------------------------------------------------------------------
    /// <summary>
    /// This is the service for the key sequence table.
    /// </summary>
    // --------------------------------------------------------------------------------
    public class KeySequenceService
    {
        private NpgsqlConnection npgsqlConnection;

        private KeySequenceRepository keySequenceRepository;

        public KeySequenceService(NpgsqlConnection npgsqlConnection)
        {
            this.npgsqlConnection = npgsqlConnection ?? throw new ArgumentNullException(nameof(npgsqlConnection));

            keySequenceRepository = new KeySequenceRepository(npgsqlConnection);
        }

        // ********************************************************************************
        /// <summary>
        /// Gets the next primary key for the passed table.
        /// </summary>
        /// <param name="tableName">The table to get the key for.</param>
        /// <param name="keyPrefix">The prefix used to create the primary key passed back.</param>
        /// <returns>A primary key for the table.</returns>
        // ********************************************************************************
        public String GetNextKey(string tableName, String keyPrefix)
        {
            int keyValue = GetNextKeyValue(tableName);

            string primaryKey = keyPrefix + keyValue.ToString().PadLeft(9, '0');

            return primaryKey;
        }

        // ********************************************************************************
        /// <summary>
        /// Sets the key value in the key sequence table for the passed table.
        /// </summary>
        /// <param name="tableName">The table to set the key value for.</param>
        /// <param name="keyValue">This is the key value to set to.</param>
        // ********************************************************************************
        public void ResetKeyValue(string tableName, int keyValue)
        {
            KeySequence keySequence = GetCreateKeySequence(tableName);

            keySequence.LastKeyValue = keyValue;

            keySequenceRepository.Save(keySequence);
        }

        // ********************************************************************************
        /// <summary>
        /// Retrieves the next key value for the passed table.
        /// </summary>
        /// <param name="tableName">The table to get the key value for.</param>
        /// <returns>The next key value.</returns>
        // ********************************************************************************
        private int GetNextKeyValue(string tableName)
        {
            KeySequence keySequence = GetCreateKeySequence(tableName);

            keySequence.LastKeyValue++;

            keySequenceRepository.Save(keySequence);

            return keySequence.LastKeyValue;
        }

        // ********************************************************************************
        /// <summary>
        /// Creates a new key sequence entity for the passed table name.
        /// </summary>
        /// <param name="tableName">The table to create a new entity for.</param>
        /// <returns>The created key sequence entity.</returns>
        // ********************************************************************************
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
