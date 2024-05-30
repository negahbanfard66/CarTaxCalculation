using CongestionTaxAPI.Business;
using CongestionTaxAPI.DataAccess;
using CongestionTaxAPI.DTO;
using CongestionTaxAPI.Interfaces;
using CongestionTaxAPI.Intrefaces;
using Microsoft.AspNetCore.Mvc;
using TaxCalculator.Models;

namespace CongestionTaxAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CongestionTaxController : ControllerBase
    {
        private readonly ICongestionTaxCalculatorService _calculator;
        private readonly ITaxRuleService _taxRuleService;
        private readonly IDatabaseContext _databaseContext;

        public CongestionTaxController(IDatabaseContext databaseContext, ICongestionTaxCalculatorService calculator, ITaxRuleService taxRuleService)
        {
            _databaseContext = databaseContext;
            _calculator = calculator;
            _taxRuleService = taxRuleService;
        }

        [HttpPost("calculate")]
        public IActionResult Calculate([FromBody] TaxCalculationRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.VehicleType) || request.Dates == null || request.Dates.Count == 0)
            {
                return BadRequest("Invalid request");
            }

            var totalTax = _calculator.CalculateTax(request.VehicleType, request.Dates,request.City= "Gothenburg");
            return Ok(new { TotalTax = totalTax });
        }

        [HttpPost("create")]
        public IActionResult CreateCityAndTaxRule([FromBody] List<TaxRules> taxRules,int maxDailyCharge)
        {
            _taxRuleService.InsertData(taxRules.FirstOrDefault().City,taxRules,maxDailyCharge);
            return Ok();
        }
    }
}
