using System;
using ParkingSystem.Services;

namespace ParkingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            ParkingManager manager = new ParkingManager();
            string command;

            Console.WriteLine("Parking System Initialized. Enter 'exit' to quit.");
            while (true)
            {
                Console.Write("$ ");
                command = Console.ReadLine();

                if (command.ToLower() == "exit")
                    break;

                manager.ProcessCommand(command);
            }
        }
    }
}
