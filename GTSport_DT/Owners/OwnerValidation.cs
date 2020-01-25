using GTSport_DT.General;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("GTSport_DT_Testing")]
namespace GTSport_DT.Owners
{

    class OwnerValidation : Validation<Owner>
    {
        private OwnersRepository ownersRepository;

        public OwnerValidation(NpgsqlConnection npgsqlConnection) : base(npgsqlConnection)
        {
            ownersRepository = new OwnersRepository(npgsqlConnection);
        }

        public override void ValidateDelete(string primaryKey)
        {
            Owner owner = ownersRepository.GetById(primaryKey);

            if (owner == null)
            {
                throw new OwnerNotFoundException(OwnerNotFoundException.OwnerKeyNotFoundMsg, primaryKey);
            }
        }

        public override void ValidateSave(Owner validateRecord)
        {
            if (String.IsNullOrWhiteSpace(validateRecord.OwnerName))
            {
                throw new OwnerNameNotSetException(OwnerNameNotSetException.OwnerNameNoSetMsg);
            }

            Owner existingOwner = ownersRepository.GetByName(validateRecord.OwnerName);

            if (existingOwner != null)
            {
                if (String.IsNullOrWhiteSpace(validateRecord.PrimaryKey) 
                    || !String.Equals(validateRecord.PrimaryKey, existingOwner.PrimaryKey)) {
                    throw new OwnerNameAlreadyExistsException(OwnerNameAlreadyExistsException.OwnerNameAlreadyExistsMsg, validateRecord.OwnerName);
                }
            }
        }
    }
}
