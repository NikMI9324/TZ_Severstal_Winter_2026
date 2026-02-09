using System;
using System.Collections.Generic;
using System.Text;

namespace Severstal.Domain.Interfaces
{
    public interface IRollStatisticsRepository
    {
        Task<int> GetAddedRollsCountAsync(DateTime from, DateTime to, 
            CancellationToken cancellationToken = default);
        Task<int> GetRemovedRollsCountAsync(DateTime from, DateTime to,
            CancellationToken cancellationToken = default);
        Task<double> GetAverageLengthAsync(DateTime from, DateTime to,
            CancellationToken cancellationToken = default);
        Task<double> GetAverageWeightAsync(DateTime from, DateTime to,
            CancellationToken cancellationToken = default);
        Task<double> GetMinLengthAsync(DateTime from, DateTime to,
            CancellationToken cancellationToken = default);
        Task<double> GetMaxLengthAsync(DateTime from, DateTime to,
            CancellationToken cancellationToken = default);
        Task<double> GetMaxWeightAsync(DateTime from, DateTime to,
            CancellationToken cancellationToken = default);
        Task<double> GetMinWeightAsync(DateTime from, DateTime to,
            CancellationToken cancellationToken = default);
        Task<double> GetSummaryWeightAsync(DateTime from, DateTime to,
            CancellationToken cancellationToken = default);
        Task<double> GetMaxGapAsync(DateTime from, DateTime to,
            CancellationToken cancellationToken = default);
        Task<double> GetMinGapAsync(DateTime from, DateTime to,
            CancellationToken cancellationToken = default);
        Task<DateTime> GetDayWithMinRollCountAsync(DateTime from, DateTime to,
            CancellationToken cancellationToken = default);
        Task<DateTime> GetDayWithMaxRollCountAsync(DateTime from, DateTime to,
            CancellationToken cancellationToken = default);
        Task<DateTime> GetDayWithMinTotalWeightAsync(DateTime from, DateTime to,
            CancellationToken cancellationToken = default);
        Task<DateTime> GetDayWithMaxTotalWeightAsync(DateTime from, DateTime to,
            CancellationToken cancellationToken = default);
    }
}
