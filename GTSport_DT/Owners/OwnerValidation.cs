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

    // --------------------------------------------------------------------------------
    /// <summary>
    /// Validations to save or delete an owner entity.
    /// </summary>
    // --------------------------------------------------------------------------------
    public class OwnerValidation : Validation<Owner>
    {
        private OwnersRepository ownersRepository;

        public OwnerValidation(NpgsqlConnection npgsqlConnection) : base(npgsqlConnection)
        {
            ownersRepository = new OwnersRepository(npgsqlConnection);
        }

        // ********************************************************************************
        /// <summary>
        /// Validations to delete an owner for the passed primary key.
        /// </summary>
        /// <param name="primaryKey">The primary key related to the owner to delete.</param>
        /// <exception cref="OwnerNotFoundException">The owner must exist before it can be deleted.</exception>
        // ********************************************************************************
        public override void ValidateDelete(string primaryKey)
        {
            Owner owner = ownersRepository.GetById(primaryKey);

            if (owner == null)
            {
                throw new OwnerNotFoundException(OwnerNotFoundException.OwnerKeyNotFoundMsg, primaryKey);
            }
        }

        // ********************************************************************************
        /// <summary>
        /// Validations to save the passed owner entity.
        /// </summary>
        /// <param name="validateEntity">The owner entity to validate.</param>
        /// <exception cref="OwnerNameNotSetException">The owner name must be filled to save.</exception>
        /// <exception cref="OwnerNameAlreadyExistsException">The owner name can not already exist when saving an owner for a different primary key.</exception>
        // ********************************************************************************
        public override void ValidateSave(Owner validateEntity)
        {
            if (String.IsNullOrWhiteSpace(validateEntity.OwnerName))
            {
                throw new OwnerNameNotSetException(OwnerNameNotSetException.OwnerNameNotSetMsg);
            }

            Owner existingOwner = ownersRepository.GetByName(validateEntity.OwnerName);

            if (existingOwner != null)
            {
                if (String.IsNullOrWhiteSpace(validateEntity.PrimaryKey) 
                    || !String.Equals(validateEntity.PrimaryKey, existingOwner.PrimaryKey)) {
                    throw new OwnerNameAlreadyExistsException(OwnerNameAlreadyExistsException.OwnerNameAlreadyExistsMsg, validateEntity.OwnerName);
                }
            }
        }
    }
}
