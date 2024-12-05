using System;
using System.Collections.Generic;
using ParkingSystem.Models;

namespace ParkingSystem.Services
{
    public class ParkingManager
    {
        private List<ParkingSlot> slots = new List<ParkingSlot>();
        private int maxSlots;

        public void ProcessCommand(string command)
        {
            var inputs = command.Split(' ');
            switch (inputs[0].ToLower())
            {
                case "create_parking_lot":
                    maxSlots = int.Parse(inputs[1]);
                    for (int i = 0; i < maxSlots; i++)
                        slots.Add(new ParkingSlot(i + 1));
                    Console.WriteLine($"Created a parking lot with {maxSlots} slots");
                    break;

                case "park":
                    var vehicle = new Vehicle(inputs[1], inputs[2], inputs[3]);
                    AllocateSlot(vehicle);
                    break;

                case "leave":
                    FreeSlot(int.Parse(inputs[1]));
                    break;

                case "report_odd_even":
                    GenerateReportByCriteria("odd_even");
                    break;

                case "report_type":
                    GenerateReportByCriteria("type");
                    break;

                case "report_color":
                    GenerateReportByCriteria("color");
                    break;

                default:
                    Console.WriteLine("Unknown command");
                    break;
            }
        }

        private void AllocateSlot(Vehicle vehicle)
        {
            if (vehicle.Type.ToLower() != "mobil" && vehicle.Type.ToLower() != "motor")
            {
                Console.WriteLine("Only small cars and motorcycles are allowed.");
                return;
            }

            foreach (var slot in slots)
            {
                if (!slot.IsOccupied)
                {
                    slot.Park(vehicle);
                    Console.WriteLine($"Allocated slot number: {slot.SlotNumber}");
                    return;
                }
            }
            Console.WriteLine("Sorry, parking lot is full");
        }

        private void FreeSlot(int slotNumber)
        {
            var slot = slots.Find(s => s.SlotNumber == slotNumber);
            if (slot != null && slot.IsOccupied)
            {
                slot.Leave();
                Console.WriteLine($"Slot number {slotNumber} is free");
            }
            else
            {
                Console.WriteLine("Slot is already free or does not exist");
            }
        }

        private void GenerateReportByCriteria(string criteria)
        {
            if (criteria == "odd_even")
            {
                int oddCount = 0, evenCount = 0;
                foreach (var slot in slots)
                {
                    if (slot.IsOccupied)
                    {
                        int lastDigit = int.Parse(slot.Vehicle.PlateNumber[^1].ToString());
                        if (lastDigit % 2 == 0)
                            evenCount++;
                        else
                            oddCount++;
                    }
                }
                Console.WriteLine($"Odd vehicles: {oddCount}, Even vehicles: {evenCount}");
            }
            else if (criteria == "type")
            {
                int carCount = 0, bikeCount = 0;
                foreach (var slot in slots)
                {
                    if (slot.IsOccupied)
                    {
                        if (slot.Vehicle.Type.ToLower() == "mobil")
                            carCount++;
                        else if (slot.Vehicle.Type.ToLower() == "motor")
                            bikeCount++;
                    }
                }
                Console.WriteLine($"Cars: {carCount}, Motorcycles: {bikeCount}");
            }
            else if (criteria == "color")
            {
                var colorCount = new Dictionary<string, int>();
                foreach (var slot in slots)
                {
                    if (slot.IsOccupied)
                    {
                        string color = slot.Vehicle.Color.ToLower();
                        if (colorCount.ContainsKey(color))
                            colorCount[color]++;
                        else
                            colorCount[color] = 1;
                    }
                }
                foreach (var entry in colorCount)
                    Console.WriteLine($"{entry.Key}: {entry.Value}");
            }
        }
    }
}
