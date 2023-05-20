using AutoMapper;
using dotnetapi.Models.Master;
using dotnetapi.ViewModels.Master;

namespace dotnetapi.Profiles.Master
{
	public class TagProfile : Profile
    {
		public TagProfile()
		{
            CreateMap<TagViewModel, Tag>()
				.ForMember(x => x.Name, cd => cd.MapFrom(map => map.name))
            ;

            CreateMap<Tag, TagViewModel>()
                .ForMember(x => x.name, cd => cd.MapFrom(map => map.Name))
            ;

            CreateMap<TagRequestViewModel, Tag>()
				.ForMember(x => x.Name, cd => cd.MapFrom(map => map.name))
            ;

            CreateMap<Tag, TagRequestViewModel>()
                .ForMember(x => x.name, cd => cd.MapFrom(map => map.Name))
            ;

            CreateMap<TagResponseViewModel, Tag>()
				.ForMember(x => x.Name, cd => cd.MapFrom(map => map.name))
                .ForMember(x => x.id, cd => cd.MapFrom(map => map.id))
            ;

            CreateMap<Tag, TagResponseViewModel>()
                .ForMember(x => x.name, cd => cd.MapFrom(map => map.Name))
                .ForMember(x => x.id, cd => cd.MapFrom(map => map.id))
            ;
        }
	}
}