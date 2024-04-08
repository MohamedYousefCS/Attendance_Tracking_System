using AttendanceTrackingSystem.DBContext;
using AttendanceTrackingSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceTrackingSystem.Repos
{


    public interface IEmployeeRepo
    {

        public List<Employee> GetAllEmployees();

        public void Add(Employee employee);

        public Employee GetById(int id);


        public void Update(Employee employee);

        public void Delete(int id);
    }


    public class EmployeeRepo : IEmployeeRepo
    {
        ITIDBContext db;
        public EmployeeRepo(ITIDBContext _db)
        {
            db = _db;
        }

        //Show all Employees
        public List<Employee> GetAllEmployees()
        {
            return db.employees.ToList();


        }

        //show Employee Data
        public Employee GetById(int id)
        {
            return db.employees.FirstOrDefault(a => a.Id == id);
        }



        //add Employee
        public void Add(Employee employee)
        {

            db.employees.Add(employee);
            db.SaveChanges();
        }

        //Update Employee data
        public void Update(Employee employee)
        {
            db.Update(employee);
            db.SaveChanges();

        }

        //Delete Employee
        public void Delete(int id)
        {
            var employeeToDelete = db.employees.FirstOrDefault(e => e.Id == id);
            if (employeeToDelete != null)
            {
                db.employees.Remove(employeeToDelete);
                db.SaveChanges();
            }
        }


    }

}


