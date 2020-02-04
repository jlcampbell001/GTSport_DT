using GTSport_DT.General;
using System;

namespace GTSport_DT.Cars
{
    public class Car : Entity
    {
        public double Acceleration { get; set; }

        public string Aspiration { get; set; }

        public double Braking { get; set; }

        public CarCategory.Category Category { get; set; }

        public double Cornering { get; set; }

        public string DisplacementCC { get; set; }

        public string DriveTrain { get; set; }

        public double Height { get; set; }

        public double Length { get; set; }

        public string ManufacturerKey { get; set; }

        public int MaxPower { get; set; }

        public double MaxSpeed { get; set; }

        public string Name { get; set; }

        public string PowerRPM { get; set; }

        public double Price { get; set; }

        public double Stability { get; set; }

        public double TorqueFTLB { get; set; }

        public string TorqueRPM { get; set; }

        public double Weight { get; set; }

        public double Width { get; set; }

        public int Year { get; set; }

        public Car()
        {
        }

        public Car(string primaryKey, string name, string manufacturerKey, int year, CarCategory.Category category, double price, 
            string displacementCC, int maxPower, string powerRPM, double torqueFTLB, string torqueRPM, string driveTrain, 
            string aspiration, double length, double width, double height, double weight, double maxSpeed, double acceleration, 
            double braking, double cornering, double stability)
        {
            PrimaryKey = primaryKey ?? throw new ArgumentNullException(nameof(primaryKey));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            ManufacturerKey = manufacturerKey ?? throw new ArgumentNullException(nameof(manufacturerKey));
            this.Year = year;
            Category = category ?? throw new ArgumentNullException(nameof(category));
            this.Price = price;
            DisplacementCC = displacementCC ?? throw new ArgumentNullException(nameof(displacementCC));
            MaxPower = maxPower;
            PowerRPM = powerRPM ?? throw new ArgumentNullException(nameof(powerRPM));
            TorqueFTLB = torqueFTLB;
            TorqueRPM = torqueRPM ?? throw new ArgumentNullException(nameof(torqueRPM));
            DriveTrain = driveTrain ?? throw new ArgumentNullException(nameof(driveTrain));
            Aspiration = aspiration ?? throw new ArgumentNullException(nameof(aspiration));
            this.Length = length;
            this.Width = width;
            this.Height = height;
            this.Weight = weight;
            MaxSpeed = maxSpeed;
            Acceleration = acceleration;
            Braking = braking;
            Cornering = cornering;
            Stability = stability;
        }
    }
}