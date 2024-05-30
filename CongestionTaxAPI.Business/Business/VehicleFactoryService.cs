
using CongestionTaxAPI.Utilities;

namespace CongestionTaxAPI.Business
{

    public static class VehicleFactoryService
    {
        public static IVehicle CreateVehicle(string vehicleType)
        {
            return vehicleType.ToLower() switch
            {
                "car" => new Car(),
                "bus" => new Bus(),
                "emergency" => new Emergency(),
                "diplomat" => new Diplomat(),
                "motorcycle" => new Motorcycle(),
                "military" => new Military(),
                "foreign" => new Foreign(),
                _ => throw new ArgumentException("Invalid vehicle type")
            };
        }
    }
}
