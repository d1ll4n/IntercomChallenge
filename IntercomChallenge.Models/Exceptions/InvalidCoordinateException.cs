using System;
using System.Collections.Generic;
using System.Text;

namespace IntercomChallenge.Models.Exceptions
{

    public class InvalidCoordinateException : Exception
    {
        public InvalidCoordinateException(string message) : base(message)
        {

        }
    }
}
