using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SearchFunction.DataLayer;
using SearchFunction.Models;
using System.Diagnostics;

namespace SearchFunction.Controllers
{
    public class HomeController : Controller
    {

        private ICloud cloud;


        public HomeController(ICloud _cloud)
        {

            cloud = _cloud;
        }

        public IActionResult Index()      //READ OPERATION
        {
            var cld = cloud.GetAllEmployees();
            return View(cld);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeDetails details)   //CREATE OPERATION
        {
            if (ModelState.IsValid)
            {
                cloud.AddEmployee(details);
                ModelState.Clear();
                ViewBag.message = "The user " + details.Name + " is saved successfully!!!";

            }
            return RedirectToAction("Index");

        }


        public IActionResult Edit(int id)
        {
            var emp = cloud.FindEmployee(id);
            return View(emp);
        }

        [HttpPost]
        public IActionResult EditDetails(EmployeeDetails details)      //UPDATE OPERATION
        {
            cloud.UpdateEmployees(details);
            return RedirectToAction("Index");

        }

        public IActionResult Delete(int ID)                          //DELETE OPERATION 
        {
            if (ModelState.IsValid)
            {

                cloud.DeleteEmployee(ID);
                ModelState.Clear();
                ViewBag.message = " User with ID " + ID + " deleted successfully!!!";
            }

            return RedirectToAction("Index");
        }

        //public IActionResult Srch()
        //{
        //    return View();
        //}


        public IActionResult Search(string search)
        {
            //List <EmployeeDetails> details = new List<EmployeeDetails>();
            //EmployeeDetails SearchEnt = new EmployeeDetails();

            var xyz = cloud.GetEmployeeBySearch(search);

            if (xyz != null && xyz.Any())
            {
                return View(xyz);
            }
            else
            {
                return BadRequest("No user found with the searchkeyword");
            }

        }

        public IActionResult Gender(string search)
        {
            var pqr = cloud.SearchByGender(search);
            return View(pqr);
        }

        [HttpGet]
        public JsonResult GetAllCustomers()
        {
            //#1 Get SearchEnt from Request
            EmployeeSearchEnt SearchEnt = new EmployeeSearchEnt();
            if (string.IsNullOrEmpty(Request.Query["SearchEnt"]) == false)
                SearchEnt = JsonConvert.DeserializeObject<EmployeeSearchEnt>(Request.Query["SearchEnt"]);

            int page = (string.IsNullOrEmpty(Request.Query["page"]) ? 1 : Convert.ToInt32(Request.Query["page"]));
            int pageSize = (string.IsNullOrEmpty(Request.Query["rows"]) ? 20 : Convert.ToInt32(Request.Query["rows"]));

            string sortColumnName = Request.Query["sidx"];
            string sortDirection = Request.Query["sord"];
            bool _search = Convert.ToBoolean(Request.Query["_search"]);
            string searchColumnName = Request.Query["searchField"];
            string searchKeyword = Request.Query["searchString"];
            string searchOper = Request.Query["searchOper"];

            IEnumerable<EmployeeDetails> CustList = new List<EmployeeDetails>();

            //#3 Check Search Flag
            bool SearchFlag = false;
            if (SearchEnt != null)
            {
                if (SearchEnt.Id == null && SearchEnt.Name == null && SearchEnt.Gender == null && SearchEnt.Email == null
                   )
                    SearchFlag = false;
                else
                    SearchFlag = true;
            }

            if (SearchFlag)
                CustList = cloud.GetCustomerBySearch(SearchEnt);

            int pageIndex = page - 1;

            //#4 Get Total Row Count
            int totalRecords = CustList.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);

            //#5 Setting Sorting  
            if (sortDirection != null)
            {
                if (sortDirection.ToUpper() == "DESC")
                    CustList = CustList.OrderByDescending(s => s.Name);
                else
                    CustList = CustList.OrderBy(s => s.Name);
                CustList = CustList.Skip(pageIndex * pageSize).Take(pageSize);
            }

            //#6 Setting Search  
            if (!string.IsNullOrEmpty(searchKeyword))
            {
                CustList = CustList.Where(m => m.Name == searchKeyword);
            }

            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = CustList.ToList(),
            };
            return Json(jsonData);
        }

        public IActionResult Details(string id)
        {
            var emp = cloud.GetEmployeeDetails(id);
            return View(emp);
        }
    }
}