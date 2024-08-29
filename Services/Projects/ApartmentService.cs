using System;
using System.Collections.Generic;
using System.Linq;
using OdaMeClone.Data.Repositories;
using OdaMeClone.Dtos.Projects;
using OdaMeClone.Models;

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
                PackageIds = a.PackagesList.Select(p => p.PackageId).ToList(),
                AddOnIds = a.AssignedAddons.Select(ad => ad.AddOnId).ToList(),
                CustomerId = a.CustomerId,
                Status = a.Status,
                ProjectId = a.ProjectId,
                AssignedPackageId = a.AssignedPackageId,
                ApartmentAddOns = a.ApartmentAddOns.Select(ao => new ApartmentAddOnDTO
                    {
                    AddOnId = ao.AddOnId,
                    InstalledUnits = ao.InstalledUnits
                    }).ToList(),
                FloorNumber = a.FloorNumber,
                ViewType = a.ViewType,
                AvailabilityDate = a.AvailabilityDate,
                TotalPrice = a.TotalPrice
                });
            }

        public ApartmentDTO GetApartmentById(Guid apartmentId)
            {
            var apartment = _apartmentRepository.GetById(apartmentId);
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
                PackageIds = apartment.PackagesList.Select(p => p.PackageId).ToList(),
                AddOnIds = apartment.AssignedAddons.Select(ad => ad.AddOnId).ToList(),
                CustomerId = apartment.CustomerId,
                Status = apartment.Status,
                ProjectId = apartment.ProjectId,
                AssignedPackageId = apartment.AssignedPackageId,
                ApartmentAddOns = apartment.ApartmentAddOns.Select(ao => new ApartmentAddOnDTO
                    {
                    AddOnId = ao.AddOnId,
                    InstalledUnits = ao.InstalledUnits
                    }).ToList(),
                FloorNumber = apartment.FloorNumber,
                ViewType = apartment.ViewType,
                AvailabilityDate = apartment.AvailabilityDate,
                TotalPrice = apartment.TotalPrice
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
                CustomerId = apartmentDTO.CustomerId,
                Status = apartmentDTO.Status,
                ProjectId = apartmentDTO.ProjectId,
                AssignedPackageId = apartmentDTO.AssignedPackageId,
                FloorNumber = apartmentDTO.FloorNumber,
                ViewType = apartmentDTO.ViewType,
                AvailabilityDate = apartmentDTO.AvailabilityDate
                };

            _apartmentRepository.Add(apartment);
            }

        public void UpdateApartment(Guid apartmentId, ApartmentDTO apartmentDTO)
            {
            var apartment = _apartmentRepository.GetById(apartmentId);
            if (apartment == null)
                {
                throw new KeyNotFoundException("Apartment not found");
                }

            apartment.ApartmentName = apartmentDTO.ApartmentName;
            apartment.ApartmentType = apartmentDTO.ApartmentType;
            apartment.Space = apartmentDTO.Space;
            apartment.Description = apartmentDTO.Description;
            apartment.ApartmentPhotos = apartmentDTO.ApartmentPhotos;
            apartment.CustomerId = apartmentDTO.CustomerId;
            apartment.Status = apartmentDTO.Status;
            apartment.ProjectId = apartmentDTO.ProjectId;
            apartment.AssignedPackageId = apartmentDTO.AssignedPackageId;
            apartment.FloorNumber = apartmentDTO.FloorNumber;
            apartment.ViewType = apartmentDTO.ViewType;
            apartment.AvailabilityDate = apartmentDTO.AvailabilityDate;

            _apartmentRepository.Update(apartment);
            }

        public void DeleteApartment(Guid apartmentId)
            {
            var apartment = _apartmentRepository.GetById(apartmentId);
            if (apartment == null)
                {
                throw new KeyNotFoundException("Apartment not found");
                }

            _apartmentRepository.Delete(apartment);
            }
        }
    }
