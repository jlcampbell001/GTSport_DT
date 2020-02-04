using System;
using System.Collections.Generic;
using System.Text;
using static GTSport_DT_Testing.Manufacturers.ManufacturersForTesting;
using static GTSport_DT.Cars.CarCategory;
using GTSport_DT.Cars;

namespace GTSport_DT_Testing.Cars
{
    public class CarsForTesting
    {
        private const string car1Key = "CAR900000001";
        private const string car1Name = "Roadster S (ND) '15";
        private static readonly string car1ManufacturerKey = Manufacturer1.PrimaryKey;
        private const int car1Year = 2015;
        private static readonly Category car1Category = N100;
        private const double car1Price = 24900.00;
        private const string car1DisplacementCC = "1496";
        private const int car1MaxPower = 128;
        private const string car1PowerRPM = "7000";
        private const double car1TorqueFTLB = 110.6;
        private const string car1TorqueRPM = "5000";
        private const string car1DriveTrain = DriveTrain.FR;
        private const string car1Aspiration = Aspiration.Empty;
        private const double car1Length = 154.1;
        private const double car1Width = 68.3;
        private const double car1Height = 48.6;
        private const double car1Weight = 2183;
        private const double car1MaxSpeed = 4.1;
        private const double car1Acceleration = 2.8;
        private const double car1Braking = 1.3;
        private const double car1Cornering = 1.1;
        private const double car1Stability = 4.9;

        private const string car2Key = "CAR900000002";
        private const string car2Name = "Golf VII GTI '14";
        private static readonly string car2ManufacturerKey = Manufacturer2.PrimaryKey;
        private const int car2Year = 2014;
        private static readonly Category car2Category = N200;
        private const double car2Price = 38330.00;
        private const string car2DisplacementCC = "1984";
        private const int car2MaxPower = 219;
        private const string car2PowerRPM = "4700";
        private const double car2TorqueFTLB = 258.2;
        private const string car2TorqueRPM = "1500";
        private const string car2DriveTrain = DriveTrain.FF;
        private const string car2Aspiration = Aspiration.Empty;
        private const double car2Length = 168.30;
        private const double car2Width = 70.90;
        private const double car2Height = 57.10;
        private const double car2Weight = 3064;
        private const double car2MaxSpeed = 5.3;
        private const double car2Acceleration = 2.7;
        private const double car2Braking = 1.5;
        private const double car2Cornering = 1.3;
        private const double car2Stability = 5.3;

        private const string car3Key = "CAR900000003";
        private const string car3Name = "Focus ST '15";
        private static readonly string car3ManufacturerKey = Manufacturer3.PrimaryKey;
        private const int car3Year = 1998;
        private static readonly Category car3Category = N300;
        private const double car3Price = 29250.00;
        private const string car3DisplacementCC = "1999";
        private const int car3MaxPower = 250;
        private const string car3PowerRPM = "5500";
        private const double car3TorqueFTLB = 669.70;
        private const string car3TorqueRPM = "2500";
        private const string car3DriveTrain = DriveTrain.FF;
        private const string car3Aspiration = Aspiration.Empty;
        private const double car3Length = 171.70;
        private const double car3Width = 71.70;
        private const double car3Height = 58.40;
        private const double car3Weight = 3223;
        private const double car3MaxSpeed = 5.7;
        private const double car3Acceleration = 2.5;
        private const double car3Braking = 1.7;
        private const double car3Cornering = 1.4;
        private const double car3Stability = 5.2;

        private const string car4Key = "CAR900000004";
        private const string car4Name = "A 45 AMG 4Matic '13";
        private static readonly string car4ManufacturerKey = Manufacturer4.PrimaryKey;
        private const int car4Year = 2013;
        private static readonly Category car4Category = N400;
        private const double car4Price = 64000.00;
        private const string car4DisplacementCC = "1991";
        private const int car4MaxPower = 355;
        private const string car4PowerRPM = "6000";
        private const double car4TorqueFTLB = 331.9;
        private const string car4TorqueRPM = "2500";
        private const string car4DriveTrain = DriveTrain.FourWD;
        private const string car4Aspiration = Aspiration.TB;
        private const double car4Length = 171.60;
        private const double car4Width = 70.10;
        private const double car4Height = 55.80;
        private const double car4Weight = 3428;
        private const double car4MaxSpeed = 7.0;
        private const double car4Acceleration = 5.5;
        private const double car4Braking = 1.6;
        private const double car4Cornering = 1.4;
        private const double car4Stability = 5.1;

