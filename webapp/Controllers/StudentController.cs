using Microsoft.AspNetCore.Mvc;
using webapp.Models;

namespace webapp.Controllers
{
    public class StudentController : Controller
    {
        PRN211_1Context context = new PRN211_1Context();
        public IActionResult Index()
        {

            ViewBag.student = (from student in context.Students
                               join depart in context.Departments
                               on student.DepartId equals depart.Id
                               select new Student
                               {
                                   Id = student.Id,
                                   Name = student.Name,
                                   Gender = student.Gender,
                                   DepartId = depart.Name,
                                   Dob = student.Dob,
                                   Gpa = student.Gpa,
                               }).ToList();
            ViewBag.depart = context.Departments.ToList();
            return View();
      

        }

        [HttpPost]
        public IActionResult add(IFormCollection f)
        {
            try
            {
                int id = Int32.Parse(f["id"]);
                String name = f["name"];
                Boolean gd;
                if (Int32.Parse(f["gd"]) == 1)
                {
                    gd = true;
                }
                else
                {
                    gd = false;
                }
                String departid = f["departid"];
                DateTime dob = DateTime.Parse(f["dob"]);
                double gpa = Double.Parse(f["gpa"]);
                var items = (from student in context.Students
                             where student.Id == id
                             select student).ToList();

                if (items.Count == 0)
                {
                    Student s = new Student();
                    s.Id = id;
                    s.Name = name;
                    s.DepartId = departid;
                    s.Gpa = gpa;
                    s.Dob = dob;
                    s.Gender = gd;
                    context.Students.Add(s);
                    context.SaveChanges();
                    s.Id = 0;
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public IActionResult delete(int id, int type)
        {


            if (type == 0)
            {
                var x = context.Students.Find(id);
                ViewBag.student = x;
                ViewBag.depart = context.Departments.ToList();
                return View();
            }
            else
            {
                var x = context.Students.Find(id);
                context.Students.Remove(x);
                context.SaveChanges();
            }
            return Redirect("Index");

        }
        [HttpPost]
        public IActionResult delete(IFormCollection f)
        {
            try
            {
                var x = context.Students.Find(Int32.Parse(f["id"]));
                x.Id = Int32.Parse(f["id"]);
                x.Name = f["name"];
                if (Int32.Parse(f["gd"]) == 1)
                {
                    x.Gender = true;
                }
                else
                {
                    x.Gender = false;
                }
                x.DepartId = f["departid"];
                x.Gpa = Double.Parse(f["gpa"]);
                x.Dob = DateTime.Parse(f["dob"]);
                context.Students.Update(x);
                context.SaveChanges();
                return Redirect("Index");
            }catch (Exception e){
                return Redirect("Index");
            }
        }


    }
}

