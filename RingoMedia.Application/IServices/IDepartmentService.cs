using RingoMedia.Application.Contract;
using RingoMedia.Dtos;
using RingoMedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingoMedia.Application.IServices
{
    public interface IDepartmentService
    {
        Task AddDepartmentAsync(DepartmentDTO departmentDto);
        Task<IEnumerable<Department>> GetDepartmentsAsync();
        Task<DepartmentDTO> GetDepartmentByIdAsync(int id);
        Task<DepartmentDTO> GetDepartmentWithSubDepartmentsAsync(int departmentId);
        Task<List<DepartmentDTO>> GetParentDepartmentsAsync(int departmentId);
        Task UpdateDepartmentAsync(DepartmentDTO departmentDto);
        Task DeleteDepartmentAsync(int id);
        Task<IEnumerable<DepartmentDTO>> GetAllDepartmentsAsync();

    }
}
