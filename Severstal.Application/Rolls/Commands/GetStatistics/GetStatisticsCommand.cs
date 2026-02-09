using MediatR;
using Severstal.Application.Rolls.Dtos;

namespace Severstal.Application.Rolls.Commands.GetStatistics
{
    public record GetStatisticsCommand : IRequest<RollStatisticsDto>
    {
        public DateTime From { get; init; }
        public DateTime To { get; init; }
    }
}
