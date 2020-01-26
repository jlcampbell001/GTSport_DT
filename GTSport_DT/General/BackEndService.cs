using GTSport_DT.General.KeySequence;
using Npgsql;
using System;

namespace GTSport_DT.General
{
    // --------------------------------------------------------------------------------
    /// <summary>
    /// The is the base class for a service that communicate with a table. 
    /// <para>T = The Entity for the table.</para>
    /// <para>U = The SQLRepository for the table.</para>
    /// <para>V = The Validation for the table.</para>
    /// <para>W = This is the no key found exception related to the table.</para>
    /// </summary>
    // --------------------------------------------------------------------------------
    public abstract class BackEndService<T, U, V, W> where T : Entity where U : SQLRespository<T> where V : Validation<T> where W : Exception
     {
        protected string primaryKeyPrefix = "";

        protected NpgsqlConnection npgsqlConnection;

        protected KeySequenceService keySequenceService;

        protected U repository;

        protected V validation;

        protected string keyNotFoundMessage = "";

        protected BackEndService(NpgsqlConnection npgsqlConnection)
        {
            this.npgsqlConnection = npgsqlConnection ?? throw new ArgumentNullException(nameof(npgsqlConnection));

            keySequenceService = new KeySequenceService(npgsqlConnection);

            repository = (U)Activator.CreateInstance(typeof(U), npgsqlConnection);

            validation = (V)Activator.CreateInstance(typeof(V), npgsqlConnection);
        }

        // ********************************************************************************
        /// <summary>
        /// Retrieves an entity from the table with the passed primary key.
        /// </summary>
        /// <param name="primaryKey">The primary key to look for.</param>
        /// <returns>The entity found.</returns>
        /// <exception>This will throw an exception if the key is not found.  The message is set in the keyNotFoundMessage parameter.</exception>
        // ********************************************************************************
        public virtual T GetByKey(string primaryKey)
        {
            T entity = repository.GetById(primaryKey);

            if (entity == null)
            {
                throw (W)Activator.CreateInstance(typeof(W), keyNotFoundMessage, primaryKey);
            }

            return entity;
        }

        // ********************************************************************************
        /// <summary>
        /// Will save the passed entity to the table.
        /// <para>If the primary key is not filled, a new primary key will be assigned.</para>
        /// <para>Will throw any exceptions set in the validation object.</para>
        /// </summary>
        /// <param name="entity">The entity to save.</param>
        // ********************************************************************************
        public virtual void Save(ref T entity)
        {
            //validate
            validation.ValidateSave(entity);

            if (String.IsNullOrWhiteSpace(entity.PrimaryKey))
            {
                entity.PrimaryKey = keySequenceService.GetNextKey(repository.tableName, primaryKeyPrefix);
            }

            repository.Save(entity);
        }

        // ********************************************************************************
        /// <summary>
        /// Will delete the record from the table for the passed primary key.
        /// <para>Will throw any exceptions set in the validation object.</para>
        /// </summary>
        /// <param name="primaryKey">The primary key to delete.</param>
        // ********************************************************************************
        public virtual void Delete(string primaryKey)
        {
            // validate
            validation.ValidateDelete(primaryKey);

            repository.Delete(primaryKey);
        }

        // ********************************************************************************
        /// <summary>
        /// Looks up the highest key in the table and set the last key in the key sequence table to that value for the table.
        /// </summary>
        // ********************************************************************************
        public virtual void ResetKey()
        {
            string maxKey = repository.GetMaxKey();

            int maxKeyValue = 0;

            if (!String.IsNullOrWhiteSpace(maxKey))
            {
                maxKeyValue = int.Parse(maxKey.Substring(primaryKeyPrefix.Length));
            }

            keySequenceService.ResetKeyValue(repository.tableName, maxKeyValue);
        }

    }
}
