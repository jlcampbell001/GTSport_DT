using GTSport_DT.General;
using System;

namespace GTSport_DT.Cars
{
    /// <summary>
    /// <para>The car entity for a record in the car table.</para>
    /// </summary>
    /// <seealso cref="GTSport_DT.General.Entity"/>
    public class Car : Entity
    {
        /// <summary>Gets or sets the acceleration.</summary>
        /// <value>The acceleration.</value>
        public double Acceleration { get; set; }

        /// <summary>Gets or sets the aspiration.</summary>
        /// <value>The aspiration.</value>
        public string Aspiration { get; set; }

        /// <summary>Gets or sets the braking.</summary>
        /// <value>The braking.</value>
        public double Braking { get; set; }

        /// <summary>Gets or sets the category.</summary>
        /// <value>The category.</value>
        public CarCategory.Category Category { get; set; }

        /// <summary>Gets or sets the cornering.</summary>
        /// <value>The cornering.</value>
        public double Cornering { get; set; }

        /// <summary>Gets or sets the displacement cc.</summary>
        /// <value>The displacement cc.</value>
        public string DisplacementCC { get; set; }

        /// <summary>Gets or sets the drive train.</summary>
        /// <value>The drive train.</value>
        public string DriveTrain { get; set; }

        /// <summary>Gets or sets the height.</summary>
        /// <value>The height.</value>
        public double Height { get; set; }

        /// <summary>Gets or sets the length.</summary>
        /// <value>The length.</value>
        public double Length { get; set; }

        /// <summary>Gets or sets the manufacturer key.</summary>
        /// <value>The manufacturer key.</value>
        public string ManufacturerKey { get; set; }

        /// <summary>Gets or sets the maximum power.</summary>
        /// <value>The maximum power.</value>
        public int MaxPower { get; set; }

        /// <summary>Gets or sets the maximum speed.</summary>
        /// <value>The maximum speed.</value>
        public double MaxSpeed { get; set; }

        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>Gets or sets the power RPM.</summary>
        /// <value>The power RPM.</value>
        public string PowerRPM { get; set; }

        /// <summary>Gets or sets the price.</summary>
        /// <value>The price.</value>
        public double Price { get; set; }

        /// <summary>Gets or sets the stability.</summary>
        /// <value>The stability.</value>
        public double Stability { get; set; }

        /// <summary>Gets or sets the torque FTLB.</summary>
        /// <value>The torque FTLB.</value>
        public double TorqueFTLB { get; set; }

        /// <summary>Gets or sets the torque RPM.</summary>
        /// <value>The torque RPM.</value>
        public string TorqueRPM { get; set; }

        /// <summary>Gets or sets the weight.</summary>
        /// <value>The weight.</value>
        public double Weight { get; set; }

        /// <summary>Gets or sets the width.</summary>
        /// <value>The width.</value>
        public double Width { get; set; }

        /// <summary>Gets or sets the year.</summary>
        /// <value>The year.</value>
        public int Year { get; set; }

        /// <summary>Initializes a new instance of the <see cref="Car"/> class.</summary>
        public Car()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="Car"/> class.</summary>
        /// <param name="primaryKey">The primary key.</param>
        /// <param name="name">The name.</param>
        /// <param name="manufacturerKey">The manufacturer key.</param>
        /// <param name="year">The year.</param>
        /// <param name="category">The category.</param>
        /// <param name="price">The price.</param>
        /// <param name="displacementCC">The displacement cc.</param>
        /// <param name="maxPower">The maximum power.</param>
        /// <param name="powerRPM">The power RPM.</param>
        /// <param name="torqueFTLB">The torque FTLB.</param>
        /// <param name="torqueRPM">The torque RPM.</param>
        /// <param name="driveTrain">The drive train.</param>
        /// <param name="aspiration">The aspiration.</param>
        /// <param name="length">The length.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="weight">The weight.</param>
        /// <param name="maxSpeed">The maximum speed.</param>
        /// <param name="acceleration">The acceleration.</param>
        /// <param name="braking">The braking.</param>
        /// <param name="cornering">The cornering.</param>
        /// <param name="stability">The stability.</param>
        /// <exception cref="ArgumentNullException">
        /// primaryKey or name or manufacturerKey or category or displacementCC or powerRPM or
        /// torqueRPM or driveTrain or aspiration
        /// </exception>
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

        public override string ToString()
        {
            string line = "{Primary Key = '" + PrimaryKey + "', Name = '" + Name + "', Manufacturer Key = '" + ManufacturerKey 
                + "', Year = " + Year + ", Category = " + Category.ToString() + ", Price = " + Price + ", Displacement CC = '" + DisplacementCC
                + "', MaxPower = " + MaxPower + ", Power RPM = '" + PowerRPM + "', Torque FTLB = " + TorqueRPM + ", Torque RPM = '" + TorqueRPM
                + "', Drive Train = '" + DriveTrain + "', Aspiration = '" + Aspiration + "', Length = " + Length + ", Width = " + Width
                + ", Height = " + Height + ", Weight = " + Weight + ", Max Speed = " + MaxSpeed + ", Acceleration = " + Acceleration
                + ", Braking = " + Braking + ", Cornering = " + Cornering + ", Stability = " + Stability + "}";
            return line;

        }
    }
}