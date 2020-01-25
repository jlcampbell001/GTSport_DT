using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTSport_DT.General
{
    abstract class Validation<T>
    {
        protected NpgsqlConnection npgsqlConnection;

        protected Validation(NpgsqlConnection npgsqlConnection)
        {
            this.npgsqlConnection = npgsqlConnection ?? throw new ArgumentNullException(nameof(npgsqlConnection));
        }

        public abstract void ValidateSave(T validateRecord);
        public abstract void ValidateDelete(string primaryKey);
    }
}
