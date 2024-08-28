using System;
using System.Collections.Generic;
using System.Linq;
using OdaMeClone.Models;
using OdaMeClone.Dtos.Projects;
using OdaMeClone.Data.Repositories;

namespace OdaMeClone.Services
{
    public class AddOnService
    {
        private readonly IAddOnRepository _addOnRepository;

        public AddOnService(IAddOnRepository addOnRepository)
        {
            _addOnRepository = addOnRepository;
        }

        public IEnumerable<AddOnDTO> GetAllAddOns()
        {
            var addOns = _addOnRepository.GetAll();
            return addOns.Select(a => new AddOnDTO
            {
                AddOnId = a.AddOnId,
                AddOnName = a.AddOnName,
                AddOnType = a.AddOnType,
                PricePerUnit = a.PricePerUnit,
                MaxUnits = a.MaxUnits,
                InstalledUnits = a.InstalledUnits
            });
        }

        public AddOnDTO GetAddOnById(Guid id)
        {
            var addOn = _addOnRepository.GetById(id);
            if (addOn == null)
            {
                throw new KeyNotFoundException("AddOn not found");
            }

            return new AddOnDTO
            {
                AddOnId = addOn.AddOnId,
                AddOnName = addOn.AddOnName,
                AddOnType = addOn.AddOnType,
                PricePerUnit = addOn.PricePerUnit,
                MaxUnits = addOn.MaxUnits,
                InstalledUnits = addOn.InstalledUnits
            };
        }

        public void AddAddOn(AddOnDTO addOnDTO)
        {
            var addOn = new AddOn
            {
                AddOnId = Guid.NewGuid(),
                AddOnName = addOnDTO.AddOnName,
                AddOnType = addOnDTO.AddOnType,
                PricePerUnit = addOnDTO.PricePerUnit,
                MaxUnits = addOnDTO.MaxUnits,
                InstalledUnits = addOnDTO.InstalledUnits
            };

            _addOnRepository.Add(addOn);
        }

        public void UpdateAddOn(Guid id, AddOnDTO addOnDTO)
        {
            var addOn = _addOnRepository.GetById(id);
            if (addOn == null)
            {
                throw new KeyNotFoundException("AddOn not found");
            }

            addOn.AddOnName = addOnDTO.AddOnName;
            addOn.AddOnType = addOnDTO.AddOnType;
            addOn.PricePerUnit = addOnDTO.PricePerUnit;
            addOn.MaxUnits = addOnDTO.MaxUnits;
            addOn.InstalledUnits = addOnDTO.InstalledUnits;

            _addOnRepository.Update(addOn);
        }

        public void DeleteAddOn(Guid id)
        {
            var addOn = _addOnRepository.GetById(id);
            if (addOn == null)
            {
                throw new KeyNotFoundException("AddOn not found");
            }

            _addOnRepository.Delete(addOn);
        }
    }
}
