using GTSport_DT.General;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("GTSport_DT_Testing")]

namespace GTSport_DT.Owners
{
    /// <summary>The service for the owners table.</summary>
    public class OwnersService : BackEndService<Owner, OwnersRepository, OwnerValidation, OwnerNotFoundException>
    {
        /// <summary>The default owner name.</summary>
        public const string DefaultOwnerName = "DEFAULT";

        /// <summary>Initializes a new instance of the <see cref="OwnersService"/> class.</summary>
        /// <param name="npgsqlConnection">The NPGSQL connection.</param>
        public OwnersService(NpgsqlConnection npgsqlConnection) : base(npgsqlConnection)
        {
            primaryKeyPrefix = "OWN";
            keyNotFoundMessage = OwnerNotFoundException.OwnerKeyNotFoundMsg;
        }

        /// <summary>Gets an owner that has the name passed.</summary>
        /// <param name="name">The name to look for.</param>
        /// <returns>The owner entity that was found.</returns>
        /// <exception cref="OwnerNotFoundException">
        /// Thrown is the owner can not be found with the passed name.
        /// </exception>
        public Owner GetByName(String name)
        {
            Owner owner = repository.GetByName(name);

            if (owner == null)
            {
                throw new OwnerNotFoundException(OwnerNotFoundException.OwnerNameNotFoundMsg, name);
            }

            return owner;
        }

        /// <summary>
        /// Gets the default owner.
        /// <para>
        /// If there is no default owner set it will first check is there is a record with the
        /// default owner name and set that to the default owner.
        /// </para>
        /// <para>Or it will create a new default owner record.</para>
        /// </summary>
        /// <returns>The default owner entity that was found or created.</returns>
        public Owner GetDefaultOwner()
        {
            Owner defaultOwner = repository.GetDefaultOwner();

            if (defaultOwner == null)
            {
                List<Owner> owners = repository.GetList();
                if (owners.Count == 1)
                {
                    defaultOwner = owners[0];
                    defaultOwner.DefaultOwner = true;
                }
                else
                {
                    defaultOwner = repository.GetByName(DefaultOwnerName);

                    if (defaultOwner == null)
                    {
                        defaultOwner = new Owner();
                        defaultOwner.OwnerName = DefaultOwnerName;
                    }

                    defaultOwner.DefaultOwner = true;
                }

                Save(ref defaultOwner);
            }

            return defaultOwner;
        }

        /// <summary>
        /// Saves an owner entity to the table.
        /// <para>If the primary key was not filled it will be filled with the next key.</para>
        /// <para>
        /// If the owner is set to be the default owner, it will make a call to clear any other
        /// owners set to be the default.
        /// </para>
        /// </summary>
        /// <param name="owner">The owner entity to save.</param>
        public override void Save(ref Owner owner)
        {
            //validate owner
            validation.ValidateSave(owner);

            if (String.IsNullOrWhiteSpace(owner.PrimaryKey))
            {
                owner.PrimaryKey = keySequenceService.GetNextKey(repository.tableName, primaryKeyPrefix);
            }

            repository.SaveAndFlush(owner);

            // make sure there is only one default
            if (owner.DefaultOwner)
            {
                ClearOtherDefaultUsers(owner);
            }
        }

        private void ClearOtherDefaultUsers(Owner currentDefaultOwner)
        {
            List<Owner> owners = repository.GetAllDefaultOwners();

            foreach (Owner owner in owners)
            {
                if (owner.PrimaryKey != currentDefaultOwner.PrimaryKey)
                {
                    owner.DefaultOwner = false;

                    repository.Save(owner);
                }
            }

            repository.Flush();
        }
    }
}