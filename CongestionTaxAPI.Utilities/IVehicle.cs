using CongestionTaxAPI.Utilities.Enums;

namespace CongestionTaxAPI.Utilities
{
    public interface IVehicle
    {
        public VehicleTypeEnum VehicleType { get; }
    }
}
