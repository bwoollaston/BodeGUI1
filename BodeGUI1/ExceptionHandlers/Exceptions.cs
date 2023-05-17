﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodeGUI1.ExceptionHandlers
{
    internal class ControlOutOfRangeException : Exception
    {
        public ControlOutOfRangeException() : base()
        {

        }
        public ControlOutOfRangeException(string message) : base(message)
        {

        }
        public ControlOutOfRangeException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
