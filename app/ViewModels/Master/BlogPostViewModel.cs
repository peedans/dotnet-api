using System.ComponentModel.DataAnnotations;
using app.ViewModels.Master;
using dotnetapi.ViewModels.Base;
using dotnetapi.ViewModels.Paginations;

namespace dotnetapi.ViewModels.Master
{
    public class BlogPostViewModel : BaseViewModel
    {
        public string? title { get; set; }
        public string? content { get; set; }
        public string? cover_image { get; set; }
        public long? author_id { get; set; }
        public string? author_name { get; set; }
        public List<TagResponseViewModel>? tags { get; set; }
    }

    public class BlogPostRequestViewModel
    {
        [Required]
        public string? title { get; set; }
        public string? content { get; set; }
        public string? cover_image { get; set; }
        [Required]
        [Range(1,long.MaxValue)]
        public long? author_id { get; set; }
        public List<TagRequestViewModel>? tags { get; set; }
    }

    public class BlogPostRequestUpdateViewModel
    {
        [Required]
        public string? title { get; set; }
        public string? content { get; set; }
        public string? cover_image { get; set; }
    }

    public class BlogPostResponseViewModel
    {
        public long? id { get; set; }
        public string? title { get; set; }
        public string? content { get; set; }
        public string? cover_image { get; set; }
        public long? author_id { get; set; }
        public string? author_name { get; set; }
        public List<TagResponseViewModel>? tags { get; set; }

        public List<IsActiveCategoryResponseViewModel>? category { get; set; }
    }

    public class BlogPostRequestPageViewModel : PageRequestViewModel
    {
    public FilterBlogPostViewModel? filter { get; set; }
    }

    public class FilterBlogPostViewModel
    {
        public string? title { get; set; }
        public string? content { get; set; }
        public long? author_id { get; set; }
        public List<int>? tags { get; set; }
    }
}