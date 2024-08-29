using System;
using System.Collections.Generic;
using System.Linq;
using OdaMeClone.Data.Repositories;
using OdaMeClone.Dtos.Projects;
using OdaMeClone.Models;

namespace OdaMeClone.Services
    {
    public class ApartmentAddOnService
        {
        private readonly IApartmentAddOnRepository _apartmentAddOnRepository;

        public ApartmentAddOnService(IApartmentAddOnRepository apartmentAddOnRepository)
            {
            _apartmentAddOnRepository = apartmentAddOnRepository;
            }

        public IEnumerable<ApartmentAddOnDTO> GetAllApartmentAddOns()
            {
            var apartmentAddOns = _apartmentAddOnRepository.GetAll();
            return apartmentAddOns.Select(aa => new ApartmentAddOnDTO
                {
                ApartmentId = aa.ApartmentId,
                AddOnId = aa.AddOnId,
                InstalledUnits = aa.InstalledUnits
                });
            }

        public ApartmentAddOnDTO GetApartmentAddOnById(Guid apartmentId, Guid addOnId)
            {
            var apartmentAddOn = _apartmentAddOnRepository.GetByIds(apartmentId, addOnId);
            if (apartmentAddOn == null)
                {
                throw new KeyNotFoundException("ApartmentAddOn not found");
                }

            return new ApartmentAddOnDTO
                {
                ApartmentId = apartmentAddOn.ApartmentId,
                AddOnId = apartmentAddOn.AddOnId,
                InstalledUnits = apartmentAddOn.InstalledUnits
                };
            }

        public void AddApartmentAddOn(ApartmentAddOnDTO apartmentAddOnDTO)
            {
            var apartmentAddOn = new ApartmentAddOn
                {
                ApartmentId = apartmentAddOnDTO.ApartmentId,
                AddOnId = apartmentAddOnDTO.AddOnId,
                InstalledUnits = apartmentAddOnDTO.InstalledUnits
                };

            _apartmentAddOnRepository.Add(apartmentAddOn);
            }

        public void UpdateApartmentAddOn(Guid apartmentId, Guid addOnId, ApartmentAddOnDTO apartmentAddOnDTO)
            {
            var apartmentAddOn = _apartmentAddOnRepository.GetByIds(apartmentId, addOnId);
            if (apartmentAddOn == null)
                {
                throw new KeyNotFoundException("ApartmentAddOn not found");
                }

            apartmentAddOn.InstalledUnits = apartmentAddOnDTO.InstalledUnits;

            _apartmentAddOnRepository.Update(apartmentAddOn);
            }

        public void DeleteApartmentAddOn(Guid apartmentId, Guid addOnId)
            {
            var apartmentAddOn = _apartmentAddOnRepository.GetByIds(apartmentId, addOnId);
            if (apartmentAddOn == null)
                {
                throw new KeyNotFoundException("ApartmentAddOn not found");
                }

            _apartmentAddOnRepository.Delete(apartmentAddOn);
            }
        }
    }
