using CongestionTaxAPI.Utilities.Enums;

namespace CongestionTaxAPI.Utilities
{
    public class Emergency : IVehicle
    {
        public VehicleTypeEnum VehicleType => VehicleTypeEnum.Emergency;
    }
}
