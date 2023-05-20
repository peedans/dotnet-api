using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Models.Master;
using app.ViewModels.Master;
using AutoMapper;

namespace app.Profiles.Master
{
    public class BlogPostCategoryProfile : Profile
    {
        public BlogPostCategoryProfile()
		{
             CreateMap<BlogPostCategoryViewModel, BlogPostCategory>()
				.ForMember(x => x.BlogPostId, cd => cd.MapFrom(map => map.blogpost_id))
                .ForMember(x => x.CategoryId, cd => cd.MapFrom(map => map.category_id))
                .ForMember(x => x.IsActive, cd => cd.MapFrom(map => map.is_active))
            ;

            CreateMap<BlogPostCategory, BlogPostCategoryViewModel>()
                .ForMember(x => x.blogpost_id, cd => cd.MapFrom(map => map.BlogPostId))
                .ForMember(x => x.category_id, cd => cd.MapFrom(map => map.CategoryId))
                .ForMember(x => x.is_active, cd => cd.MapFrom(map => map.IsActive))
            ;

            CreateMap<BlogPostCategoryRequestActiveViewModel, BlogPostCategory>()
				.ForMember(x => x.BlogPostId, cd => cd.MapFrom(map => map.blogpost_id))
                .ForMember(x => x.CategoryId, cd => cd.MapFrom(map => map.category_id))
            ;

            CreateMap<BlogPostCategory, BlogPostCategoryRequestActiveViewModel>()
                .ForMember(x => x.blogpost_id, cd => cd.MapFrom(map => map.BlogPostId))
                .ForMember(x => x.category_id, cd => cd.MapFrom(map => map.CategoryId))
            ;


        }
    }
}