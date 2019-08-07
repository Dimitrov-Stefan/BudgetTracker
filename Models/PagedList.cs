using System;
using System.Collections.Generic;
using System.Linq;

namespace Models
{
    public class PagedList<T>
    {
        #region Constructors

        public PagedList(IEnumerable<T> items, int page, int pageSize, int totalItemCount)
        {
            Items = items ?? Enumerable.Empty<T>();
            Page = page;
            PageSize = pageSize;
            TotalItemCount = totalItemCount;
            PageCount = TotalItemCount > 0
                ? (int)Math.Ceiling(TotalItemCount / (double)PageSize)
                : 0;
        }

        #endregion

        #region Properties

        public IEnumerable<T> Items { get; private set; }

        public int Page { get; private set; }

        public int PageSize { get; private set; }

        public int TotalItemCount { get; private set; }

        public int PageCount { get; private set; }

        #endregion
    }
}
