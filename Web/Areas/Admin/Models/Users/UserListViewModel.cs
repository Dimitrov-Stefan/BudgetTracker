using Models;
using Models.Entities.Identity;

namespace Web.Areas.Admin.Models.Users
{
    public class UserListViewModel
    {
        public string SearchText { get; set; }

        public PagedList<User> Users { get; set; }
    }
}
