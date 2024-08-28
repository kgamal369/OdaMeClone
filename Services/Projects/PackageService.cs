using System;
using System.Collections.Generic;
using OdaMeClone.Models;
using OdaMeClone.Dtos.Projects;
using OdaMeClone.Data.Repositories;

namespace OdaMeClone.Services
{
    public class PackageService
    {
        private readonly IPackageRepository _packageRepository;

        public PackageService(IPackageRepository packageRepository)
        {
            _packageRepository = packageRepository;
        }

        public IEnumerable<PackageDTO> GetAllPackages()
        {
            var packages = _packageRepository.GetAll();
            return packages.Select(p => new PackageDTO
            {
                PackageId = p.PackageId,
                PackageName = p.PackageName,
                PackageType = p.PackageType,
                Price = p.Price
            });
        }

        public PackageDTO GetPackageById(Guid id)
        {
            var package = _packageRepository.GetById(id);
            if (package == null)
            {
                throw new KeyNotFoundException("Package not found");
            }

            return new PackageDTO
            {
                PackageId = package.PackageId,
                PackageName = package.PackageName,
                PackageType = package.PackageType,
                Price = package.Price
            };
        }

        public void AddPackage(PackageDTO packageDTO)
        {
            var package = new Package
            {
                PackageId = Guid.NewGuid(),
                PackageName = packageDTO.PackageName,
                PackageType = packageDTO.PackageType,
                Price = packageDTO.Price
            };

            _packageRepository.Add(package);
        }

        public void UpdatePackage(Guid id, PackageDTO packageDTO)
        {
            var package = _packageRepository.GetById(id);
            if (package == null)
            {
                throw new KeyNotFoundException("Package not found");
            }

            package.PackageName = packageDTO.PackageName;
            package.PackageType = packageDTO.PackageType;
            package.Price = packageDTO.Price;

            _packageRepository.Update(package);
        }

        public void DeletePackage(Guid id)
        {
            var package = _packageRepository.GetById(id);
            if (package == null)
            {
                throw new KeyNotFoundException("Package not found");
            }

            _packageRepository.Delete(package);
        }

        public void UpdatePackagePrice(Guid id, decimal newPrice)
        {
            var package = _packageRepository.GetById(id);
            if (package == null)
            {
                throw new KeyNotFoundException("Package not found");
            }

            package.UpdatePackagePrice(_packageRepository.GetContext(), newPrice);
        }
    }
}
