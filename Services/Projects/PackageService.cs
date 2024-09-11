using System;
using System.Collections.Generic;
using OdaMeClone.Data.Repositories;
using OdaMeClone.Dtos.Projects;
using OdaMeClone.Models;

namespace OdaMeClone.Services
    {
    public class PackageService
        {
        private readonly IPackageRepository _packageRepository;

        public PackageService(IPackageRepository packageRepository)
            {
            _packageRepository = packageRepository;
            }

        public IEnumerable<Package> GetAllPackages()
            {
            var packages = _packageRepository.GetAll();
            return packages.Select(p => new Package
                {
                PackageId = p.PackageId,
                PackageName = p.PackageName,
                PackageType = p.PackageType,
                Price = p.Price
                });
            }

        public Package GetPackageById(Guid id)
            {
            var package = _packageRepository.GetById(id);
            if (package == null)
                {
                throw new KeyNotFoundException("Package not found");
                }

            return new Package
                {
                PackageId = package.PackageId,
                PackageName = package.PackageName,
                PackageType = package.PackageType,
                Price = package.Price
                };
            }

        public void AddPackage(Package Package)
            {
            var package = new Package
                {
                PackageId = Guid.NewGuid(),
                PackageName = Package.PackageName,
                PackageType = Package.PackageType,
                Price = Package.Price
                };

            _packageRepository.Add(package);
            }

        public void UpdatePackage(Guid id, Package Package)
            {
            var package = _packageRepository.GetById(id);
            if (package == null)
                {
                throw new KeyNotFoundException("Package not found");
                }

            package.PackageName = Package.PackageName;
            package.PackageType = Package.PackageType;
            package.Price = Package.Price;

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