        private const string car5Key = "CAR900000005";
        private const string car5Name = "911 GT3 RS (991) '16";
        private static readonly string car5ManufacturerKey = Manufacturer5.PrimaryKey;
        private const int car5Year = 2016;
        private static readonly Category car5Category = N500;
        private const double car5Price = 253000.00;
        private const string car5DisplacementCC = "3996";
        private const int car5MaxPower = 491;
        private const string car5PowerRPM = "8100";
        private const double car5TorqueFTLB = 340.1;
        private const string car5TorqueRPM = "6000";
        private const string car5DriveTrain = DriveTrain.RR;
        private const string car5Aspiration = Aspiration.NA;
        private const double car5Length = 178.90;
        private const double car5Width = 74.80;
        private const double car5Height = 50.80;
        private const double car5Weight = 3131;
        private const double car5MaxSpeed = 8.6;
        private const double car5Acceleration = 5.2;
        private const double car5Braking = 2.4;
        private const double car5Cornering = 2.3;
        private const double car5Stability = 5.3;

        private const string car6Key = "CAR900000006";
        private const string car6Name = "F-Type R Coupe '14";
        private static readonly string car6ManufacturerKey = Manufacturer6.PrimaryKey;
        private const int car6Year = 2014;
        private static readonly Category car6Category = N600;
        private const double car6Price = 128600.00;
        private const string car6DisplacementCC = "4999";
        private const int car6MaxPower = 542;
        private const string car6PowerRPM = "6500";
        private const double car6TorqueFTLB = 501.20;
        private const string car6TorqueRPM = "3500";
        private const string car6DriveTrain = DriveTrain.FR;
        private const string car6Aspiration = Aspiration.Empty;
        private const double car6Length = 176.00;
        private const double car6Width = 75.80;
        private const double car6Height = 51.80;
        private const double car6Weight = 3638;
        private const double car6MaxSpeed = 8.9;
        private const double car6Acceleration = 4.0;
        private const double car6Braking = 2.0;
        private const double car6Cornering = 1.8;
        private const double car6Stability = 4.9;

        private const string car7Key = "CAR900000007";
        private const string car7Name = "650S Coupe '14";
        private static readonly string car7ManufacturerKey = Manufacturer7.PrimaryKey;
        private const int car7Year = 2014;
        private static readonly Category car7Category = N700;
        private const double car7Price = 340000.00;
        private const string car7DisplacementCC = "3799";
        private const int car7MaxPower = 641;
        private const string car7PowerRPM = "7500";
        private const double car7TorqueFTLB = 500.50;
        private const string car7TorqueRPM = "6000";
        private const string car7DriveTrain = DriveTrain.MR;
        private const string car7Aspiration = Aspiration.Empty;
        private const double car7Length = 177.60;
        private const double car7Width = 75.10;
        private const double car7Height = 47.20;
        private const double car7Weight = 2932;
        private const double car7MaxSpeed = 9.6;
        private const double car7Acceleration = 4.7;
        private const double car7Braking = 2.0;
        private const double car7Cornering = 2.0;
        private const double car7Stability = 5.2;

        private const string car8Key = "CAR900000008";
        private const string car8Name = "One-77 '11";
        private static readonly string car8ManufacturerKey = Manufacturer8.PrimaryKey;
        private const int car8Year = 2011;
        private static readonly Category car8Category = N800;
        private const double car8Price = 1320000.00;
        private const string car8DisplacementCC = "7312";
        private const int car8MaxPower = 748;
        private const string car8PowerRPM = "8000";
        private const double car8TorqueFTLB = 553.50;
        private const string car8TorqueRPM = "5500";
        private const string car8DriveTrain = DriveTrain.FR;
        private const string car8Aspiration = Aspiration.Empty;
        private const double car8Length = 181.10;
        private const double car8Width = 78.70;
        private const double car8Height = 48.10;
        private const double car8Weight = 3592;
        private const double car8MaxSpeed = 10.0;
        private const double car8Acceleration = 4.4;
        private const double car8Braking = 2.1;
        private const double car8Cornering = 2.0;
        private const double car8Stability = 4.7;

