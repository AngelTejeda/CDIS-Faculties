using System;

namespace School_BLL
{
    public class BLLValidationException : Exception
    {
        public BLLValidationException(string message) : base(message)
        {

        }
    }
}
