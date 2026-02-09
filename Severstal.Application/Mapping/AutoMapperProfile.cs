using AutoMapper;
using Severstal.Application.Rolls.Commands.AddRoll;
using Severstal.Application.Rolls.Commands.RemoveRoll;
using Severstal.Application.Rolls.Dtos;
using Severstal.Domain.Entities;

namespace Severstal.Application.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddRollCommand, Roll>();
            CreateMap<Roll, CreateRollResponse>()
                .ForMember(dest => dest.Message, opt => opt.Ignore());
            CreateMap<RemoveRollCommand, Roll>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
            CreateMap<Roll, RemoveRollResponse>()
                .ForMember(dest => dest.Message, opt => opt.Ignore());
            CreateMap<Roll, RollDto>();
        }
    }
}
