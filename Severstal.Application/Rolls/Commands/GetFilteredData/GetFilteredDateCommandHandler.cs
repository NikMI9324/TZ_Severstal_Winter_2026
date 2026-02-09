using AutoMapper;
using MediatR;
using Severstal.Application.Rolls.Dtos;
using Severstal.Domain.Interfaces;

namespace Severstal.Application.Rolls.Commands.GetFilteredData
{
    public class GetFilteredDateCommandHandler : IRequestHandler<GetFilteredDataCommand, List<RollDto>>
    {
        private readonly IRollCrudRepository _repo;
        private readonly IMapper _mapper;

        public GetFilteredDateCommandHandler(IRollCrudRepository repo, IMapper mapper) => (_repo, _mapper) = (repo, mapper);
        public async Task<List<RollDto>> Handle(GetFilteredDataCommand request, CancellationToken cancellationToken)
        {
            var rolls = await _repo.GetFilteredDataAsync(
                request.IdFrom, 
                request.IdTo, 
                request.WeightFrom, 
                request.WeightTo, 
                request.LengthFrom, 
                request.LengthTo, 
                request.DateAddFrom, 
                request.DateAddTo, 
                request.DateRemoveFrom, 
                request.DateRemoveTo);
            return _mapper.Map<List<RollDto>>(rolls);
        }
    }
}
