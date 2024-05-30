using CongestionTaxAPI.DTO;

namespace CongestionTaxAPI.Interfaces
{
    public interface ITaxRuleService
    {
        public void InsertData(string city, List<TaxRules> taxRules, int maxDailyCharge);
    }
}
