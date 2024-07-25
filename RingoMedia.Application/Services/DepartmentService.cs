using AutoMapper;
using RingoMedia.Application.Contract;
using RingoMedia.Application.IServices;
using RingoMedia.Dtos;
using RingoMedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingoMedia.Application.Services
{
    public class DepartmentService:IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task AddDepartmentAsync(DepartmentDTO departmentDto)
        {
            var department = _mapper.Map<Department>(departmentDto);

            if (department.SubDepartments == null)
            {
                department.SubDepartments = new List<Department>();
            }

            if (departmentDto.SubDepartmentIds != null && departmentDto.SubDepartmentIds.Count > 0)
            {
                foreach (var subDeptId in departmentDto.SubDepartmentIds)
                {
                    var subDepartment = await _departmentRepository.GetDepartmentByIdAsync(subDeptId);
                    if (subDepartment != null)
                    {
                        department.SubDepartments.Add(subDepartment);
                    }
                }
            }

            await _departmentRepository.AddDepartmentAsync(department);
        }
    

    public async Task<IEnumerable<Department>> GetDepartmentsAsync()
        {
            return await _departmentRepository.GetDepartmentsAsync();
        }

        public async Task<DepartmentDTO> GetDepartmentByIdAsync(int id)
        {
            var department = await _departmentRepository.GetDepartmentByIdAsync(id);
            return _mapper.Map<DepartmentDTO>(department);
        }
        public async Task<DepartmentDTO> GetDepartmentWithSubDepartmentsAsync(int departmentId)
        {
            var department = await _departmentRepository.GetDepartmentWithSubDepartmentsAsync(departmentId);
            var departmentDto = _mapper.Map<DepartmentDTO>(department);
            return departmentDto;
        }
        public async Task<IEnumerable<DepartmentDTO>> GetAllDepartmentsAsync()
        {
            var departments = await _departmentRepository.GetAllDepartmentsAsync();
            return _mapper.Map<IEnumerable<DepartmentDTO>>(departments);
        }

        public async Task<List<DepartmentDTO>> GetParentDepartmentsAsync(int departmentId)
        {
            var parentDepartments = await _departmentRepository.GetParentDepartmentsAsync(departmentId);
            var parentDepartmentDtos = _mapper.Map<List<DepartmentDTO>>(parentDepartments);
            return parentDepartmentDtos;
        }

        public async Task UpdateDepartmentAsync(DepartmentDTO departmentDto)
        {
            var department = _mapper.Map<Department>(departmentDto);
            await _departmentRepository.UpdateDepartmentAsync(department);
        }

        public async Task DeleteDepartmentAsync(int id)
        {
            await _departmentRepository.DeleteDepartmentAsync(id);
        }
    }
}
