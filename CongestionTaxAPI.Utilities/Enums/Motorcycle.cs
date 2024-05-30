using CongestionTaxAPI.Utilities.Enums;

namespace CongestionTaxAPI.Utilities
{
    public class Motorcycle : IVehicle
    {
        public VehicleTypeEnum VehicleType => VehicleTypeEnum.Motorcycle;
    }
}
