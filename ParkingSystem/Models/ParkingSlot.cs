using ParkingSystem.Models;

public class ParkingSlot
{
    public int SlotNumber { get; }
    public bool IsOccupied { get; private set; }
    public Vehicle? Vehicle { get; private set; }

    public ParkingSlot(int number)
    {
        SlotNumber = number;
        Vehicle = null;
    }

    public void Park(Vehicle vehicle)
    {
        Vehicle = vehicle;
        IsOccupied = true;
    }

    public void Leave()
    {
        Vehicle = null;
        IsOccupied = false;
    }
}
