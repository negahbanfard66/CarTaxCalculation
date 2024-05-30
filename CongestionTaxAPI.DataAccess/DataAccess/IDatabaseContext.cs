// IDatabaseContext.cs
using CongestionTaxAPI.DTO;
using System;
using System.Collections.Generic;

namespace CongestionTaxAPI.DataAccess
{
    public interface IDatabaseContext : IDisposable
    {
        void Initialize();
        void SeedData(string city, List<TaxRules> taxRules, int maxDailyCharge);
        List<Tuple<string, string, int>> GetTaxRules(string city);
        int GetMaxDailyCharge(string city);
        bool IsVehicleExempt(string vehicleType);
        bool IsHoliday(string date, string city);
    }
}
