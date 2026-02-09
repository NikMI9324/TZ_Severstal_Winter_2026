using System;
using System.Collections.Generic;
using System.Text;

namespace Severstal.Domain.Exeptions
{
    public class InvalidRangeValuesException : Exception
    {
        public InvalidRangeValuesException(string message) : base(message) { }
    }
}
