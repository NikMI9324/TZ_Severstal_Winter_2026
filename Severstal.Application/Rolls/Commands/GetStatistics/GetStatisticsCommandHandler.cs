using MediatR;
using Severstal.Application.Rolls.Dtos;
using Severstal.Domain.Interfaces;

namespace Severstal.Application.Rolls.Commands.GetStatistics
{
    public class GetStatisticsCommandHandler : IRequestHandler<GetStatisticsCommand, RollStatisticsDto>
    {
        private readonly IRollStatisticsRepository _repo;
        public GetStatisticsCommandHandler(IRollStatisticsRepository repo) => _repo = repo;
        public async Task<RollStatisticsDto> Handle(GetStatisticsCommand request, CancellationToken cancellationToken)
        {
            var from = request.From;
            var to = request.To;

            var addedCountTask = _repo.GetAddedRollsCountAsync(from, to, cancellationToken);
            var removedCountTask = _repo.GetRemovedRollsCountAsync(from, to, cancellationToken);
            var avgLengthTask = _repo.GetAverageLengthAsync(from, to, cancellationToken);
            var avgWeightTask = _repo.GetAverageWeightAsync(from, to, cancellationToken);
            var minLengthTask = _repo.GetMinLengthAsync(from, to, cancellationToken);
            var maxLengthTask = _repo.GetMaxLengthAsync(from, to, cancellationToken);
            var minWeightTask = _repo.GetMinWeightAsync(from, to, cancellationToken);
            var maxWeightTask = _repo.GetMaxWeightAsync(from, to, cancellationToken);
            var summaryWeightTask = _repo.GetSummaryWeightAsync(from, to, cancellationToken);
            var maxGapTask = _repo.GetMaxGapAsync(from, to, cancellationToken);
            var minGapTask = _repo.GetMinGapAsync(from, to, cancellationToken);
            var minCountDayTask = _repo.GetDayWithMinRollCountAsync(from, to, cancellationToken);
            var maxCountDayTask = _repo.GetDayWithMaxRollCountAsync(from, to, cancellationToken);
            var minWeightDayTask = _repo.GetDayWithMinTotalWeightAsync(from, to, cancellationToken);
            var maxWeightDayTask = _repo.GetDayWithMaxTotalWeightAsync(from, to, cancellationToken);

            await Task.WhenAll(
                addedCountTask,
                removedCountTask,
                avgLengthTask,
                avgWeightTask,
                minLengthTask,
                maxLengthTask,
                minWeightTask,
                maxWeightTask,
                summaryWeightTask,
                maxGapTask,
                minGapTask,
                minCountDayTask,
                maxCountDayTask,
                minWeightDayTask,
                maxWeightDayTask);

            return new RollStatisticsDto
            {
                AddedRollsCount = addedCountTask.Result,
                RemovedRollsCount = removedCountTask.Result,
                AverageLength = avgLengthTask.Result,
                AverageWeight = avgWeightTask.Result,
                MinLength = minLengthTask.Result,
                MaxLength = maxLengthTask.Result,
                MinWeight = minWeightTask.Result,
                MaxWeight = maxWeightTask.Result,
                SummaryWeight = summaryWeightTask.Result,
                MaxGap = maxGapTask.Result,
                MinGap = minGapTask.Result,
                DayWithMinRollCount = minCountDayTask.Result,
                DayWithMaxRollCount = maxCountDayTask.Result,
                DayWithMinTotalWeight = minWeightDayTask.Result,
                DayWithMaxTotalWeight = maxWeightDayTask.Result,
            };
        }
    }
}
