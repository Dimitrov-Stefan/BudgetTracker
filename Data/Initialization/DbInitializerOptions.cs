using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Initialization
{
    public class DbInitializerOptions
    {
        public IEnumerable<InitialUser> Users { get; set; }
    }
}