        private const string car9Key = "CAR900000009";
        private const string car9Name = "Veyron 16.4 '13";
        private static readonly string car9ManufacturerKey = Manufacturer9.PrimaryKey;
        private const int car9Year = 2013;
        private static readonly Category car9Category = N1000;
        private const double car9Price = 2000000.00;
        private const string car9DisplacementCC = "7993";
        private const int car9MaxPower = 986;
        private const string car9PowerRPM = "6000";
        private const double car9TorqueFTLB = 922.50;
        private const string car9TorqueRPM = "2500";
        private const string car9DriveTrain = DriveTrain.FourWD;
        private const string car9Aspiration = Aspiration.Empty;
        private const double car9Length = 175.70;
        private const double car9Width = 78.70;
        private const double car9Height = 47.50;
        private const double car9Weight = 4162;
        private const double car9MaxSpeed = 10.0;
        private const double car9Acceleration = 6.6;
        private const double car9Braking = 2.2;
        private const double car9Cornering = 2.1;
        private const double car9Stability = 5.9;

        private const string car10Key = "CAR900000010";
        private const string car10Name = "Mazda LM55 VGT (Gr.1)";
        private static readonly string car10ManufacturerKey = Manufacturer1.PrimaryKey;
        private const int car10Year = 0;
        private static readonly Category car10Category = GR1;
        private const double car10Price = 1000000.00;
        private const string car10DisplacementCC = "";
        private const int car10MaxPower = 641;
        private const string car10PowerRPM = "8500";
        private const double car10TorqueFTLB = 416.60;
        private const string car10TorqueRPM = "6500";
        private const string car10DriveTrain = DriveTrain.FourWD;
        private const string car10Aspiration = Aspiration.NA;
        private const double car10Length = 187.00;
        private const double car10Width = 80.10;
        private const double car10Height = 33.90;
        private const double car10Weight = 1940;
        private const double car10MaxSpeed = 10.0;
        private const double car10Acceleration = 8.9;
        private const double car10Braking = 5.8;
        private const double car10Cornering = 5.8;
        private const double car10Stability = 7.4;

        private const string car11Key = "CAR900000011";
        private const string car11Name = "650S GT3 '15";
        private static readonly string car11ManufacturerKey = Manufacturer7.PrimaryKey;
        private const int car11Year = 2015;
        private static readonly Category car11Category = GR3;
        private const double car11Price = 450000.00;
        private const string car11DisplacementCC = "3799";
        private const int car11MaxPower = 506;
        private const string car11PowerRPM = "7500";
        private const double car11TorqueFTLB = 379.90;
        private const string car11TorqueRPM = "6000";
        private const string car11DriveTrain = DriveTrain.MR;
        private const string car11Aspiration = Aspiration.Empty;
        private const double car11Length = 182.20;
        private const double car11Width = 80.30;
        private const double car11Height = 45.80;
        private const double car11Weight = 2734;
        private const double car11MaxSpeed = 6.1;
        private const double car11Acceleration = 4.4;
        private const double car11Braking = 4.0;
        private const double car11Cornering = 4.0;
        private const double car11Stability = 5.7;

        private const string car12Key = "CAR900000012";
        private const string car12Name = "650S Gr.4";
        private static readonly string car12ManufacturerKey = Manufacturer7.PrimaryKey;
        private const int car12Year = 0;
        private static readonly Category car12Category = GR4;
        private const double car12Price = 350000.00;
        private const string car12DisplacementCC = "3799";
        private const int car12MaxPower = 399;
        private const string car12PowerRPM = "7500";
        private const double car12TorqueFTLB = 312.30;
        private const string car12TorqueRPM = "6000";
        private const string car12DriveTrain = DriveTrain.MR;
        private const string car12Aspiration = Aspiration.Empty;
        private const double car12Length = 180.10;
        private const double car12Width = 75.10;
        private const double car12Height = 45.70;
        private const double car12Weight = 2866;
        private const double car12MaxSpeed = 6.2;
        private const double car12Acceleration = 5.3;
        private const double car12Braking = 3.1;
        private const double car12Cornering = 3.1;
        private const double car12Stability = 5.9;

        private const string car13Key = "CAR900000013";
        private const string car13Name = "Focus Gr.B Rally Car";
        private static readonly string car13ManufacturerKey = Manufacturer3.PrimaryKey;
        private const int car13Year = 0;
        private static readonly Category car13Category = GRB;
        private const double car13Price = 450000.00;
        private const string car13DisplacementCC = "1999";
        private const int car13MaxPower = 538;
        private const string car13PowerRPM = "7500";
        private const double car13TorqueFTLB = 427.40;
        private const string car13TorqueRPM = "4500";
        private const string car13DriveTrain = DriveTrain.FourWD;
        private const string car13Aspiration = Aspiration.TB;
        private const double car13Length = 173.90;
        private const double car13Width = 78.70;
        private const double car13Height = 61.80;
        private const double car13Weight = 2778;
        private const double car13MaxSpeed = 5.7;
        private const double car13Acceleration = 7.2;
        private const double car13Braking = 3.0;
        private const double car13Cornering = 2.9;
        private const double car13Stability = 5.3;

