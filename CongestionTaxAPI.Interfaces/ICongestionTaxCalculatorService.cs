namespace CongestionTaxAPI.Intrefaces
{
    public interface ICongestionTaxCalculatorService
    {
        public int CalculateTax(string vehicleType, List<string> dates, string city);    
    }
}
