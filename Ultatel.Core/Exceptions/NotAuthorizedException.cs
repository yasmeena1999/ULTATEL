﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ultatel.Core.Exceptions
{
    public class NotAuthorizedException:Exception
    {
        public NotAuthorizedException(string msg) : base(msg) { }
    }

}
