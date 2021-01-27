using System;
using System.Collections.Generic;

namespace Pokeshop.Api.Common
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public PagedList(int pageNumber, int pageSize, int count, IEnumerable<T> items)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }

        public static PagedList<T> ToPagedList(int pageNumber, int pageSize, int count, IEnumerable<T> items)
        {
            return new PagedList<T>(pageNumber, pageSize, count, items);
        }
    }
}
