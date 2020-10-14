using PDR.PatientBooking.Data;
using PDR.PatientBooking.Data.Models;
using PDR.PatientBooking.Service.BookingService.Requests;
using PDR.PatientBooking.Service.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDR.PatientBooking.Service.BookingService.Validation
{
    public class CancelBookingRequestValidator : ICancelBookingRequestValidator
    {
        private readonly PatientBookingContext _context;

        public CancelBookingRequestValidator(PatientBookingContext context)
        {
            _context = context;
        }
        public PdrValidationResult ValidateRequest(CancelBookingRequest request)
        {
            var result = new PdrValidationResult(true);

            if (IncorrectBooking(request, ref result))
                return result;

            return result;
        }

        private bool IncorrectBooking(CancelBookingRequest request, ref PdrValidationResult result)
        {
            if (!_context.Order.Any(x => x.Id == request.BookingId && x.Status == OrderStatus.Unserved))
               
            {
                result.PassedValidation = false;
                result.Errors.Add("Selected booking can not be canceled.");
                return true;
            }

            return false;
        }
    }
}
