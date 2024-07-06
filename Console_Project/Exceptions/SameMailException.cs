using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Project.Exceptions
{
    public class SameMailException:Exception
    {
        public SameMailException(string message): base(message) { }
    }
}
