using Microsoft.AspNetCore.Mvc;
using Web.Models.Common;

namespace Web.ViewComponents
{
    /// <summary>
    /// A view component that represents the delete confirmation dialog.
    /// </summary>
    /// <seealso cref="ViewComponent" />
    public class DeleteConfirmationDialogViewComponent : ViewComponent
    {
        /// <summary>
        /// Invokes the view component.
        /// </summary>
        /// <param name="dialogId">The dialog identifier.</param>
        /// <param name="title">The title.</param>
        /// <param name="text">The text.</param>
        /// <param name="url">The URL.</param>
        public IViewComponentResult Invoke(string dialogId, string title, string text, string url)
        {
            return View(new DeleteConfirmationDialogViewModel()
            {
                Id = dialogId,
                Title = title,
                Text = text,
                Url = url
            });
        }
    }
}
