using CongestionTaxAPI.DataAccess;
using CongestionTaxAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxAPI.Business.Business
{
    public class TaxRuleService
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
