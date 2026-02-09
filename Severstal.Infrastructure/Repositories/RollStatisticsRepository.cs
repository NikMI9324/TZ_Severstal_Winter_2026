using Microsoft.EntityFrameworkCore;
using Severstal.Domain.Entities;
using Severstal.Domain.Interfaces;
using Severstal.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Severstal.Infrastructure.Repositories
{
    public class RollStatisticsRepository : IRollStatisticsRepository
    {
        private readonly IDbContextFactory<AppDbContext> _contextFactory;

        public RollStatisticsRepository(IDbContextFactory<AppDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        private async Task<AppDbContext> CreateContextAsync(CancellationToken ct = default)
        {
            var ctx = await _contextFactory.CreateDbContextAsync(ct);
            ctx.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return ctx;
        }
        private IQueryable<Roll> AggregateRolls(DateTime from, DateTime to, AppDbContext context)
        {

            return context.Rolls
                .Where(r => r.AddedDate <= to &&
                (r.RemovedDate == null || r.RemovedDate >= from))
                .AsNoTracking();
        }

        public async Task<int> GetAddedRollsCountAsync(DateTime from, DateTime to,
            CancellationToken cancellationToken = default)
        {
            await using var context = await CreateContextAsync(cancellationToken);
            return await context.Rolls
                .Where(r => r.AddedDate >= from && r.AddedDate <= to)
                .CountAsync(cancellationToken);
        }

        public async Task<double> GetAverageLengthAsync(DateTime from, DateTime to,
            CancellationToken cancellationToken = default)
        {
            await using var context = await CreateContextAsync(cancellationToken);
            var query = AggregateRolls(from, to, context);
            return await query.Select(r => (double?)r.Length).AverageAsync(cancellationToken) ?? 0;
        }

        public async Task<double> GetAverageWeightAsync(DateTime from, DateTime to,
            CancellationToken cancellationToken = default)
        {
            await using var context = await CreateContextAsync(cancellationToken);
            var query = AggregateRolls(from, to, context);
            return await query.Select(r => (double?)r.Weight).AverageAsync(cancellationToken) ?? 0;
        }



        public async Task<double> GetMaxGapAsync(DateTime from, DateTime to,
            CancellationToken cancellationToken = default)
        {
            await using var context = await CreateContextAsync(cancellationToken);
            var query = AggregateRolls(from, to, context);
            var timeDiff = await query.Select(r => (r.RemovedDate - r.AddedDate)).MaxAsync(cancellationToken);
            return timeDiff?.TotalDays ?? 0;
        }

        public async Task<double> GetMaxLengthAsync(DateTime from, DateTime to,
            CancellationToken cancellationToken = default)
        {
            await using var context = await CreateContextAsync(cancellationToken);

            var query = AggregateRolls(from, to, context);
            return await query.Select(r => r.Length).MaxAsync(cancellationToken);
        }

        public async Task<double> GetMaxWeightAsync(DateTime from, DateTime to,
            CancellationToken cancellationToken = default)
        {
            await using var context = await CreateContextAsync(cancellationToken);

            var query = AggregateRolls(from, to, context);
            return await query.Select(r => r.Weight).MaxAsync(cancellationToken);
        }

        public async Task<double> GetMinGapAsync(DateTime from, DateTime to,
            CancellationToken cancellationToken = default)
        {
            await using var context = await CreateContextAsync(cancellationToken);

            var query = AggregateRolls(from, to, context);
            var timeDiff = await query.Select(r => (r.RemovedDate - r.AddedDate)).MinAsync(cancellationToken);
            return timeDiff?.TotalDays ?? 0;
        }

        public async Task<double> GetMinLengthAsync(DateTime from, DateTime to,
            CancellationToken cancellationToken = default)
        {
            await using var context = await CreateContextAsync(cancellationToken);

            var query = AggregateRolls(from, to, context);
            return await query.Select(r => r.Length).MinAsync(cancellationToken);
        }

        public async Task<double> GetMinWeightAsync(DateTime from, DateTime to,
            CancellationToken cancellationToken = default)
        {
            await using var context = await CreateContextAsync(cancellationToken);

            var query = AggregateRolls(from, to, context);
            return await query.Select(r => r.Weight).MinAsync(cancellationToken);
        }

        public async Task<int> GetRemovedRollsCountAsync(DateTime from, DateTime to,
            CancellationToken cancellationToken = default)
        {
            await using var context = await CreateContextAsync(cancellationToken);

            return await context.Rolls
                .Where(r => r.RemovedDate != null &&
                       r.RemovedDate >= from &&
                       r.RemovedDate <= to)
                .CountAsync(cancellationToken);
        }

        public async Task<double> GetSummaryWeightAsync(DateTime from, DateTime to,
            CancellationToken cancellationToken = default)
        {
            await using var context = await CreateContextAsync(cancellationToken);

            var query = AggregateRolls(from, to, context);
            return await query.Select(r => r.Weight).SumAsync(cancellationToken);
        }

        public async Task<DateTime> GetDayWithMinRollCountAsync(DateTime from, DateTime to, CancellationToken cancellationToken = default)
        {
            await using var context = await CreateContextAsync(cancellationToken);

            var rolls = await context.Rolls
                .Where(r => r.AddedDate <= to && (r.RemovedDate == null || r.RemovedDate >= from))
                .Select(r => new
                {
                    Added = r.AddedDate.Date,
                    Removed = r.RemovedDate.HasValue ? r.RemovedDate.Value.Date : (DateTime?)null
                })
                .ToListAsync(cancellationToken);

            int minCount = int.MaxValue;
            DateTime minDay = from;

            for (DateTime day = from.Date; day <= to.Date; day = day.AddDays(1))
            {
                int count = rolls.Count(r => r.Added <= day && (r.Removed == null || r.Removed > day));
                if (count < minCount)
                {
                    minCount = count;
                    minDay = day;
                }
            }

            return minDay;
        }

        public async Task<DateTime> GetDayWithMaxRollCountAsync(DateTime from, DateTime to, CancellationToken cancellationToken = default)
        {
            await using var context = await CreateContextAsync(cancellationToken);

            var rolls = await context.Rolls
                .Where(r => r.AddedDate <= to && (r.RemovedDate == null || r.RemovedDate >= from))
                .Select(r => new
                {
                    Added = r.AddedDate.Date,
                    Removed = r.RemovedDate.HasValue ? r.RemovedDate.Value.Date : (DateTime?)null
                })
                .ToListAsync(cancellationToken);

            int maxCount = -1;
            DateTime maxDay = from;

            for (DateTime day = from.Date; day <= to.Date; day = day.AddDays(1))
            {
                int count = rolls.Count(r => r.Added <= day && (r.Removed == null || r.Removed > day));
                if (count > maxCount)
                {
                    maxCount = count;
                    maxDay = day;
                }
            }

            return maxDay;
        }

        public async Task<DateTime> GetDayWithMinTotalWeightAsync(DateTime from, DateTime to, CancellationToken cancellationToken = default)
        {
            await using var context = await CreateContextAsync(cancellationToken);

            var rolls = await context.Rolls
                .Where(r => r.AddedDate <= to && (r.RemovedDate == null || r.RemovedDate >= from))
                .Select(r => new
                {
                    Added = r.AddedDate.Date,
                    Removed = r.RemovedDate.HasValue ? r.RemovedDate.Value.Date : (DateTime?)null,
                    r.Weight
                })
                .ToListAsync(cancellationToken);

            double minWeight = double.MaxValue;
            DateTime minDay = from;

            for (DateTime day = from.Date; day <= to.Date; day = day.AddDays(1))
            {
                double totalWeight = rolls.
                    Where(r => r.Added <= day &&
                    (r.Removed == null || r.Removed > day))
                    .Sum(r => r.Weight);
                if (totalWeight < minWeight)
                {
                    minWeight = totalWeight;
                    minDay = day;
                }
            }

            return minDay;
        }

        public async Task<DateTime> GetDayWithMaxTotalWeightAsync(DateTime from, DateTime to,
            CancellationToken cancellationToken = default)
        {
            await using var context = await CreateContextAsync(cancellationToken);

            var rolls = await context.Rolls
                .Where(r => r.AddedDate <= to && (r.RemovedDate == null || r.RemovedDate >= from))
                .Select(r => new
                {
                    Added = r.AddedDate.Date,
                    Removed = r.RemovedDate.HasValue ? r.RemovedDate.Value.Date : (DateTime?)null,
                    r.Weight
                })
                .ToListAsync(cancellationToken);

            double maxWeight = -1;
            DateTime maxDay = from;

            for (DateTime day = from.Date; day <= to.Date; day = day.AddDays(1))
            {
                double totalWeight = rolls
                    .Where(r => r.Added <= day &&
                    (r.Removed == null || r.Removed > day))
                    .Sum(r => r.Weight);
                if (totalWeight > maxWeight)
                {
                    maxWeight = totalWeight;
                    maxDay = day;
                }
            }

            return maxDay;
        }
    }
}
