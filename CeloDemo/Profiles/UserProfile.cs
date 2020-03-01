using AutoMapper;
using CeloDemo.Entities;
using CeloDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CeloDemo.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<OrgUser, User>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(dest => dest.FirstName,
                opt => opt.MapFrom(src => src.name.first))
                .ForMember(dest => dest.LastName,
                opt => opt.MapFrom(src => src.name.last))
                .ForMember(dest => dest.Title,
                opt => opt.MapFrom(src => src.name.title))
                .ForMember(dest => dest.Birth,
                opt => opt.MapFrom(src => src.dob.date))
                .ForMember(dest => dest.LargePicture,
                opt => opt.MapFrom(src => src.picture.large))
                .ForMember(dest => dest.ThumbnailPicture,
                opt => opt.MapFrom(src => src.picture.thumbnail));
            CreateMap<User, UserViewModel>();
            CreateMap<UserViewModel, User>();
        }
    }
}
