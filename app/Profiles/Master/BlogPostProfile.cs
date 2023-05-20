using System;
using System.Data;
using app.ViewModels.Master;
using AutoMapper;
using dotnetapi.Models.Master;
using dotnetapi.ViewModels.Master;

namespace dotnetapi.Profiles.Master
{
    public class BlogPostProfile : Profile
    {
        public BlogPostProfile()
        {
            CreateMap<BlogPostViewModel, BlogPost>()
                .ForMember(x => x.Title, cd => cd.MapFrom(map => map.title))
                .ForMember(x => x.Content, cd => cd.MapFrom(map => map.content))
                .ForMember(x => x.CoverImage, cd => cd.MapFrom(map => map.cover_image))
                .ForMember(x => x.AuthorId, cd => cd.MapFrom(map => map.author_id))
                .ForMember(x => x.BlogPostTags, cd => cd.MapFrom(map => map.tags))
                .AfterMap((s, d) => d.Author!.Name = s.author_name!)

            ;

            // CreateMap<BlogPost, BlogPostViewModel>()
            //     .ForMember(x => x.title, cd => cd.MapFrom(map => map.Title))
            //     .ForMember(x => x.content, cd => cd.MapFrom(map => map.Content))
            //     .ForMember(x => x.cover_image, cd => cd.MapFrom(map => map.CoverImage))
            //     .ForMember(x => x.author_id, cd => cd.MapFrom(map => map.AuthorId))
            //     .ForMember(x => x.tags, cd => cd.MapFrom(map => map.BlogPostTags.Select(t => new TagResponseViewModel { id = t.TagId, name = t.Tag.Name })))
            //     .AfterMap((s, d) => d.author_name = s.Author.Name);
            // ;

            CreateMap<BlogPostRequestViewModel, BlogPost>()
               .ForMember(x => x.Title, cd => cd.MapFrom(map => map.title))
               .ForMember(x => x.Content, cd => cd.MapFrom(map => map.content))
               .ForMember(x => x.CoverImage, cd => cd.MapFrom(map => map.cover_image))
               .ForMember(x => x.AuthorId, cd => cd.MapFrom(map => map.author_id))
           ;

            CreateMap<BlogPostRequestUpdateViewModel, BlogPost>()
               .ForMember(x => x.Title, cd => cd.MapFrom(map => map.title))
               .ForMember(x => x.Content, cd => cd.MapFrom(map => map.content))
               .ForMember(x => x.CoverImage, cd => cd.MapFrom(map => map.cover_image))
           ;

            // CreateMap<BlogPost, BlogPostRequestViewModel>()
            //     .ForMember(x => x.title, cd => cd.MapFrom(map => map.Title))
            //     .ForMember(x => x.content, cd => cd.MapFrom(map => map.Content))
            //     .ForMember(x => x.cover_image, cd => cd.MapFrom(map => map.CoverImage))
            //     .ForMember(x => x.author_id, cd => cd.MapFrom(map => map.AuthorId))
                
            // ;

        //     CreateMap<BlogPostResponseViewModel, BlogPost>()              
        //         .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.title))
        //         .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.content))
        //         .ForMember(dest => dest.CoverImage, opt => opt.MapFrom(src => src.cover_image))
        //         .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.author_id))
        //    ;

            CreateMap<BlogPost, BlogPostResponseViewModel>()
                .ForMember(x => x.id, cd => cd.MapFrom(x => x.id))
                .ForMember(x => x.title, cd => cd.MapFrom(map => map.Title))
                .ForMember(x => x.content, cd => cd.MapFrom(map => map.Content))
                .ForMember(x => x.cover_image, cd => cd.MapFrom(map => map.CoverImage))
                .ForMember(x => x.author_id, cd => cd.MapFrom(map => map.AuthorId))
                .ForMember(x => x.tags, cd => cd.MapFrom(map => map.BlogPostTags!.Select(t => new TagResponseViewModel { id = t.TagId, name = t.Tag!.Name })))
                .ForMember(x => x.category, cd => cd.MapFrom(map => map.BlogPostCategory!.Select(t => new IsActiveCategoryResponseViewModel { id = t.CategoryId, name = t.Category.Name, description = t.Category.Description, is_active = t.IsActive })))
                .AfterMap((s, d) => d.author_name = s.Author!.Name);
            ;

        }
    }
}

