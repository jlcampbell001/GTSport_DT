using GTSport_DT.Owners;
using System;
using System.Collections.Generic;
using System.Text;

namespace GTSport_DT_Testing.Owners
{
    static class OwnersForTesting
    {
        private static readonly string owner1Key = "OWN900000001";
        private static readonly string owner1Name = "XXX_Test_Owner_1_XXX";
        private static readonly Boolean owner1Default = false;

        private static readonly string owner2Key = "OWN900000002";
        private static readonly string owner2Name = "XXX_Test_Owner_2_XXX";
        private static readonly Boolean owner2Default = true;

        private static readonly string owner3Key = "OWN900000003";
        private static readonly string owner3Name = "XXX_Test_Owner_3_XXX";
        private static readonly Boolean owner3Default = false;

        /**
         * Test data for an owner.
         */
        public static readonly Owner owner1 = new Owner(owner1Key, owner1Name, owner1Default);

        /**
         * Test data for an owner.
         */
        public static readonly Owner owner2 = new Owner(owner2Key, owner2Name, owner2Default);

        /**
         * Test data for an owner.
         */
        public static readonly Owner owner3 = new Owner(owner3Key, owner3Name, owner3Default);

    }
}
