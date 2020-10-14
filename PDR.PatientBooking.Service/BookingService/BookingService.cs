using PDR.PatientBooking.Data;
using PDR.PatientBooking.Data.Models;
using PDR.PatientBooking.Service.BookingService.Requests;
using PDR.PatientBooking.Service.BookingService.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDR.PatientBooking.Service.BookingService
{
    public class BookingService : IBookingService
    {
        private readonly PatientBookingContext _context;
        private readonly IAddBookingRequestValidator _addbookingvalidator;
        private readonly ICancelBookingRequestValidator _cancelbookingvalidator;

        public BookingService(PatientBookingContext context, IAddBookingRequestValidator addvalidator, ICancelBookingRequestValidator cancelvalidator)
        {
            _context = context;
            _addbookingvalidator = addvalidator;
            _cancelbookingvalidator = cancelvalidator;
        }

        public void AddBooking(AddBookingRequest request)
        {
            var validationResult = _addbookingvalidator.ValidateRequest(request);

            if (!validationResult.PassedValidation)
            {
                throw new ArgumentException(validationResult.Errors.First());
            }

            var bookingId = new Guid();
            var bookingStartTime = request.StartTime;
            var bookingEndTime = request.EndTime;
            var bookingPatientId = request.PatientId;
            var bookingPatient = _context.Patient.FirstOrDefault(x => x.Id == request.PatientId);
            var bookingDoctorId = request.DoctorId;
            var bookingDoctor = _context.Doctor.FirstOrDefault(x => x.Id == request.DoctorId);
            var bookingSurgeryType = _context.Patient.FirstOrDefault(x => x.Id == bookingPatientId).Clinic.SurgeryType;

            _context.Order.Add(new Order
            {
                Id = bookingId,
                StartTime = bookingStartTime,
                EndTime = bookingEndTime,
                PatientId = bookingPatientId,
                DoctorId = bookingDoctorId,
                Patient = bookingPatient,
                Doctor = bookingDoctor,
                SurgeryType = (int)bookingSurgeryType,
                Status = OrderStatus.Unserved
            });

            _context.SaveChanges();
        }


        public void CancelBooking(CancelBookingRequest request)
        {
            var validationResult = _cancelbookingvalidator.ValidateRequest(request);

            if (!validationResult.PassedValidation)
            {
                throw new ArgumentException(validationResult.Errors.First());
            }

            var booking = _context.Order.Find(request.BookingId);
            booking.Status = OrderStatus.Canceled;
            _context.SaveChanges();

        }
    }
}
