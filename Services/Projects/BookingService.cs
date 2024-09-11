using System;
using System.Collections.Generic;
using System.Linq;
using OdaMeClone.Data.Repositories;
using OdaMeClone.Dtos.Projects;
using OdaMeClone.Models;

namespace OdaMeClone.Services
    {
    public class BookingService
        {
        private readonly IBookingRepository _bookingRepository;
        private readonly IApartmentRepository _apartmentRepository;
        private readonly IPackageRepository _packageRepository;
        private readonly IAddOnRepository _addOnRepository;
        private readonly IApartmentAddOnRepository _apartmentAddOnRepository;


        public BookingService(
            IBookingRepository bookingRepository,
            IApartmentRepository apartmentRepository,
            IPackageRepository packageRepository,
            IAddOnRepository addOnRepository,
            IApartmentAddOnRepository apartmentAddOnRepository
            )
            {
            _bookingRepository = bookingRepository;
            _apartmentRepository = apartmentRepository;
            _packageRepository = packageRepository;
            _addOnRepository = addOnRepository;
            _apartmentAddOnRepository = _apartmentAddOnRepository;
            }

        public IEnumerable<Booking> GetAllBookings()
            {
            var bookings = _bookingRepository.GetAll();
            return bookings.Select(b => new Booking
                {
                BookingId = b.BookingId,
                CustomerId = b.CustomerId,
                ApartmentId = b.ApartmentId,
                CreatedDateTime = b.CreatedDateTime,
                BookingStatus = b.BookingStatus,
                LastModifiedDateTime = b.LastModifiedDateTime,
                AssignedPerson = b.AssignedPerson,
                PaymentMethod = b.PaymentMethod,
                Invoices = (ICollection<Invoice>)b.Invoices.Select(i => new InvoiceDTO
                    {
                    InvoiceId = i.InvoiceId,
                    Amount = i.Amount,
                    PaymentMethod = i.PaymentMethod,
                    InvoiceStatus = i.InvoiceStatus
                    }).ToList()
                });
            }

        public Booking GetBookingById(Guid id)
            {
            var booking = _bookingRepository.GetById(id);
            if (booking == null)
                {
                throw new KeyNotFoundException("Booking not found");
                }

            return new Booking
                {
                BookingId = booking.BookingId,
                CustomerId = booking.CustomerId,
                ApartmentId = booking.ApartmentId,
                CreatedDateTime = booking.CreatedDateTime,
                BookingStatus = booking.BookingStatus,
                LastModifiedDateTime = booking.LastModifiedDateTime,
                AssignedPerson = booking.AssignedPerson,
                PaymentMethod = booking.PaymentMethod,
                Invoices = booking.Invoices.Select(i => new Invoice
                    {
                    InvoiceId = i.InvoiceId,
                    Amount = i.Amount,
                    PaymentMethod = i.PaymentMethod,
                    InvoiceStatus = i.InvoiceStatus
                    }).ToList()
                };
            }

        public void AddBooking(Booking bookingDTO)
            {
            var originalApartment = _apartmentRepository.GetById(bookingDTO.ApartmentId);
            if (originalApartment == null)
                {
                throw new KeyNotFoundException("Apartment not found");
                }

            // Create a new copy of the apartment with status "InProgress"
            var newApartment = new Apartment
                {
                ApartmentId = Guid.NewGuid(),
                ApartmentName = originalApartment.ApartmentName,
                ApartmentType = originalApartment.ApartmentType,
                Space = originalApartment.Space,
                Description = originalApartment.Description,
                ProjectId = originalApartment.ProjectId,
                ApartmentStatus = ApartmentStatus.InProgress, // New status
                AssignedApartmentAddOns = new List<ApartmentAddOn>(),
                AssignedPackage = null // Will be set later
                };

            // Assign selected package (required)
            var selectedPackage = _packageRepository.GetById(bookingDTO.Apartment.AssignedPackage.PackageId);
            if (selectedPackage == null)
                {
                throw new InvalidOperationException("Selected package is not available for this apartment.");
                }
            newApartment.AssignedPackage = selectedPackage;

            // Assign selected add-ons (optional)
            foreach (var addOn in bookingDTO.Apartment.AssignedApartmentAddOns)
                {
                var selectedAddOn = _addOnRepository.GetById(addOn.AddOnId);
                if (selectedAddOn == null)
                    {
                    throw new InvalidOperationException($"Selected add-on {addOn.AddOnId} is not available.");
                    }

                newApartment.AssignedApartmentAddOns.Add(new ApartmentAddOn
                    {
                    AddOnId = selectedAddOn.AddOnId,
                    InstalledUnits = addOn.InstalledUnits,
                    AddOn = selectedAddOn
                    });
                }

            // Save the new apartment
            _apartmentRepository.Add(newApartment);

            // Create the booking with the new apartment
            var newBooking = new Booking
                {
                BookingId = Guid.NewGuid(),
                CustomerId = bookingDTO.CustomerId,
                ApartmentId = newApartment.ApartmentId, // Link to the new apartment
                CreatedDateTime = DateTime.Now,
                BookingStatus = BookingStatus.Pending,
                AssignedPerson = bookingDTO.AssignedPerson,
                PaymentMethod = bookingDTO.PaymentMethod
                };

            _bookingRepository.Add(newBooking);
            }

        public void UpdateBooking(Guid id, Booking Booking)
            {
            var booking = _bookingRepository.GetById(id);
            if (booking == null)
                {
                throw new KeyNotFoundException("Booking not found");
                }

            booking.BookingStatus = Booking.BookingStatus;
            booking.LastModifiedDateTime = DateTime.Now;
            booking.AssignedPerson = Booking.AssignedPerson;

            _bookingRepository.Update(booking);
            }

        public bool IsValidApartment(Guid apartmentId)
            {
            var apartment = _apartmentRepository.GetById(apartmentId);
            return apartment != null;
            }

        public bool IsValidPackage(Guid apartmentId, Guid packageId)
            {
            var apartment = _apartmentRepository.GetById(apartmentId);
            return apartment.AvailablePackages.Any(p => p.PackageId == packageId);
            }
        public bool AreValidAddOns(Guid apartmentId, List<Guid> addOnIds)
            {
            var apartment = _apartmentRepository.GetById(apartmentId);
            return addOnIds.All(addOnId => apartment.AvailableApartmentAddOns.Any(ao => ao.AddOnId == addOnId));
            }
        public void DeleteBooking(Guid id)
            {
            var booking = _bookingRepository.GetById(id);
            if (booking == null)
                {
                throw new KeyNotFoundException("Booking not found");
                }

            _bookingRepository.Delete(booking);
            }

        public void FinalizeBooking(Guid id)
            {
            var booking = _bookingRepository.GetById(id);
            if (booking == null)
                {
                throw new KeyNotFoundException("Booking not found");
                }

            booking.FinalizeBooking(_bookingRepository.GetContext());
            }
        }
    }
