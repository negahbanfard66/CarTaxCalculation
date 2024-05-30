using CongestionTaxAPI.DataAccess;
using CongestionTaxAPI.DTO;
using CongestionTaxAPI.Interfaces;

namespace CongestionTaxAPI.Business
{
    public class TaxRuleService: ITaxRuleService
    {
        private readonly IDatabaseContext _databaseContext;
        public TaxRuleService(IDatabaseContext databaseContext) 
        {
            _databaseContext = databaseContext;
        }
        public void InsertData(string city, List<TaxRules> taxRules, int maxDailyCharge)
        {
            _databaseContext.SeedData(city,taxRules,maxDailyCharge);
        }
    }
}
