using AutoMapper;
using DataAccess.Entities;
using WriterPlatformWeb.Models.ViewModel;
using WriterPlatformWeb.Models.Work;

namespace WriterPlatformWeb.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AuthorEntity, AuthorDTO>().ReverseMap();

        CreateMap<CommentEntity, CommentDTO>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Login))
            .ForMember(dest => dest.WorkName, opt => opt.MapFrom(src => src.Works.Title))
            .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Text))
            .ReverseMap();

        CreateMap<GenreEntity, GenreDTO>()
            .ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.Name))
            .ReverseMap();

        CreateMap<RatingEntity, RatingDTO>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Login))
            .ForMember(dest => dest.WorkName, opt => opt.MapFrom(src => src.Works.Title))
            .ReverseMap()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.UserId))
            .ForMember(dest => dest.WorksId, opt => opt.MapFrom(src => src.Work.WorkId));

        CreateMap<RoleEntity, RoleDTO>()
            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Users))
            .ReverseMap();

        CreateMap<UserEntity, UserDTO>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Login))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.PasswordHash))
            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name))
            .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
            .ForMember(dest => dest.Ratings, opt => opt.MapFrom(src => src.Ratings))
            .ReverseMap()
            .ForMember(dest => dest.Login, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => new RoleEntity { Name = src.RoleName }))
            .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
            .ForMember(dest => dest.Ratings, opt => opt.MapFrom(src => src.Ratings));

        CreateMap<WorkEntity, WorkDTO>()
            .ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.Genre.Name))
            .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.FirstName + " " + src.Author.LastName))
            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre))
            .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
            .ForMember(dest => dest.Ratings, opt => opt.MapFrom(src => src.Ratings))
            .ReverseMap()
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre))
            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
            .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
            .ForMember(dest => dest.Ratings, opt => opt.MapFrom(src => src.Ratings));

        CreateMap<WorkDTO, WorkViewModel>()
           .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => src.AverageRating ?? 0));
    }
}