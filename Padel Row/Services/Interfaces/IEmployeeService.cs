using Padel_Row.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padel_Row.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<bool> AddOrUpdateEmployee(EmployeeModel employeeModel);
        Task<bool> DeleteEmployee(string key);
        Task<List<EmployeeModel>> GetAllEmployee();

    }
}
