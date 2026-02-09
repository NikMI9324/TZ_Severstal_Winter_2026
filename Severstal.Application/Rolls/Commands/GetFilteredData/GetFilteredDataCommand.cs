using MediatR;
using Severstal.Application.Rolls.Dtos;

namespace Severstal.Application.Rolls.Commands.GetFilteredData
{
    public record GetFilteredDataCommand : IRequest<List<RollDto>>
    {
        public int? IdFrom { get; init; }
        public int? IdTo { get; init; }
        public double? WeightFrom { get; init; }
        public double? WeightTo { get; init; }
        public double? LengthFrom { get; init; }
        public double? LengthTo { get; init; }
        public DateTime? DateAddFrom { get; init; }
        public DateTime? DateAddTo { get; init; }
        public DateTime? DateRemoveFrom { get; init; }
        public DateTime? DateRemoveTo { get; init; }
    }
}
