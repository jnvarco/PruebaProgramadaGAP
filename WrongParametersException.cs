using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Services.Exceptions
{
    public class WrongParametersException:Exception
    {
        public WrongParametersException()
            : base("Wrong parameters (id is not a number")
        {
        }

        public WrongParametersException(string message)
            :base(message)
        {
        }       

    }
}