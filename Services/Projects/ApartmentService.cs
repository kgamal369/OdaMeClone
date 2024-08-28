using System;
using System.Collections.Generic;
using System.Linq;
using OdaMeClone.Data;
using OdaMeClone.Models;
using OdaMeClone.Dtos.Projects;
using OdaMeClone.Data.Repositories;

namespace OdaMeClone.Services
{
    public class ApartmentService
    {
        private readonly IApartmentRepository _apartmentRepository;

        public ApartmentService(IApartmentRepository apartmentRepository)
        {
            _apartmentRepository = apartmentRepository;
        }

        public IEnumerable<ApartmentDTO> GetAllApartments()
        {
            var apartments = _apartmentRepository.GetAll();
            return apartments.Select(a => new ApartmentDTO
            {
                ApartmentId = a.ApartmentId,
                ApartmentName = a.ApartmentName,
                ApartmentType = a.ApartmentType,
                Space = a.Space,
                Description = a.Description,
                ApartmentPhotos = a.ApartmentPhotos,
                FloorNumber = a.FloorNumber,
                ViewType = a.ViewType,
                AvailabilityDate = a.AvailabilityDate,
                TotalPrice = a.TotalPrice,
                ProjectId = a.ProjectId
            });
        }

        public ApartmentDTO GetApartmentById(Guid id)
        {
            var apartment = _apartmentRepository.GetById(id);
            if (apartment == null)
            {
                throw new KeyNotFoundException("Apartment not found");
            }

            return new ApartmentDTO
            {
                ApartmentId = apartment.ApartmentId,
                ApartmentName = apartment.ApartmentName,
                ApartmentType = apartment.ApartmentType,
                Space = apartment.Space,
                Description = apartment.Description,
                ApartmentPhotos = apartment.ApartmentPhotos,
                FloorNumber = apartment.FloorNumber,
                ViewType = apartment.ViewType,
                AvailabilityDate = apartment.AvailabilityDate,
                TotalPrice = apartment.TotalPrice,
                ProjectId = apartment.ProjectId
            };
        }

        public void AddApartment(ApartmentDTO apartmentDTO)
        {
            var apartment = new Apartment
            {
                ApartmentId = Guid.NewGuid(),
                ApartmentName = apartmentDTO.ApartmentName,
                ApartmentType = apartmentDTO.ApartmentType,
                Space = apartmentDTO.Space,
                Description = apartmentDTO.Description,
                ApartmentPhotos = apartmentDTO.ApartmentPhotos,
                FloorNumber = apartmentDTO.FloorNumber,
                ViewType = apartmentDTO.ViewType,
                AvailabilityDate = apartmentDTO.AvailabilityDate,
                ProjectId = apartmentDTO.ProjectId
            };

            _apartmentRepository.Add(apartment);
        }

        public void UpdateApartment(Guid id, ApartmentDTO apartmentDTO)
        {
            var apartment = _apartmentRepository.GetById(id);
            if (apartment == null)
            {
                throw new KeyNotFoundException("Apartment not found");
            }

            apartment.ApartmentName = apartmentDTO.ApartmentName;
            apartment.ApartmentType = apartmentDTO.ApartmentType;
            apartment.Space = apartmentDTO.Space;
            apartment.Description = apartmentDTO.Description;
            apartment.ApartmentPhotos = apartmentDTO.ApartmentPhotos;
            apartment.FloorNumber = apartmentDTO.FloorNumber;
            apartment.ViewType = apartmentDTO.ViewType;
            apartment.AvailabilityDate = apartmentDTO.AvailabilityDate;
            apartment.ProjectId = apartmentDTO.ProjectId;

            _apartmentRepository.Update(apartment);
        }

        public void DeleteApartment(Guid id)
        {
            var apartment = _apartmentRepository.GetById(id);
            if (apartment == null)
            {
                throw new KeyNotFoundException("Apartment not found");
            }

            _apartmentRepository.Delete(apartment);
        }
    }
}
