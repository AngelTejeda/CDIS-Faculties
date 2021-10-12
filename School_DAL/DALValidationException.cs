using System;

namespace School_DAL
{
    public class DALValidationException : Exception
    {
        public DALValidationException(string message) : base(message)
        {
        }
    }
}