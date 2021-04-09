using System;
using System.Collections.Generic;
using JOIEnergy.Domain;
using JOIEnergy.Models;
using JOIEnergy.Services;
using Microsoft.AspNetCore.Mvc;

namespace JOIEnergy.Controllers
{
    [Route("cost")]
    public class CostController : Controller
    {
        private readonly IMeterReadingService _meterReadingService;
        private readonly IAccountService accountService;
        private readonly IPricePlanService pricePlanService;

        public CostController(IMeterReadingService meterReadingService, IAccountService accountService, IPricePlanService pricePlanService)
        {
            _meterReadingService = meterReadingService;
            this.accountService = accountService;
            this.pricePlanService = pricePlanService;
        }

        [HttpGet("last-week/{smartMeterId}")]
        public ObjectResult GetCostForLastWeek(string smartMeterId)
        {
            DateTime[] dateTimes = GetLastWeekDates();
            List<ElectricityReading> electricityReadings = _meterReadingService.GetReadings(smartMeterId, dateTimes[0], dateTimes[1]);
            Enums.Supplier supplier = accountService.GetPricePlanIdForSmartMeterId(smartMeterId);
            decimal cost = pricePlanService.GetConsumptionCost(electricityReadings, supplier);
            return new OkObjectResult(new CostResponse() { FromDate = dateTimes[0], ToDate = dateTimes[1], Cost = cost });
        }

        private DateTime[] GetLastWeekDates()
        {
            DayOfWeek currentDay = DateTime.Now.DayOfWeek;
            int daysTillCurrentDay = currentDay - DayOfWeek.Monday;
            DateTime currentWeekStartDate = DateTime.Now.AddDays(-daysTillCurrentDay);
            return new DateTime[] { currentWeekStartDate.AddDays(-7), currentWeekStartDate.AddDays(-1) };
        }
    }
}
