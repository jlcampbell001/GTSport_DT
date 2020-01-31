using GTSport_DT.Regions;
using System;
using System.Collections.Generic;
using System.Text;

namespace GTSport_DT_Testing.Regions
{
    static class RegionsForTesting
    {
        private const string region1Key = "REG900000001";
        private const string region1Description = "ASIAN-PACIFIC";

        private const string region2Key = "REG900000002";
        private const string region2Description = "EUROPE";

        private const string region3Key = "REG900000003";
        private const string region3Description = "AMERICA";

        public static Region Region1 = new Region(region1Key, region1Description);
        public static Region Region2 = new Region(region2Key, region2Description);
        public static Region Region3 = new Region(region3Key, region3Description);
    }
}
