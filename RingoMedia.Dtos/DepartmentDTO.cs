using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingoMedia.Dtos
{
    public class DepartmentDTO
    {

        public string Name { get; set; }
        public int? Id { get; set; }

       
        public string? Logo { get; set; }

        public IFormFile? LogoFile { get; set; }

        public int? ParentDepartmentId { get; set; }

        public List<int> ?SubDepartmentIds { get; set; }
        public List<DepartmentDTO>? SubDepartments { get; set; }
    }
}
