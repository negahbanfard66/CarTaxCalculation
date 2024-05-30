using CongestionTaxAPI.Utilities.Enums;

namespace CongestionTaxAPI.Utilities
{
    public class Car : IVehicle
    {
        public VehicleTypeEnum VehicleType => VehicleTypeEnum.Car;
    }
}
