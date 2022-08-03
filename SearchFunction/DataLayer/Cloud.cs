using Dapper;
using SearchFunction.Models;
using System.Data.SqlClient;

namespace SearchFunction.DataLayer
{
    public class Cloud : ICloud
    {
        private string connectionstring = string.Empty;
        private IConfiguration configuration;
        protected IDataRepository _repository;

        public Cloud(IConfiguration config, IDataRepository repo)
        {
            configuration = config;
            _repository = repo;
            connectionstring = configuration.GetConnectionString("RTG");
        }


        public bool AddEmployee(EmployeeDetails employee)
        {
            string query = $"Insert into EmployeeDetails (Name,Gender,Email) values ('{employee.Name}', '{employee.Gender}','{employee.Email}')";


            using (SqlConnection sql = new SqlConnection(connectionstring))
            {
                sql.Open();
                var parameters = new DynamicParameters();
                parameters.Add("Name", employee.Name, System.Data.DbType.String);
                parameters.Add("Gender", employee.Gender, System.Data.DbType.String);
                parameters.Add("Email", employee.Email, System.Data.DbType.String);
                int rowsaffected = sql.Execute(query, parameters);
                return true;
            }

        }

        public bool DeleteEmployee(int id)
        {
            string query = $"Delete from EmployeeDetails where ID = {id} ";
            using (SqlConnection sql = new SqlConnection(connectionstring))
            {
                sql.Open();
                var parameters = new DynamicParameters();
                parameters.Add("ID", id, System.Data.DbType.Int32);
                int rowsaffected = sql.Execute(query, parameters);
                return true;
            }

        }

        public EmployeeDetails FindEmployee(int ID)
        {
            string query = $"Select * From EmployeeDetails where ID = {ID}";
            using (SqlConnection sql = new SqlConnection(connectionstring))
            {
                sql.Open();
                var emp = sql.Query<EmployeeDetails>(query);
                return emp.FirstOrDefault();
            }
        }



        public List<EmployeeDetails> GetAllEmployees()
        {
            string query = "Select ID,Name, Gender, Email from EmployeeDetails";
            using (SqlConnection sql = new SqlConnection(connectionstring))
            {
                sql.Open();
                var emp = sql.Query<EmployeeDetails>(query);
                return emp.ToList();
            }
        }

        public List<EmployeeDetails> GetEmployeeBySearch(string search)
        {
            string query = $"Select * from EmployeeDetails where Name LIKE @search OR Gender LIKE @search OR Email LIKE @search ";
            using (SqlConnection sql = new SqlConnection(connectionstring))
            {
                sql.Open();
                var parameters = new DynamicParameters();
                parameters.Add("search", $"%{search}%", System.Data.DbType.String);
                var abc = sql.Query<EmployeeDetails>(query, parameters);
                return abc.ToList();
            }
        }

        public bool UpdateEmployees(EmployeeDetails details)
        {
            string query = $"Update  EmployeeDetails  SET Name = @Name,Gender = @Gender,Email = @Email WHERE ID = @ID ";

            using (SqlConnection sql = new SqlConnection(connectionstring))
            {
                sql.Open();
                var parameters = new DynamicParameters();
                parameters.Add("Name", details.Name, System.Data.DbType.String);
                parameters.Add("Gender", details.Gender, System.Data.DbType.String);
                parameters.Add("Email", details.Email, System.Data.DbType.String);

                parameters.Add("ID", details.Id, System.Data.DbType.Int32);
                int rowsaffected = sql.Execute(query, parameters);
                return true;

            }
        }

        public List<EmployeeDetails> SearchByGender(string search)
        {
            string query = $"select * from EmployeeDetails where Gender LIKE @search";
            using (SqlConnection sql = new SqlConnection(connectionstring))
            {
                sql.Open();
                var parameters = new DynamicParameters();
                parameters.Add("search", $"%{search}%", System.Data.DbType.String);
                var abc = sql.Query<EmployeeDetails>(query, parameters);
                return abc.ToList();
            }
        }

        public IEnumerable<EmployeeDetails> GetCustomerBySearch(EmployeeSearchEnt objCustSearchEnt)
        {
            string sFilterString = "";

            if (!string.IsNullOrEmpty(objCustSearchEnt.Id))
            {
                sFilterString = $" Id like '%{objCustSearchEnt.Id}%' ";
            }
            if (!string.IsNullOrEmpty(objCustSearchEnt.Name))
            {
                if (string.IsNullOrEmpty(sFilterString))
                    sFilterString = $" Name like '%{objCustSearchEnt.Name}%' ";
                else
                    sFilterString += $" and Name like '%{objCustSearchEnt.Name}%' ";
            }

            if (!string.IsNullOrEmpty(objCustSearchEnt.Gender))
            {
                if (string.IsNullOrEmpty(sFilterString))
                    sFilterString = $" Gender like '%{objCustSearchEnt.Gender}%' ";
                else
                    sFilterString += $" and Gender like '%{objCustSearchEnt.Gender}%' ";
            }

            if (!string.IsNullOrEmpty(objCustSearchEnt.Email))
            {
                if (string.IsNullOrEmpty(sFilterString))
                    sFilterString = $" Email like '%{objCustSearchEnt.Email}%' ";
                else
                    sFilterString += $" and Email like '%{objCustSearchEnt.Email}%' ";
            }



            List<EmployeeDetails> lstCustomer = new List<EmployeeDetails>();
            if (sFilterString != "")
                lstCustomer = _repository.QueryEntity<EmployeeDetails>(sFilterString, "Id Asc").ToList();
            return lstCustomer;
        }

        public EmployeeDetails GetEmployeeDetails(string id)
        {
            string query = $"select * from EmployeeDetails where Id = {id} ";
            using (SqlConnection sql = new SqlConnection(connectionstring))
            {
                sql.Open();
                //var parameters = new DynamicParameters();
                //parameters.Add("search", $"%{search}%", System.Data.DbType.String);
                var abc = sql.Query<EmployeeDetails>(query);
                return abc.FirstOrDefault();
            }
        }
    }
}
