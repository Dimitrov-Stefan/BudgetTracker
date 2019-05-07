using Models.Entities.Identity;
using System.Collections.Generic;

namespace Core.Contracts.Services
{
    public interface IRoleService
    {
        IEnumerable<Role> GetAll();
    }
}
