using CongestionTaxAPI.Utilities.Enums;

namespace CongestionTaxAPI.Utilities
{
    public class Bus : IVehicle
    {
        public VehicleTypeEnum VehicleType => VehicleTypeEnum.Bus;
    }
}
