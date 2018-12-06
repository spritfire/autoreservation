using System;

namespace AutoReservation.BusinessLayer.Exceptions
{
    public class InvalidDateRangeException
        : Exception
    {
        public InvalidDateRangeException(string message) : base(message)
        {
        }

        public InvalidDateRangeException(string message, DateTime von, DateTime bis) : base(message)
        {
            Von = von;
            Bis = bis;
        }

        public DateTime Von { get; set; }
        public DateTime Bis { get; set; }
    }
}