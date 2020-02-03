using GTSport_DT.General;
using GTSport_DT.Manufacturers;
using GTSport_DT.Regions;
using Npgsql;
using System;
using System.Collections.Generic;

namespace GTSport_DT.Countries
{
    /// <summary>The validation class for a country record.</summary>
    /// <seealso cref="GTSport_DT.General.Validation{GTSport_DT.Countries.Country}"/>
    public class CountryValidation : Validation<Country>
    {
        private CountriesRepository countriesRespository;
        private RegionsRepository regionsRepository;
        private ManufacturersRepository manufacturersRepository;

        /// <summary>Initializes a new instance of the <see cref="CountryValidation"/> class.</summary>
        /// <param name="npgsqlConnection">The NPGSQL connection.</param>
        public CountryValidation(NpgsqlConnection npgsqlConnection) : base(npgsqlConnection)
        {
            countriesRespository = new CountriesRepository(npgsqlConnection);
            regionsRepository = new RegionsRepository(npgsqlConnection);
            manufacturersRepository = new ManufacturersRepository(npgsqlConnection);
        }

        /// <summary>Validations done to an country for the passed primary key before it is deleted.</summary>
        /// <param name="primaryKey">The primary key of the country to delete.</param>
        /// <exception cref="GTSport_DT.Countries.CountryNotFoundException">When the country can not be found to be deleted.</exception>
        /// <exception cref="GTSport_DT.Countries.CountryInUseException">When the country is used in one or more manufactures and the user tries to delete.</exception>
        public override void ValidateDelete(string primaryKey)
        {
            Country country = countriesRespository.GetById(primaryKey);

            if (country == null)
            {
                throw new CountryNotFoundException(CountryNotFoundException.CountryKeyNotFoundMsg, primaryKey);
            }

            List<Manufacturer> manufacturers = manufacturersRepository.GetListForCountryKey(primaryKey);

            if (manufacturers.Count > 0)
            {
                throw new CountryInUseException(CountryInUseException.CountryInUseCanNotBeDeletedManufacturerMsg);
            }
        }

        /// <summary>Validations done to a country before it is saved.</summary>
        /// <param name="validateEntity">The country to validate.</param>
        /// <exception cref="GTSport_DT.Countries.CountryDescriptionNotSetException">
        /// If the description is not filled.
        /// </exception>
        /// <exception cref="GTSport_DT.Countries.CountryRegionKeyNotSetException">
        /// If the region key is not filled.
        /// </exception>
        /// <exception cref="GTSport_DT.Countries.CountryDescriptionAlreadyExistsException">
        /// If the description already exists in another country record.
        /// </exception>
        /// <exception cref="RegionNotFoundException">
        /// If the region key is not found in the region table.
        /// </exception>
        public override void ValidateSave(Country validateEntity)
        {
            if (String.IsNullOrWhiteSpace(validateEntity.Description))
            {
                throw new CountryDescriptionNotSetException(CountryDescriptionNotSetException.CountryDescriptionNotSetMsg);
            }

            if (String.IsNullOrWhiteSpace(validateEntity.RegionKey))
            {
                throw new CountryRegionKeyNotSetException(CountryRegionKeyNotSetException.CountryRegionKeyNotSetMsg);
            }

            Country country = countriesRespository.GetByDescription(validateEntity.Description);

            if (country != null)
            {
                if (String.IsNullOrWhiteSpace(validateEntity.PrimaryKey) || validateEntity.PrimaryKey != country.PrimaryKey)
                {
                    throw new CountryDescriptionAlreadyExistsException(CountryDescriptionAlreadyExistsException.CountryDescriptionAlreadyExistsMsg, validateEntity.Description);
                }
            }

            Region region = regionsRepository.GetById(validateEntity.RegionKey);

            if (region == null)
            {
                throw new RegionNotFoundException(RegionNotFoundException.RegionKeyNotFoundMsg, validateEntity.RegionKey);
            }
        }
    }
}