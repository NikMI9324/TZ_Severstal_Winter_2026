using AutoMapper;
using MediatR;
using Severstal.Application.Rolls.Dtos;
using Severstal.Domain.Interfaces;

namespace Severstal.Application.Rolls.Commands.RemoveRoll
{
    public class RemoveRollCommandHandler : IRequestHandler<RemoveRollCommand, RemoveRollResponse>
    {
        private readonly IRollCrudRepository _repo;
        private readonly IMapper _mapper;


        public RemoveRollCommandHandler(IRollCrudRepository repo, IMapper mapper) => (_repo, _mapper) = (repo, mapper); 

        public async Task<RemoveRollResponse> Handle(RemoveRollCommand request, CancellationToken cancellationToken)
        {
            var roll = await _repo.RemoveRollAsync(request.Id, cancellationToken);
            var response = _mapper.Map<RemoveRollResponse>(roll);
            response = response with { Message = "Рулон успешно удален" };
            return response;
        }
    }
}
