namespace TaxCalculator.Models
{
    public class TaxCalculationRequest
    {
        public string VehicleType { get; set; }
        public string? City { get; set; }
        public List<string> Dates { get; set; }
    }
}
