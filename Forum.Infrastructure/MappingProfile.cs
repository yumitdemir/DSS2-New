using AutoMapper;
using Forum.Application.Dto;
using Forum.Domain.Models;

namespace Forum.Infrastructure;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Comment, CommentDetailDto>()
            .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator));
        CreateMap<User, UserDetailsDto>();
        CreateMap<Topic, TopicDetailDto>()
            .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator))
            .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments));
    }


    
}