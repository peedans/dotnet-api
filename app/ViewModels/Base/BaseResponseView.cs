using System;
using dotnetapi.ViewModels.Paginations;

namespace dotnetapi.ViewModels.Base
{
	public class BaseResponseView<T>
	{
        public T? data { get; set; }
        public bool success { get; set; } = true;
        public int error_code { get; set; }
        public string error_message { get; set; } = string.Empty;
    }

    public class BaseResponsePageView<T>
    {
        public BaseResponsePageDataModel<T>? data { get; set; }
        public bool success { get; set; } = true;
        public int error_code { get; set; }
        public string error_message { get; set; } = string.Empty;
    }

    public class BaseResponsePageDataModel<T>
    {
        public T? list { get; set; }
        public PageRequestViewModel? paging { get; set; }
    }
}

