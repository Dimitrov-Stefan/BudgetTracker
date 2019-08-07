using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.Common;

namespace Web.ViewComponents
{
    /// <summary>
    /// A view component that represents a pagination control.
    /// </summary>
    /// <seealso cref="ViewComponent" />
    public class PaginationViewComponent : ViewComponent
    {
        #region Fields

        private const int MaxPages = 5;

        #endregion

        #region Public Methods

        /// <summary>
        /// Invokes the view component.
        /// </summary>
        /// <param name="page">The current page.</param>
        /// <param name="pageCount">The page count.</param>
        /// <param name="pageUrlFunc">The function for generating the page URL.</param>
        public IViewComponentResult Invoke(int page, int pageCount, Func<int, string> pageUrlFunc)
        {
            var model = BuildModel(page, pageCount, pageUrlFunc);
            return View(model);
        }

        #endregion

        #region Private Methods

        private PaginationModel BuildModel(int currentPage, int pageCount, Func<int, string> pageUrlFunc)
        {
            var (startPage, endPage) = GetPageRange(currentPage, pageCount);

            var model = new PaginationModel
            {
                PageUrlFunc = pageUrlFunc,
                CurrentPage = currentPage,
                PageCount = pageCount,
                Pages = Enumerable.Range(startPage, (endPage + 1) - startPage),
                ShowFirstPage = ShouldShowFirstPage(currentPage),
                ShowLastPage = ShouldShowLastPage(currentPage, pageCount)
            };

            return model;
        }

        private (int StartPage, int EndPage) GetPageRange(int currentPage, int pageCount)
        {
            var startPage = 1;
            var endPage = pageCount;

            if (pageCount > MaxPages)
            {
                var maxPagesBeforeCurrent = (int)Math.Floor(MaxPages / 2d);
                var maxPagesAfterCurrent = (int)Math.Ceiling(MaxPages / 2d) - 1;

                if (currentPage <= maxPagesBeforeCurrent)
                {
                    startPage = 1;
                    endPage = MaxPages;
                }
                else if (currentPage + maxPagesAfterCurrent >= pageCount)
                {
                    startPage = pageCount - MaxPages + 1;
                    endPage = pageCount;
                }
                else
                {
                    startPage = currentPage - maxPagesBeforeCurrent;
                    endPage = currentPage + maxPagesAfterCurrent;
                }
            }

            return (startPage, endPage);
        }

        private bool ShouldShowFirstPage(int currentPage)
        {
            var maxPagesBeforeCurrent = (int)Math.Floor(MaxPages / 2d);
            return (currentPage - maxPagesBeforeCurrent) > 1;
        }

        private bool ShouldShowLastPage(int currentPage, int pageCount)
        {
            var maxPagesAfterCurrent = (int)Math.Ceiling(MaxPages / 2d) - 1;
            return (currentPage + maxPagesAfterCurrent) < pageCount;
        }

        #endregion
    }
}
