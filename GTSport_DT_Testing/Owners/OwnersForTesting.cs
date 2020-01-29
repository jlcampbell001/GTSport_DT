using GTSport_DT.Owners;
using System;
using System.Collections.Generic;
using System.Text;

namespace GTSport_DT_Testing.Owners
{
    static class OwnersForTesting
    {
        private const string owner1Key = "OWN900000001";
        private const string owner1Name = "XXX_Test_Owner_1_XXX";
        private const Boolean owner1Default = false;

        private const string owner2Key = "OWN900000002";
        private const string owner2Name = "XXX_Test_Owner_2_XXX";
        private const Boolean owner2Default = true;

        private const string owner3Key = "OWN900000003";
        private const string owner3Name = "XXX_Test_Owner_3_XXX";
        private const Boolean owner3Default = false;

        public static Owner owner1 = new Owner(owner1Key, owner1Name, owner1Default);

        public static Owner owner2 = new Owner(owner2Key, owner2Name, owner2Default);

        public static Owner owner3 = new Owner(owner3Key, owner3Name, owner3Default);

    }
}
