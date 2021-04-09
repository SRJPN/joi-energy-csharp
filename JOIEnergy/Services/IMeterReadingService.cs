using System;
using System.Collections.Generic;
using JOIEnergy.Domain;

namespace JOIEnergy.Services
{
    public interface IMeterReadingService
    {
        List<ElectricityReading> GetReadings(string smartMeterId, DateTime? startDate = null, DateTime? endDate = null);
        void StoreReadings(string smartMeterId, List<ElectricityReading> electricityReadings);
    }
}