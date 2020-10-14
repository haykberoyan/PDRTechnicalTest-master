using PDR.PatientBooking.Service.BookingService.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace PDR.PatientBooking.Service.BookingService
{
    public interface IBookingService
    {
        void AddBooking(AddBookingRequest request);
        void CancelBooking(CancelBookingRequest request);
    }
}
