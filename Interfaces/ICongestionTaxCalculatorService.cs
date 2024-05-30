using System;
using System.Collections.Generic;
using CongestionTaxAPI.Business.Business;
using CongestionTaxAPI.DataAccess;
using CongestionTaxAPI.Models;

namespace CongestionTaxAPI.Business
{
    public interface ICongestionTaxCalculatorService
    {
        public int CalculateTax(string vehicleType, List<string> dates, string city);    
    }
}
