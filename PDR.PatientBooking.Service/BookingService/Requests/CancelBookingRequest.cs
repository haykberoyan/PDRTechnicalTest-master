using System;
using System.Collections.Generic;
using System.Text;

namespace PDR.PatientBooking.Service.BookingService.Requests
{
    public class CancelBookingRequest
    {
        public Guid BookingId { get; set; }
    }
}
