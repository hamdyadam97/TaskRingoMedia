using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using RingoMedia.Application.Contract;
using RingoMedia.Context;
using RingoMedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingoMedia.Infrastructure
{
    public class DepartmentRepository: IDepartmentRepository
    {
        private readonly RingoMediaDbContext _context;

        public DepartmentRepository(RingoMediaDbContext context)
        {
            _context = context;
        }

        public async Task AddDepartmentAsync(Department department)
        {
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Department>> GetDepartmentsAsync()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            return await _context.Departments.FindAsync(id);
        }


        public async Task<Department> GetDepartmentWithSubDepartmentsAsync(int departmentId)
        {
            return await _context.Departments
                .Include(d => d.SubDepartments)
                .FirstOrDefaultAsync(d => d.Id == departmentId);
        }
        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<List<Department>> GetParentDepartmentsAsync(int departmentId)
        {
            var parents = new List<Department>();
            var department = await _context.Departments.FindAsync(departmentId);

            while (department != null && department.ParentDepartmentId.HasValue)
            {
                department = await _context.Departments.FindAsync(department.ParentDepartmentId.Value);
                if (department != null)
                {
                    parents.Insert(0, department); // Insert at the beginning to maintain hierarchy
                }
            }

            return parents;
        }

        public async Task UpdateDepartmentAsync(Department department)
        {
            _context.Departments.Update(department);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDepartmentAsync(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department != null)
            {
                _context.Departments.Remove(department);
                await _context.SaveChangesAsync();
            }
        }
    }
}

