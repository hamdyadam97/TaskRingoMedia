using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingoMedia.Dtos
{
    public class DepartmentDetailsViewModel
    {
        public DepartmentDTO Department { get; set; }
        public List<DepartmentDTO> ParentDepartments { get; set; }
    }
}
