namespace Models
{
    /// <summary>
    /// A class that represents a paged list request.
    /// </summary>
    public class PagedListRequest
    {
        #region Fields

        private const int DefaultPageSize = 10;

        private int _page = 1;
        private int _pageSize = DefaultPageSize;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the page.
        /// </summary>
        public int Page
        {
            get => _page;
            set => _page = value > 0 ? value : 1;
        }

        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > 0 ? value : DefaultPageSize;
        }

        /// <summary>
        /// Gets the skip.
        /// </summary>
        public int Skip => (Page - 1) * PageSize;

        #endregion
    }
}
