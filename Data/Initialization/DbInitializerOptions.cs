using System.Collections.Generic;

namespace Data.Initialization
{
    public class DbInitializerOptions
    {
        public IEnumerable<InitialUser> Users { get; set; }
    }
}
