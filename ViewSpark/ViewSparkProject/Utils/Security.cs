using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewSparkProject.Utils
{
    public class Security
    {
        [Flags]
        public enum SharePermissions
        {
            None = 0,
            Read = 1,
            Write = 2
        }
    }
}