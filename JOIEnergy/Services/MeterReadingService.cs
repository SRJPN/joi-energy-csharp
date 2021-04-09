using System;
using System.Collections.Generic;
using System.Linq;
using JOIEnergy.Domain;

namespace JOIEnergy.Services
{
    public class MeterReadingService : IMeterReadingService
    {
        public Dictionary<string, List<ElectricityReading>> MeterAssociatedReadings { get; set; }
        public MeterReadingService(Dictionary<string, List<ElectricityReading>> meterAssociatedReadings)
        {
            MeterAssociatedReadings = meterAssociatedReadings;
        }

        public List<ElectricityReading> GetReadings(string smartMeterId, DateTime? startDate = null, DateTime? endDate = null)
        {
            if (MeterAssociatedReadings.ContainsKey(smartMeterId))
            {
                var readings = MeterAssociatedReadings[smartMeterId];
                if (startDate != null)
                    readings = readings.Where(x => x.Time >= startDate).ToList();
                if (endDate != null)
                    readings = readings.Where(x => x.Time <= endDate).ToList();
                return readings;
            }
            return new List<ElectricityReading>();
        }

        public void StoreReadings(string smartMeterId, List<ElectricityReading> electricityReadings)
        {
            if (!MeterAssociatedReadings.ContainsKey(smartMeterId))
            {
                MeterAssociatedReadings.Add(smartMeterId, new List<ElectricityReading>());
            }

            electricityReadings.ForEach(electricityReading => MeterAssociatedReadings[smartMeterId].Add(electricityReading));
        }
    }
}
