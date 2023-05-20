using System;
using System.Data;
using AutoMapper;
using dotnetapi.Models.Master;
using dotnetapi.ViewModels.Master;

namespace dotnetapi.Profiles.Master
{
	public class AuthorProfile : Profile
    {
		public AuthorProfile()
		{
            CreateMap<AuthorViewModel, Author>()
				.ForMember(x => x.Name, cd => cd.MapFrom(map => map.name))
                .ForMember(x => x.EmailAddress, cd => cd.MapFrom(map => map.email_address))
            ;

            CreateMap<Author, AuthorViewModel>()
                .ForMember(x => x.name, cd => cd.MapFrom(map => map.Name))
                .ForMember(x => x.email_address, cd => cd.MapFrom(map => map.EmailAddress))
            ;

            CreateMap<AuthorRequestViewModel, Author>()
				.ForMember(x => x.Name, cd => cd.MapFrom(map => map.name))
                .ForMember(x => x.EmailAddress, cd => cd.MapFrom(map => map.email_address))
            ;

            CreateMap<Author, AuthorRequestViewModel>()
                .ForMember(x => x.name, cd => cd.MapFrom(map => map.Name))
                .ForMember(x => x.email_address, cd => cd.MapFrom(map => map.EmailAddress))
            ;

            CreateMap<AuthorResponseViewModel, Author>()
                .ForMember(x => x.id, cd => cd.MapFrom(map => map.id))
				.ForMember(x => x.Name, cd => cd.MapFrom(map => map.name))
                .ForMember(x => x.EmailAddress, cd => cd.MapFrom(map => map.email_address))
            ;

            CreateMap<Author, AuthorResponseViewModel>()
                .ForMember(x => x.id, cd => cd.MapFrom(map => map.id))
                .ForMember(x => x.name, cd => cd.MapFrom(map => map.Name))
                .ForMember(x => x.email_address, cd => cd.MapFrom(map => map.EmailAddress))
            ;
        }
	}
}

