using System.Collections.Generic;
using Models.Entities.Identity;

namespace Web.Areas.Admin.Models.Users
{
    public class UserListViewModel
    {
        public string SearchText { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
}
