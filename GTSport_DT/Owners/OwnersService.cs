using GTSport_DT.General.KeySequence;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("GTSport_DT_Testing")]
namespace GTSport_DT.Owners
{
    class OwnersService
    {
        public const string DefaultOwnerName = "DEFAULT";

        private const string PrimaryKeyPrefix = "OWN";

        private NpgsqlConnection npgsqlConnection;

        private KeySequenceService keySequenceService;

        private OwnersRepository ownersRepository;

        private OwnerValidation ownerValidation;

        public OwnersService(NpgsqlConnection npgsqlConnection)
        {
            this.npgsqlConnection = npgsqlConnection ?? throw new ArgumentNullException(nameof(npgsqlConnection));
            keySequenceService = new KeySequenceService(npgsqlConnection);
            ownersRepository = new OwnersRepository(npgsqlConnection);
            ownerValidation = new OwnerValidation(npgsqlConnection);
        }

        public Owner GetOwnerByKey(string primaryKey)
        {
            Owner owner = ownersRepository.GetById(primaryKey);

            if (owner == null)
            {
                throw new OwnerNotFoundException(OwnerNotFoundException.OwnerKeyNotFoundMsg, primaryKey);
            }

            return owner;
        }

        public Owner GetOwnerByName(String name)
        {
            Owner owner = ownersRepository.GetByName(name);

            if (owner == null)
            {
                throw new OwnerNotFoundException(OwnerNotFoundException.OwnerNameNotFoundMsg, name);
            }

            return owner;
        }

        public Owner GetDefaultOwner()
        {
            Owner defaultOwner = ownersRepository.GetDefaultOwner();

            if (defaultOwner == null)
            {
                List<Owner> owners = ownersRepository.GetList();
                if (owners.Count == 1)
                {
                    defaultOwner = owners[0];
                    defaultOwner.DefaultOwner = true;
                } else
                {
                    defaultOwner = ownersRepository.GetByName(DefaultOwnerName);

                    if (defaultOwner == null)
                    {
                        defaultOwner = new Owner();
                        defaultOwner.OwnerName = DefaultOwnerName;
                    }

                    defaultOwner.DefaultOwner = true;
                }

                SaveOwner(ref defaultOwner);
            }

            return defaultOwner;
        }

        public void SaveOwner(ref Owner owner)
        {
            //validate owner
            ownerValidation.ValidateSave(owner);

            if (String.IsNullOrWhiteSpace(owner.PrimaryKey))
            {
                owner.PrimaryKey = keySequenceService.GetNextKey(ownersRepository.tableName, PrimaryKeyPrefix);
            }

            ownersRepository.Save(owner);

            // make sure there is only one default
            if (owner.DefaultOwner)
            {
                ClearOtherDefaultUsers(owner);
            }
        }

        public void DeleteOwner(string primaryKey)
        {
            // validate owner
            ownerValidation.ValidateDelete(primaryKey);

            ownersRepository.Delete(primaryKey);
        }

        public void ResetKey()
        {
            string maxKey = ownersRepository.GetMaxKey();

            int maxKeyValue = 0;

            if (!String.IsNullOrWhiteSpace(maxKey))
            {
                maxKeyValue = int.Parse(maxKey.Substring(3));
            }

            keySequenceService.ResetKeyValue(ownersRepository.tableName, maxKeyValue);
        }

        private void ClearOtherDefaultUsers(Owner currentDefaultOwner)
        {
            List<Owner> owners = ownersRepository.GetAllDefaultOwners();

            foreach(Owner owner in owners)
            {
                if (owner.PrimaryKey != currentDefaultOwner.PrimaryKey)
                {
                    owner.DefaultOwner = false;

                    ownersRepository.Save(owner);
                }
            }
        }
    }
}
