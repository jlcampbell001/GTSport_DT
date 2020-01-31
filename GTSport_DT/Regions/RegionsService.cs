using GTSport_DT.General;
using Npgsql;

namespace GTSport_DT.Regions
{
    /// <summary>The service for the region table.</summary>
    public class RegionsService : BackEndService<Region, RegionsRepository, RegionValidation, RegionNotFoundException>
    {
        /// <summary>Initializes a new instance of the <see cref="RegionsService"/> class.</summary>
        /// <param name="npgsqlConnection">The NPGSQL connection.</param>
        public RegionsService(NpgsqlConnection npgsqlConnection) : base(npgsqlConnection)
        {
            primaryKeyPrefix = "REG";
            keyNotFoundMessage = RegionNotFoundException.RegionKeyNotFoundMsg;
        }

        /// <summary>Get a region that matches the passed description.</summary>
        /// <param name="description">The description to look up.</param>
        /// <returns>The region found.</returns>
        /// <exception cref="RegionNotFoundException">
        /// If it can not found the region by the description.
        /// </exception>
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