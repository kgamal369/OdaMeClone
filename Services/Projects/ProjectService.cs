using System;
using System.Collections.Generic;
using System.Linq;
using OdaMeClone.Data.Repositories;
using OdaMeClone.Dtos.Projects;
using OdaMeClone.Models;

namespace OdaMeClone.Services
    {
    public class ProjectService
        {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
            {
            _projectRepository = projectRepository;
            }

        public IEnumerable<ProjectDTO> GetAllProjects()
            {
            var projects = _projectRepository.GetAll();
            return projects.Select(p => new ProjectDTO
                {
                ProjectId = p.ProjectId,
                ProjectName = p.ProjectName,
                Location = p.Location,
                Amenities = p.Amenities,
                TotalUnits = p.TotalUnits,
                ProjectLogo = p.ProjectLogo,
                ApartmentIds = p.Apartments.Select(a => a.ApartmentId).ToList()
                });
            }

        public ProjectDTO GetProjectById(Guid id)
            {
            var project = _projectRepository.GetById(id);
            if (project == null)
                {
                throw new KeyNotFoundException("Project not found");
                }

            return new ProjectDTO
                {
                ProjectId = project.ProjectId,
                ProjectName = project.ProjectName,
                Location = project.Location,
                Amenities = project.Amenities,
                TotalUnits = project.TotalUnits,
                ProjectLogo = project.ProjectLogo,
                ApartmentIds = project.Apartments.Select(a => a.ApartmentId).ToList()
                };
            }

        public void AddProject(ProjectDTO projectDTO)
            {
            var project = new Project
                {
                ProjectId = Guid.NewGuid(),
                ProjectName = projectDTO.ProjectName,
                Location = projectDTO.Location,
                Amenities = projectDTO.Amenities,
                TotalUnits = projectDTO.TotalUnits,
                ProjectLogo = projectDTO.ProjectLogo
                };

            _projectRepository.Add(project);
            }

        public void UpdateProject(Guid id, ProjectDTO projectDTO)
            {
            var project = _projectRepository.GetById(id);
            if (project == null)
                {
                throw new KeyNotFoundException("Project not found");
                }

            project.ProjectName = projectDTO.ProjectName;
            project.Location = projectDTO.Location;
            project.Amenities = projectDTO.Amenities;
            project.TotalUnits = projectDTO.TotalUnits;
            project.ProjectLogo = projectDTO.ProjectLogo;

            _projectRepository.Update(project);
            }

        public void DeleteProject(Guid id)
            {
            var project = _projectRepository.GetById(id);
            if (project == null)
                {
                throw new KeyNotFoundException("Project not found");
                }

            _projectRepository.Delete(project);
            }
        }
    }
