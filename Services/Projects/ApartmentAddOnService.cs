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

        public IEnumerable<ApartmentAddOn> GetAllApartmentAddOns()
            {
            var apartmentAddOns = _apartmentAddOnRepository.GetAll();
            return apartmentAddOns.Select(aa => new ApartmentAddOn
                {
                ApartmentId = aa.ApartmentId,
                AddOnId = aa.AddOnId,
                InstalledUnits = aa.InstalledUnits
                });
            }

        public ApartmentAddOn GetApartmentAddOnById(Guid apartmentId, Guid addOnId)
            {
            var apartmentAddOn = _apartmentAddOnRepository.GetByIds(apartmentId, addOnId);
            if (apartmentAddOn == null)
                {
                throw new KeyNotFoundException("ApartmentAddOn not found");
                }

            return new ApartmentAddOn
                {
                ApartmentId = apartmentAddOn.ApartmentId,
                AddOnId = apartmentAddOn.AddOnId,
                InstalledUnits = apartmentAddOn.InstalledUnits
                };
            }

        public void AddApartmentAddOn(ApartmentAddOn ApartmentAddOn)
            {
            var apartmentAddOn = new ApartmentAddOn
                {
                ApartmentId = ApartmentAddOn.ApartmentId,
                AddOnId = ApartmentAddOn.AddOnId,
                InstalledUnits = ApartmentAddOn.InstalledUnits
                };

            _apartmentAddOnRepository.Add(apartmentAddOn);
            }

        public void UpdateApartmentAddOn(Guid apartmentId, Guid addOnId, ApartmentAddOn ApartmentAddOn)
            {
            var apartmentAddOn = _apartmentAddOnRepository.GetByIds(apartmentId, addOnId);
            if (apartmentAddOn == null)
                {
                throw new KeyNotFoundException("ApartmentAddOn not found");
                }

            apartmentAddOn.InstalledUnits = ApartmentAddOn.InstalledUnits;

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