        private const string car14Key = "CAR900000014";
        private const string car14Name = "Aston Martin DP-100 VGT";
        private static readonly string car14ManufacturerKey = Manufacturer8.PrimaryKey;
        private const int car14Year = 0;
        private static readonly Category car14Category = GRX;
        private const double car14Price = 1000000.00;
        private const string car14DisplacementCC = "6000";
        private const int car14MaxPower = 799;
        private const string car14PowerRPM = "6500";
        private const double car14TorqueFTLB = 774.90;
        private const string car14TorqueRPM = "4500";
        private const string car14DriveTrain = DriveTrain.FourWD;
        private const string car14Aspiration = Aspiration.Empty;
        private const double car14Length = 180.20;
        private const double car14Width = 79.00;
        private const double car14Height = 43.80;
        private const double car14Weight = 3031;
        private const double car14MaxSpeed = 9.5;
        private const double car14Acceleration = 8.0;
        private const double car14Braking = 3.4;
        private const double car14Cornering = 3.5;
        private const double car14Stability = 5.8;

        private const string car15Key = "CAR900000015";
        private const string car15Name = "Mustang GT Premium Fastback '15";
        private static readonly string car15ManufacturerKey = Manufacturer3.PrimaryKey;
        private const int car15Year = 2015;
        private static readonly Category car15Category = N400;
        private const double car15Price = 44310.00;
        private const string car15DisplacementCC = "6000";
        private const int car15MaxPower = 434;
        private const string car15PowerRPM = "6500";
        private const double car15TorqueFTLB = 774.90;
        private const string car15TorqueRPM = "4500";
        private const string car15DriveTrain = DriveTrain.FF;
        private const string car15Aspiration = Aspiration.Empty;
        private const double car15Length = 180.20;
        private const double car15Width = 79.00;
        private const double car15Height = 43.80;
        private const double car15Weight = 3031;
        private const double car15MaxSpeed = 6.7;
        private const double car15Acceleration = 3.1;
        private const double car15Braking = 1.9;
        private const double car15Cornering = 1.7;
        private const double car15Stability = 4.6;

        public static readonly Car Car1 = new Car(car1Key, car1Name, car1ManufacturerKey, car1Year, car1Category, car1Price, car1DisplacementCC,
            car1MaxPower, car1PowerRPM, car1TorqueFTLB, car1TorqueRPM, car1DriveTrain, car1Aspiration, car1Length, car1Width,
            car1Height, car1Weight, car1MaxSpeed, car1Acceleration, car1Braking, car1Cornering, car1Stability);

        public static readonly Car Car2 = new Car(car2Key, car2Name, car2ManufacturerKey, car2Year, car2Category, car2Price, car2DisplacementCC,
            car2MaxPower, car2PowerRPM, car2TorqueFTLB, car2TorqueRPM, car2DriveTrain, car2Aspiration, car2Length, car2Width,
            car2Height, car2Weight, car2MaxSpeed, car2Acceleration, car2Braking, car2Cornering, car2Stability);

        public static readonly Car Car3 = new Car(car3Key, car3Name, car3ManufacturerKey, car3Year, car3Category, car3Price, car3DisplacementCC,
            car3MaxPower, car3PowerRPM, car3TorqueFTLB, car3TorqueRPM, car3DriveTrain, car3Aspiration, car3Length, car3Width,
            car3Height, car3Weight, car3MaxSpeed, car3Acceleration, car3Braking, car3Cornering, car3Stability);

        public static readonly Car Car4 = new Car(car4Key, car4Name, car4ManufacturerKey, car4Year, car4Category, car4Price, car4DisplacementCC,
            car4MaxPower, car4PowerRPM, car4TorqueFTLB, car4TorqueRPM, car4DriveTrain, car4Aspiration, car4Length, car4Width,
            car4Height, car4Weight, car4MaxSpeed, car4Acceleration, car4Braking, car4Cornering, car4Stability);

        public static readonly Car Car5 = new Car(car5Key, car5Name, car5ManufacturerKey, car5Year, car5Category, car5Price, car5DisplacementCC,
            car5MaxPower, car5PowerRPM, car5TorqueFTLB, car5TorqueRPM, car5DriveTrain, car5Aspiration, car5Length, car5Width,
            car5Height, car5Weight, car5MaxSpeed, car5Acceleration, car5Braking, car5Cornering, car5Stability);

