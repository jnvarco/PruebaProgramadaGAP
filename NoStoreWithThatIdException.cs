using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Services.Exceptions
{
    public class NoStoreWithThatIdException:Exception
    {
        public NoStoreWithThatIdException()
            :base("No store with that ID")
        {
        }

        public NoStoreWithThatIdException(string message)
            :base(message)
        {
        }
    }
}