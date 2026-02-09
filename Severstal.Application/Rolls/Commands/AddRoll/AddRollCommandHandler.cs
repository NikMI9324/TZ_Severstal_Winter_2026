using AutoMapper;
using MediatR;
using Severstal.Application.Rolls.Dtos;
using Severstal.Domain.Entities;
using Severstal.Domain.Interfaces;

namespace Severstal.Application.Rolls.Commands.AddRoll
{
    public class AddRollCommandHandler : IRequestHandler<AddRollCommand, CreateRollResponse>
    {
        private readonly IRollCrudRepository _repo;
        private readonly IMapper _mapper;
        public AddRollCommandHandler(IRollCrudRepository repo, IMapper mapper) => (_repo, _mapper) = (repo, mapper);
        public async Task<CreateRollResponse> Handle(AddRollCommand request, CancellationToken cancellationToken)
        {
            var roll = new Roll(request.Length, request.Weight);
            await _repo.AddRollAsync(roll, cancellationToken);
            var response = _mapper.Map<CreateRollResponse>(roll);
            response = response with { Message = "Рулон успешно добавлен" };
            return response;
        }
    }
}