        public static readonly Car Car6 = new Car(car6Key, car6Name, car6ManufacturerKey, car6Year, car6Category, car6Price, car6DisplacementCC,
            car6MaxPower, car6PowerRPM, car6TorqueFTLB, car6TorqueRPM, car6DriveTrain, car6Aspiration, car6Length, car6Width,
            car6Height, car6Weight, car6MaxSpeed, car6Acceleration, car6Braking, car6Cornering, car6Stability);

        public static readonly Car Car7 = new Car(car7Key, car7Name, car7ManufacturerKey, car7Year, car7Category, car7Price, car7DisplacementCC,
            car7MaxPower, car7PowerRPM, car7TorqueFTLB, car7TorqueRPM, car7DriveTrain, car7Aspiration, car7Length, car7Width,
            car7Height, car7Weight, car7MaxSpeed, car7Acceleration, car7Braking, car7Cornering, car7Stability);

        public static readonly Car Car8 = new Car(car8Key, car8Name, car8ManufacturerKey, car8Year, car8Category, car8Price, car8DisplacementCC,
            car8MaxPower, car8PowerRPM, car8TorqueFTLB, car8TorqueRPM, car8DriveTrain, car8Aspiration, car8Length, car8Width,
            car8Height, car8Weight, car8MaxSpeed, car8Acceleration, car8Braking, car8Cornering, car8Stability);

        public static readonly Car Car9 = new Car(car9Key, car9Name, car9ManufacturerKey, car9Year, car9Category, car9Price, car9DisplacementCC,
            car9MaxPower, car9PowerRPM, car9TorqueFTLB, car9TorqueRPM, car9DriveTrain, car9Aspiration, car9Length, car9Width,
            car9Height, car9Weight, car9MaxSpeed, car9Acceleration, car9Braking, car9Cornering, car9Stability);

        public static readonly Car Car10 = new Car(car10Key, car10Name, car10ManufacturerKey, car10Year, car10Category, car10Price, car10DisplacementCC,
            car10MaxPower, car10PowerRPM, car10TorqueFTLB, car10TorqueRPM, car10DriveTrain, car10Aspiration, car10Length, car10Width,
            car10Height, car10Weight, car10MaxSpeed, car10Acceleration, car10Braking, car10Cornering, car10Stability);

        public static readonly Car Car11 = new Car(car11Key, car11Name, car11ManufacturerKey, car11Year, car11Category, car11Price, car11DisplacementCC,
            car11MaxPower, car11PowerRPM, car11TorqueFTLB, car11TorqueRPM, car11DriveTrain, car11Aspiration, car11Length, car11Width,
            car11Height, car11Weight, car11MaxSpeed, car11Acceleration, car11Braking, car11Cornering, car11Stability);

        public static readonly Car Car12 = new Car(car12Key, car12Name, car12ManufacturerKey, car12Year, car12Category, car12Price, car12DisplacementCC,
            car12MaxPower, car12PowerRPM, car12TorqueFTLB, car12TorqueRPM, car12DriveTrain, car12Aspiration, car12Length, car12Width,
            car12Height, car12Weight, car12MaxSpeed, car12Acceleration, car12Braking, car12Cornering, car12Stability);

        public static readonly Car Car13 = new Car(car13Key, car13Name, car13ManufacturerKey, car13Year, car13Category, car13Price, car13DisplacementCC,
            car13MaxPower, car13PowerRPM, car13TorqueFTLB, car13TorqueRPM, car13DriveTrain, car13Aspiration, car13Length, car13Width,
            car13Height, car13Weight, car13MaxSpeed, car13Acceleration, car13Braking, car13Cornering, car13Stability);

        public static readonly Car Car14 = new Car(car14Key, car14Name, car14ManufacturerKey, car14Year, car14Category, car14Price, car14DisplacementCC,
            car14MaxPower, car14PowerRPM, car14TorqueFTLB, car14TorqueRPM, car14DriveTrain, car14Aspiration, car14Length, car14Width,
            car14Height, car14Weight, car14MaxSpeed, car14Acceleration, car14Braking, car14Cornering, car14Stability);

        public static readonly Car Car15 = new Car(car15Key, car15Name, car15ManufacturerKey, car15Year, car15Category, car15Price, car15DisplacementCC,
            car15MaxPower, car15PowerRPM, car15TorqueFTLB, car15TorqueRPM, car15DriveTrain, car15Aspiration, car15Length, car15Width,
            car15Height, car15Weight, car15MaxSpeed, car15Acceleration, car15Braking, car15Cornering, car15Stability);

    }
}
