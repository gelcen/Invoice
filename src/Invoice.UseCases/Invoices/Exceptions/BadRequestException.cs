using System;

namespace Invoice.UseCases.Invoices.Exceptions
{
    public abstract class BadRequestException : Exception
    {
        protected BadRequestException(string message)
            : base(message)
        {
        }
    }
}
