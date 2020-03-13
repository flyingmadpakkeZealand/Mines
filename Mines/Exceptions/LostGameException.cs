using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mines.Exceptions
{
    public class LostGameException : Exception
    {
        public LostGameException(string message):base(message)
        {
            
        }
    }
}
