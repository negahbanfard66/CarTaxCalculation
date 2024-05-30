using CongestionTaxAPI.Utilities.Enums;

namespace CongestionTaxAPI.Utilities
{
    public class Foreign : IVehicle
    {
        public VehicleTypeEnum VehicleType => VehicleTypeEnum.Foreign;
    }
}
