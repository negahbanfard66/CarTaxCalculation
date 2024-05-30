using CongestionTaxAPI.Utilities.Enums;

namespace CongestionTaxAPI.Utilities
{
    public class Military : IVehicle
    {
        public VehicleTypeEnum VehicleType => VehicleTypeEnum.Military;
    }
}
