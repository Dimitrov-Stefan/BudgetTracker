using System.Collections.Generic;
using Models.Entities.Identity;

namespace Web.Areas.Admin.Models.Users
{
    public class UserListViewModel
    {
        public IEnumerable<User> Users { get; set; }
    }
}
