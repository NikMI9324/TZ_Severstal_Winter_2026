using MediatR;
using Severstal.Application.Rolls.Dtos;

namespace Severstal.Application.Rolls.Commands.AddRoll
{
    public record AddRollCommand : IRequest<CreateRollResponse>
    {
        public double Length { get; init; }
        public double Weight { get; init; }

    }
}
