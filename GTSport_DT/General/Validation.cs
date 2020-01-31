using Npgsql;
using System;

namespace GTSport_DT.General
{
    /// <summary>The base class for validations.</summary>
    /// <typeparam name="T">The entity to preform validations against.</typeparam>
    public abstract class Validation<T>
    {
        /// <summary>The NPGSQL connection to the database.</summary>
        protected NpgsqlConnection npgsqlConnection;

        /// <summary>Initializes a new instance of the <see cref="Validation{T}"/> class.</summary>
        /// <param name="npgsqlConnection">The NPGSQL connection.</param>
        /// <exception cref="ArgumentNullException">npgsqlConnection</exception>
        protected Validation(NpgsqlConnection npgsqlConnection)
        {
            this.npgsqlConnection = npgsqlConnection ?? throw new ArgumentNullException(nameof(npgsqlConnection));
        }

        /// <summary>Validations done to an entity for the passed primary key before it is deleted.</summary>
        /// <param name="primaryKey">The primary key of the entity to delete.</param>
        public abstract void ValidateDelete(string primaryKey);

        /// <summary>Validations done to an entity before it is saved.</summary>
        /// <param name="validateEntity">The entity to validate.</param>
        public abstract void ValidateSave(T validateEntity);
    }
}