namespace ParkingSystem.Models
{
    public class Vehicle
    {
        public string PlateNumber { get; }
        public string Color { get; }
        public string Type { get; }

        public Vehicle(string plateNumber, string color, string type)
        {
            PlateNumber = plateNumber;
            Color = color;
            Type = type;
        }
    }
}
