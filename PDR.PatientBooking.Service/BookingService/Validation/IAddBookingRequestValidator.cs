using PDR.PatientBooking.Service.BookingService.Requests;
using PDR.PatientBooking.Service.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace PDR.PatientBooking.Service.BookingService.Validation
{
   public interface IAddBookingRequestValidator
    {
        PdrValidationResult ValidateRequest(AddBookingRequest request);
    }
}
