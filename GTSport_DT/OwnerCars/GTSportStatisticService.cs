using GTSport_DT.Cars;
using Npgsql;
using System;
using System.Collections.Generic;

namespace GTSport_DT.OwnerCars
{
    /// <summary>The service for statistics.</summary>
    public class GTSportStatisticService
    {
        /// <summary>The NPGSQL connection</summary>
        protected NpgsqlConnection npgsqlConnection;

        private CarsRepository carsRepository;
        private OwnerCarsRepository ownerCarsRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GTSportStatisticService"/> class.
        /// </summary>
        /// <param name="npgsqlConnection">The NPGSQL connection.</param>
        /// <exception cref="ArgumentNullException">npgsqlConnection</exception>
        public GTSportStatisticService(NpgsqlConnection npgsqlConnection)
        {
            this.npgsqlConnection = npgsqlConnection ?? throw new ArgumentNullException(nameof(npgsqlConnection));

            ownerCarsRepository = new OwnerCarsRepository(npgsqlConnection);
            carsRepository = new CarsRepository(npgsqlConnection);
        }

        /// <summary>
        /// <para>Gets the gt sport statistics.</para>
        /// <para>
        /// The total line is only the total cars in game, total cars owned, and the unique total
        /// cars owned.
        /// </para>
        /// </summary>
        /// <param name="ownerKey">The owner key.</param>
        /// <returns>A list of gt sport statistics for each category and the total line.</returns>
        public List<GTSportStatistic> GetGTSportStatistics(string ownerKey)
        {
            List<GTSportStatistic> statistics = new List<GTSportStatistic>();

            foreach (CarCategory.Category category in CarCategory.categories)
            {
                if (category != CarCategory.Empty)
                {
                    GTSportStatistic statistic = new GTSportStatistic();
                    statistic.Category = category;

                    statistics.Add(statistic);
                }
            }

            AddCarStatistics(ref statistics);
            AddOwnedStatistics(ref statistics, ownerKey);
            AddUniqueOwnedStatistics(ref statistics, ownerKey);
            AddTotalLine(ref statistics);

            return statistics;
        }

        private void AddCarStatistics(ref List<GTSportStatistic> statistics)
        {
            List<CarStatistic> carStatistics = carsRepository.GetCarStatistics();

            foreach (CarStatistic carStatistic in carStatistics)
            {
                foreach (GTSportStatistic statistic in statistics)
                {
                    if (statistic.Category == carStatistic.Category)
                    {
                        statistic.AvgMaxSpeed = Math.Round(carStatistic.AvgMaxSpeed, 1);
                        statistic.AvgAcceleration = Math.Round(carStatistic.AvgAcceleration, 1);
                        statistic.AvgBraking = Math.Round(carStatistic.AvgBraking, 1);
                        statistic.AvgCornering = Math.Round(carStatistic.AvgCornering, 1);
                        statistic.AvgMaxPower = Math.Round(carStatistic.AvgMaxPower, 0);
                        statistic.AvgPrice = Math.Round(carStatistic.AvgPrice, 2);
                        statistic.AvgStability = Math.Round(carStatistic.AvgStability, 1);
                        statistic.NumberOfCars = carStatistic.NumberOfCars;
                    }
                }
            }
        }

        private void AddOwnedStatistics(ref List<GTSportStatistic> statistics, string ownerKey)
        {
            List<OwnerCarsStatistic> ownerCarsStatistics = ownerCarsRepository.GetOwnerCarStatistics(ownerKey);

            foreach (OwnerCarsStatistic ownerCarsStatistic in ownerCarsStatistics)
            {
                foreach (GTSportStatistic statistic in statistics)
                {
                    if (statistic.Category == ownerCarsStatistic.Category)
                    {
                        statistic.CarsOwned = ownerCarsStatistic.carsOwned;
                    }
                }
            }
        }

        private void AddTotalLine(ref List<GTSportStatistic> statistics)
        {
            GTSportStatistic statistic = new GTSportStatistic();
            statistic.Category = CarCategory.Total;

            foreach (GTSportStatistic sportStatistic in statistics)
            {
                statistic.NumberOfCars = statistic.NumberOfCars + sportStatistic.NumberOfCars;
                statistic.CarsOwned = statistic.CarsOwned + sportStatistic.CarsOwned;
                statistic.UniqueCarsOwned = statistic.UniqueCarsOwned + sportStatistic.UniqueCarsOwned;

                if (sportStatistic.NumberOfCars > 0)
                {
                    sportStatistic.PercentOwned = (double)sportStatistic.UniqueCarsOwned / sportStatistic.NumberOfCars;
                }
            }

            if (statistic.NumberOfCars > 0)
            {
                statistic.PercentOwned = (double)statistic.UniqueCarsOwned / statistic.NumberOfCars;
            }
            statistics.Insert(0, statistic);
        }

        private void AddUniqueOwnedStatistics(ref List<GTSportStatistic> statistics, string ownerKey)
        {
            List<OwnerCarsStatistic> ownerCarsStatistics = ownerCarsRepository.GetUniqueOwnerCarStatistics(ownerKey);

            foreach (OwnerCarsStatistic ownerCarsStatistic in ownerCarsStatistics)
            {
                foreach (GTSportStatistic statistic in statistics)
                {
                    if (statistic.Category == ownerCarsStatistic.Category)
                    {
                        statistic.UniqueCarsOwned = ownerCarsStatistic.uniqueCarsOwned;
                    }
                }
            }
        }
    }
}