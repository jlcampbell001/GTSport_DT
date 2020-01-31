using GTSport_DT.General;
using GTSport_DT.Regions;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTSport_DT.Countries
{
    public class CountryValidation : Validation<Country>
    {
        private CountriesRepository countriesRespository;
        private RegionsRepository regionsRepository;

        public CountryValidation(NpgsqlConnection npgsqlConnection) : base(npgsqlConnection)
        {
            countriesRespository = new CountriesRepository(npgsqlConnection);
            regionsRepository = new RegionsRepository(npgsqlConnection);
        }

        public override void ValidateDelete(string primaryKey)
        {
            Country country = countriesRespository.GetById(primaryKey);

            if (country == null)
            {
                throw new CountryNotFoundException(CountryNotFoundException.CountryKeyNotFoundMsg, primaryKey);
            }
        }

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
                    throw new CountryDescriptionAlreadyExistsException(CountryDescriptionAlreadyExistsException.CountryDesciprtionAlreadyExistsMsg, validateEntity.Description);
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
