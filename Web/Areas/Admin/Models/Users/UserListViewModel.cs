using Models;
using Models.Entities.Identity;
using System.Collections.Generic;

namespace Web.Areas.Admin.Models.Users
{
    public class UserListViewModel
    {
        public string SearchText { get; set; }

        public PagedList<User> Users { get; set; }
    }
}
