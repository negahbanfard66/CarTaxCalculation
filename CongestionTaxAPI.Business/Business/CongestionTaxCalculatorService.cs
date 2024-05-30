using CongestionTaxAPI.DataAccess;
using CongestionTaxAPI.Intrefaces;

namespace CongestionTaxAPI.Business
{
    public class CongestionTaxCalculatorService: ICongestionTaxCalculatorService
    {
        private readonly IDatabaseContext _dbContext;

        public CongestionTaxCalculatorService(IDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int CalculateTax(string vehicleType, List<string> dates, string city)
        {
            var vehicle = VehicleFactoryService.CreateVehicle(vehicleType);
            if (_dbContext.IsVehicleExempt(vehicle.VehicleType.ToString()))
                return 0;

            var taxRules = _dbContext.GetTaxRules(city);
            var maxDailyCharge = _dbContext.GetMaxDailyCharge(city);
            var dateAmounts = new Dictionary<string, List<Tuple<DateTime, int>>>();

            foreach (var date in dates)
            {
                var dt = DateTime.Parse(date);
                var dateStr = dt.Date.ToString("yyyy-MM-dd");

                if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday || _dbContext.IsHoliday(dateStr, city) || dt.Month == 7)
                    continue;

                foreach (var rule in taxRules)
                {
                    var startTime = DateTime.Parse(rule.Item1);
                    var endTime = DateTime.Parse(rule.Item2);
                    var amount = rule.Item3;

                    if (startTime.TimeOfDay <= dt.TimeOfDay && dt.TimeOfDay <= endTime.TimeOfDay)
                    {
                        if (!dateAmounts.ContainsKey(dateStr))
                            dateAmounts[dateStr] = new List<Tuple<DateTime, int>>();

                        dateAmounts[dateStr].Add(new Tuple<DateTime, int>(dt, amount));
                    }
                }
            }

            int totalTax = 0;

            foreach (var dateStr in dateAmounts.Keys)
            {
                var amounts = dateAmounts[dateStr];
                amounts.Sort();
                int dailyTotal = 0;
                int currentPeriodMax = 0;
                DateTime? periodStart = null;

                foreach (var amount in amounts)
                {
                    if (periodStart == null || amount.Item1 - periodStart > TimeSpan.FromMinutes(60))
                    {
                        dailyTotal += currentPeriodMax;
                        currentPeriodMax = amount.Item2;
                        periodStart = amount.Item1;
                    }
                    else
                    {
                        if (amount.Item2 > currentPeriodMax)
                            currentPeriodMax = amount.Item2;
                    }
                }

                dailyTotal += currentPeriodMax;
                totalTax += Math.Min(dailyTotal, maxDailyCharge);
            }

            return totalTax;
        }
    }
}
