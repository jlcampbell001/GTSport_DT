using GTSport_DT.Countries;
using GTSport_DT.General;
using Npgsql;
using System;
using System.Collections.Generic;

namespace GTSport_DT.Regions
{
    /// <summary>The validations to save / delete a region.</summary>
    public class RegionValidation : Validation<Region>
    {
        private CountriesRepository countriesRespository;
        private RegionsRepository regionsRepository;

        /// <summary>Initializes a new instance of the <see cref="RegionValidation"/> class.</summary>
        /// <param name="npgsqlConnection">The NPGSQL connection.</param>
        public RegionValidation(NpgsqlConnection npgsqlConnection) : base(npgsqlConnection)
        {
            regionsRepository = new RegionsRepository(npgsqlConnection);
            countriesRespository = new CountriesRepository(npgsqlConnection);
        }

        /// <summary>The validations for a delete.</summary>
        /// <param name="primaryKey">The primary key of the region to delete.</param>
        /// <exception cref="RegionNotFoundException">
        /// Happens when the primary key can not be found.
        /// </exception>
        /// <exception cref="RegionInUseException">Happens when the regions is used in a country.</exception>
        public override void ValidateDelete(string primaryKey)
        {
            Region region = regionsRepository.GetById(primaryKey);

            if (region == null)
            {
                throw new RegionNotFoundException(RegionNotFoundException.RegionKeyNotFoundMsg, primaryKey);
            }

            List<Country> countries = countriesRespository.GetListForRegionKey(primaryKey);

            if (countries.Count > 0)
            {
                throw new RegionInUseException(RegionInUseException.RegionInUseCanNotBeDeletedCountryMsg);
            }
        }

        /// <summary>The validations for a save.</summary>
        /// <param name="validateEntity">The region to check.</param>
        /// <exception cref="RegionDescriptionNotSetException">The region must be filled.</exception>
        /// <exception cref="RegionDescriptionAlreadyExistsException">
        /// The region descriptions must be unique.
        /// </exception>
        /// "
        public override void ValidateSave(Region validateEntity)
        {
            if (String.IsNullOrWhiteSpace(validateEntity.Description))
            {
                throw new RegionDescriptionNotSetException(RegionDescriptionNotSetException.regionDescriptionNotSetMsg);
            }

            Region existingRegion = regionsRepository.GetByDescription(validateEntity.Description);

            if (existingRegion != null)
            {
                if (String.IsNullOrWhiteSpace(validateEntity.PrimaryKey)
                    || validateEntity.PrimaryKey != existingRegion.PrimaryKey)
                {
                    throw new RegionDescriptionAlreadyExistsException(RegionDescriptionAlreadyExistsException.RegionDescriptionAlreadyExistsMsg, validateEntity.Description);
                }
            }
        }
    }
}