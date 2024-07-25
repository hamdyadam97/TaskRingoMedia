using RingoMedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingoMedia.Application.Contract
{
    public interface IDepartmentRepository
    {
        Task AddDepartmentAsync(Department department);
        Task<IEnumerable<Department>> GetDepartmentsAsync();
        Task<Department> GetDepartmentByIdAsync(int id);
        Task<Department> GetDepartmentWithSubDepartmentsAsync(int departmentId);
        Task<List<Department>> GetParentDepartmentsAsync(int departmentId);
        Task UpdateDepartmentAsync(Department department);
        Task DeleteDepartmentAsync(int id);
        Task<IEnumerable<Department>> GetAllDepartmentsAsync();

    }
}
