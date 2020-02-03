using GTSport_DT.Countries;
using GTSport_DT.General;
using Npgsql;
using System;

namespace GTSport_DT.Manufacturers
{
    /// <summary>The validations when saving / deleting a manufacturer.</summary>
    /// <seealso cref="GTSport_DT.General.Validation{GTSport_DT.Manufacturers.Manufacturer}"/>
    public class ManufacturerValidation : Validation<Manufacturer>
    {
        private CountriesRepository countriesRepository;
        private ManufacturersRepository manufacturersRepository;

        /// <summary>Initializes a new instance of the <see cref="ManufacturerValidation"/> class.</summary>
        /// <param name="npgsqlConnection">The NPGSQL connection.</param>
        public ManufacturerValidation(NpgsqlConnection npgsqlConnection) : base(npgsqlConnection)
        {
            manufacturersRepository = new ManufacturersRepository(npgsqlConnection);
            countriesRepository = new CountriesRepository(npgsqlConnection);
        }

        /// <summary>Validations done to an entity for the passed primary key before it is deleted.</summary>
        /// <param name="primaryKey">The primary key of the entity to delete.</param>
        /// <exception cref="GTSport_DT.Manufacturers.ManufacturerNotFoundException">
        /// When the manufacturer can not be found to be deleted.
        /// </exception>
        /// <exception cref="GTSport_DT.Manufacturers.ManufacturerInUseException">
        /// When the manufacture is in use with a car record.
        /// </exception>
        public override void ValidateDelete(string primaryKey)
        {
            Manufacturer manufacturer = manufacturersRepository.GetById(primaryKey);

            if (manufacturer == null)
            {
                throw new ManufacturerNotFoundException(ManufacturerNotFoundException.ManufacturerKeyNotFoundMsg, primaryKey);
            }

            if (false)
            {
                throw new ManufacturerInUseException(ManufacturerInUseException.ManufacturerInUseCanNotBeDeletedCarMsg);
            }
        }

        /// <summary>Validations done to an entity before it is saved.</summary>
        /// <param name="validateEntity">The entity to validate.</param>
        /// <exception cref="GTSport_DT.Manufacturers.ManufacturerNameNotSetException">
        /// When the name is not filled.
        /// </exception>
        /// <exception cref="GTSport_DT.Manufacturers.ManufacturerCountryKeyNotSetException">
        /// When the country key is not filled.
        /// </exception>
        /// <exception cref="GTSport_DT.Manufacturers.ManufacturerNameAlreadyExistsException">
        /// When the name already exists for another manufacturer.
        /// </exception>
        /// <exception cref="CountryNotFoundException">
        /// When the country key is not found in the country table.
        /// </exception>
        public override void ValidateSave(Manufacturer validateEntity)
        {
            if (String.IsNullOrWhiteSpace(validateEntity.Name))
            {
                throw new ManufacturerNameNotSetException(ManufacturerNameNotSetException.ManufacturerNameNotSetMsg);
            }

            if (String.IsNullOrWhiteSpace(validateEntity.CountryKey))
            {
                throw new ManufacturerCountryKeyNotSetException(ManufacturerCountryKeyNotSetException.ManufacturerCountryKeyNotSetMsg);
            }

            Manufacturer manufacturer = manufacturersRepository.GetByName(validateEntity.Name);

            if (manufacturer != null)
            {
                if (String.IsNullOrWhiteSpace(validateEntity.PrimaryKey)
                    || manufacturer.PrimaryKey != validateEntity.PrimaryKey)
                {
                    throw new ManufacturerNameAlreadyExistsException(ManufacturerNameAlreadyExistsException.ManufacturerNameAlreadyExistsMsg);
                }
            }

            Country country = countriesRepository.GetById(validateEntity.CountryKey);

            if (country == null)
            {
                throw new CountryNotFoundException(CountryNotFoundException.CountryKeyNotFoundMsg, validateEntity.CountryKey);
            }
        }
    }
}