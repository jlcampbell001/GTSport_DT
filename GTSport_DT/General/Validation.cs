using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTSport_DT.General
{
    // --------------------------------------------------------------------------------
    /// <summary>
    /// The base class for validations.
    /// </summary>
    // --------------------------------------------------------------------------------
    public abstract class Validation<T>
    {
        protected NpgsqlConnection npgsqlConnection;

        protected Validation(NpgsqlConnection npgsqlConnection)
        {
            this.npgsqlConnection = npgsqlConnection ?? throw new ArgumentNullException(nameof(npgsqlConnection));
        }

        // ********************************************************************************
        /// <summary>
        /// Validations done to an entity before it is saved.
        /// </summary>
        /// <param name="validateEntity">The entity to validate.</param>
        // ********************************************************************************
        public abstract void ValidateSave(T validateEntity);

        // ********************************************************************************
        /// <summary>Validations done to an entity for the passed primary key before it is deleted.
        /// 
        /// </summary>
        /// <param name="primaryKey">The primary key of the entity to delete.</param>
        // ********************************************************************************
        public abstract void ValidateDelete(string primaryKey);
    }
}
