using System;
using System.Collections.Generic;
using System.Linq;
using OdaMeClone.Data.Repositories;
using OdaMeClone.Dtos.Projects;
using OdaMeClone.Models;

namespace OdaMeClone.Services
    {
    public class AddOnService
        {
        private readonly IAddOnRepository _addOnRepository;


        public AddOnService(IAddOnRepository addOnRepository)
            {
            _addOnRepository = addOnRepository;
            }

        public IEnumerable<AddOn> GetAllAddOns()
            {
            var addOns = _addOnRepository.GetAll();
            return addOns.Select(a => new AddOn
                {
                AddOnId = a.AddOnId,
                AddOnName = a.AddOnName,
                AddOnType = a.AddOnType,
                PricePerUnit = a.PricePerUnit,
                MaxUnits = a.MaxUnits,
                InstalledUnits = a.InstalledUnits
                });
            }

        public AddOn GetAddOnById(Guid id)
            {
            var addOn = _addOnRepository.GetById(id);
            if (addOn == null)
                {
                throw new KeyNotFoundException("AddOn not found");
                }

            return new AddOn
                {
                AddOnId = addOn.AddOnId,
                AddOnName = addOn.AddOnName,
                AddOnType = addOn.AddOnType,
                PricePerUnit = addOn.PricePerUnit,
                MaxUnits = addOn.MaxUnits,
                InstalledUnits = addOn.InstalledUnits
                };
            }

        public void AddAddOn(AddOn AddOn)
            {
            var addOn = new AddOn
                {
                AddOnId = Guid.NewGuid(),
                AddOnName = AddOn.AddOnName,
                AddOnType = AddOn.AddOnType,
                PricePerUnit = AddOn.PricePerUnit,
                MaxUnits = AddOn.MaxUnits,
                InstalledUnits = AddOn.InstalledUnits
                };

            _addOnRepository.Add(addOn);
            }

        public void UpdateAddOn(Guid id, AddOn AddOn)
            {
            var addOn = _addOnRepository.GetById(id);
            if (addOn == null)
                {
                throw new KeyNotFoundException("AddOn not found");
                }

            addOn.AddOnName = AddOn.AddOnName;
            addOn.AddOnType = AddOn.AddOnType;
            addOn.PricePerUnit = AddOn.PricePerUnit;
            addOn.MaxUnits = AddOn.MaxUnits;
            addOn.InstalledUnits = AddOn.InstalledUnits;

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
