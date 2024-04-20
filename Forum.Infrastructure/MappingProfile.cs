using AutoMapper;
using Forum.Application.Dto;
using Forum.Domain.Models;
using Forum.Web.Api.Controllers;

namespace Forum.Infrastructure;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Comment, CommentDetailDto>()
            .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator));
       
        
        
        
 CreateMap<User, UserDetailsDto>()
    .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
        
        
        
        
        
        CreateMap<Topic, TopicDetailDto>()
            .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.Creator))
            .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments));
        CreateMap<CreateCommentDto, Comment>();
        CreateMap<UpdateCommentDto, Comment>();
        CreateMap<CreateTopicDto, Topic>();
        CreateMap<UpdateTopicDto, Topic>();
    }


    
}