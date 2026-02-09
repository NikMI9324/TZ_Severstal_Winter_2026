using System;
using System.Collections.Generic;
using System.Text;

namespace Severstal.Application.Rolls.Dtos
{
    public record RollStatisticsDto
    {
        public int AddedRollsCount { get; init; }
        public int RemovedRollsCount { get; init; }
        public double AverageLength { get; init; }
        public double AverageWeight { get; init; }
        public double MinLength { get; init; }
        public double MaxLength { get; init; }
        public double MinWeight { get; init; }
        public double MaxWeight { get; init; }
        public double SummaryWeight { get; init; }
        public double MaxGap { get; init; }
        public double MinGap { get; init; }
        public DateTime DayWithMinRollCount { get; init; }
        public DateTime DayWithMaxRollCount { get; init; }
        public DateTime DayWithMinTotalWeight { get; init; }
        public DateTime DayWithMaxTotalWeight { get; init; }
    }
}
