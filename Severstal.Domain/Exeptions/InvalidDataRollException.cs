using System;
using System.Collections.Generic;
using System.Text;

namespace Severstal.Domain.Exeptions
{
    public class InvalidDataRollException : Exception
    {
        public InvalidDataRollException(string message) : base(message) { }
    }
}
