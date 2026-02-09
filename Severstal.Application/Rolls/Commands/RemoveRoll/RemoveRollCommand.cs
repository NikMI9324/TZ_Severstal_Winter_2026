using MediatR;
using Severstal.Application.Rolls.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Severstal.Application.Rolls.Commands.RemoveRoll
{
    public record RemoveRollCommand : IRequest<RemoveRollResponse>
    {
        public int Id { get; init; }
    }

}
