using AutoMapper;
using System.Collections;
using System.Linq.Dynamic.Core;

namespace dotnetapi.Utils
{
    public interface ISearchUtil
    {
        List<T> SearchText<T>(IQueryable<T> source, List<string>? searchFileds, string searchText);
        string FilterQuery(object filers);
    }

    public class SearchUtil : ISearchUtil
    {
        private readonly IMapper mapper;

        public SearchUtil(IMapper mapper)
        {
            this.mapper = mapper;
        }
        public List<T> SearchText<T>(IQueryable<T> source, List<string>? searchFileds, string searchText)
        {
            var search = (from field in searchFileds
                    let searchResult = source.Where(field + ".Contains(@0)", searchText)
                    from result in searchResult
                    select result).ToList();

            return search;
        }

        public string FilterQuery(object filers) {
            var query = "";
            foreach (var property in filers.GetType().GetProperties())
            {
                if (string.IsNullOrEmpty($"{property.GetValue(filers)}") || property.GetValue(filers) == null)
                {
                    continue;
                }

                if (property.Name != filers!.GetType().GetProperties().First().Name && !string.IsNullOrEmpty(query))
                {
                    query += "&& ";
                }

                if(property.GetValue(filers, null) is IList) 
                {
                    var subQuery = "";
                    var filterValues =  property.GetValue(filers, null) as List<int>;
                    
                    foreach (var value in filterValues!)
                    {
                        if(!value.Equals(filterValues.FirstOrDefault())) {
                            subQuery += "|| ";
                        }

                        subQuery += $"id == \"{value}\" ";
                    }

                    if (!string.IsNullOrEmpty(subQuery))
                    {
                        query += string.Format("{0}.Any({1})", property.Name, subQuery);
                    }

                } else {
                    query += $"{property.Name} == \"{property.GetValue(filers, null)}\" ";
                }
            }

            return query;
        }
    }
}