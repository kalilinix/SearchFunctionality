using SearchFunction.Models;

namespace SearchFunction.DataLayer
{
    public interface ICloud
    {
        public List<EmployeeDetails> GetAllEmployees();

        public bool AddEmployee(EmployeeDetails employee);

        public EmployeeDetails FindEmployee(int ID);

        public bool UpdateEmployees(EmployeeDetails details);

        public bool DeleteEmployee(int id);

        public List<EmployeeDetails> GetEmployeeBySearch(string search);
        public List<EmployeeDetails> SearchByGender(string search);

        public EmployeeDetails GetEmployeeDetails(string id);
        public IEnumerable<EmployeeDetails> GetCustomerBySearch(EmployeeSearchEnt objCustSearchEnt);

    }
}
