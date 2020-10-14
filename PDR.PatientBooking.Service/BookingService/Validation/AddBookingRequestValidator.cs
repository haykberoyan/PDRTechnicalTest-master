using PDR.PatientBooking.Data;
using PDR.PatientBooking.Service.BookingService.Requests;
using PDR.PatientBooking.Service.PatientServices.Requests;
using PDR.PatientBooking.Service.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDR.PatientBooking.Service.BookingService.Validation
{
    public class AddBookingRequestValidator : IAddBookingRequestValidator
    {

        private readonly PatientBookingContext _context;

        public AddBookingRequestValidator(PatientBookingContext context)
        {
            _context = context;
        }
        public PdrValidationResult ValidateRequest(AddBookingRequest request)
        {
            var result = new PdrValidationResult(true);

            if (PastDate(request, ref result))
                return result;

            if (BusyDate(request, ref result))
                return result;

            return result;
        }

        private bool PastDate(AddBookingRequest request, ref PdrValidationResult result)
        {
            if (request.StartTime <= DateTime.Now)
            {
                result.PassedValidation = false;
                result.Errors.Add("Selected booking date can not be in the past ");
                return true;
            }

            return false;
        }

        private bool BusyDate(AddBookingRequest request, ref PdrValidationResult result)
        {
            if (_context.Order.Any(x => x.DoctorId == request.DoctorId
                && x.StartTime <= request.EndTime && x.EndTime >= request.StartTime))
            {
                result.PassedValidation = false;
                result.Errors.Add("Selected date is already booked.");
                return true;
            }

            return false;
        }
    }
}
