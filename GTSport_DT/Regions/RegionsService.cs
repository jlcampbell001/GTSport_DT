using GTSport_DT.General;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTSport_DT.Regions
{
    // --------------------------------------------------------------------------------
    /// <summary>
    /// The service for the region table.
    /// </summary>
    // --------------------------------------------------------------------------------
    public class RegionsService : BackEndService<Region, RegionsRepository, RegionValidation, RegionNotFoundException>
    {

        public RegionsService(NpgsqlConnection npgsqlConnection) : base(npgsqlConnection)
        {
            primaryKeyPrefix = "REG";
            keyNotFoundMessage = RegionNotFoundException.RegionKeyNotFoundMsg;
        }

        // ********************************************************************************
        /// <summary>
        /// Get a region that matches the passed description.
        /// </summary>
        /// <param name="description">The description to look up.</param>
        /// <returns>The region found.</returns>
        /// <exception cref="RegionNotFoundException">If it can not found the region by the description.</exception>
        // ********************************************************************************
        public Region GetByDescription(string description)
        {
            Region region = repository.GetByDescription(description);

            if (region == null)
            {
                throw new RegionNotFoundException(RegionNotFoundException.RegionDescriptionNotFoundMsg, description);
            }

            return region;
        }
    }
}
