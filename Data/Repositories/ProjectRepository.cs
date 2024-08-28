using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OdaMeClone.Data;
using OdaMeClone.Models;

namespace OdaMeClone.Repositories
{
    public interface IProjectRepository
    {
        IEnumerable<Project> GetAll();
        Project GetById(Guid id);
        void Add(Project project);
        void Update(Project project);
        void Delete(Project project);
    }

    public class ProjectRepository : IProjectRepository
    {
        private readonly OdaDbContext _context;

        public ProjectRepository(OdaDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Project> GetAll()
        {
            return _context.Projects
                .Include(p => p.Apartments)
                .ToList();
        }

        public Project GetById(Guid id)
        {
            return _context.Projects
                .Include(p => p.Apartments)
                .FirstOrDefault(p => p.ProjectId == id);
        }

        public void Add(Project project)
        {
            _context.Projects.Add(project);
            _context.SaveChanges();
        }

        public void Update(Project project)
        {
            _context.Projects.Update(project);
            _context.SaveChanges();
        }

        public void Delete(Project project)
        {
            _context.Projects.Remove(project);
            _context.SaveChanges();
        }
    }
}
