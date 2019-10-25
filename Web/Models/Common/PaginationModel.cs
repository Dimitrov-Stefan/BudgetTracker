using System;
using System.Collections.Generic;

namespace Web.Models.Common
{
    public class PaginationModel
    {
        /// <summary>
        /// Gets or sets the function for generating the page URL.
        /// </summary>
        public Func<int, string> PageUrlFunc { get; set; }

        /// <summary>
        /// Gets or sets the current page.
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Gets or sets the total number of pages.
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// Gets a value indicating whether the previous page should be shown.
        /// </summary>
        public bool HasPreviousPage => CurrentPage > 1;

        /// <summary>
        /// Gets a value indicating whether the next page should be shown.
        /// </summary>
        public bool HasNextPage => CurrentPage < PageCount;

        /// <summary>
        /// Gets or sets a value indicating whether to show the first page.
        /// </summary>
        public bool ShowFirstPage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to show the last page.
        /// </summary>
        public bool ShowLastPage { get; set; }

        /// <summary>
        /// Gets or sets the pages.
        /// </summary>
        public IEnumerable<int> Pages { get; set; }
    }
}
