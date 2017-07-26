using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TeamProgrammer.UniversityManagement.Core.Business_Interface;
using TeamProgrammer.UniversityManagement.Core.Entities;
using TeamProgrammer.UniversityManagement.DataRepository.Context;

namespace TeamProgrammer.UniversityManagement.Web.Controllers.University
{
    public class StudentsController : Controller
    {
        private readonly IStudentBusiness _studentBusiness;
        private readonly IDepartmentBusiness _departmentBusiness;
        private readonly ISemesterBusiness _semesterBusiness;

        public StudentsController(IStudentBusiness studentBusiness, IDepartmentBusiness departmentBusiness,
            ISemesterBusiness semesterBusiness)
        {
            _studentBusiness = studentBusiness;
            _departmentBusiness = departmentBusiness;
            _semesterBusiness = semesterBusiness;
        }

        // GET: Students
        public ActionResult Index()
        {

            return View();
        }

        public JsonResult GetAllStudents(string draw, int? start, int? length)
        {
            int initial = start ?? 0;
            int totalPage = length ?? 0;
            var totalRecords = _studentBusiness.GetAll().Count();
            var studentList = _studentBusiness.GetAll().OrderBy(c => c.StudentName).Skip(initial).Take(totalPage);
            var students = studentList.Select(c => new
            {

                StudentId = c.StudentId,
                StudentName = c.StudentName,
                Registration = c.Registration,
                Email = c.Email,
                Date = c.Date,
                Seasson = c.Seasson,
                Address = c.Address,
                ContactNo = c.ContactNo,
                DeptName = c.Department.DeptName,
                Semester = c.Semester.SemesterName
            });
            var jsonData =
                new {draw = draw, recordsTotal = totalRecords, recordsFiltered = totalRecords, data = students};
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = _studentBusiness.GetById((int) id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(_departmentBusiness.GetAll(), "DepartmentId", "DeptName");
            ViewBag.SemesterId = new SelectList(_semesterBusiness.GetAll(), "SemesterId", "SemesterName");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(
                Include =
                    "StudentId,StudentName,Registration,Email,Date,Seasson,Address,ContactNo,DepartmentId,SemesterId")] Student student)
        {
            if (string.IsNullOrEmpty(student.Registration))
            {
                student.Registration = _studentBusiness.GetRegistrationNo(student.DepartmentId, student.Seasson);
            }
            if (ModelState.IsValid)
            {
                _studentBusiness.Add(student);
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(_departmentBusiness.GetAll(), "DepartmentId", "DeptName");
            ViewBag.SemesterId = new SelectList(_semesterBusiness.GetAll(), "SemesterId", "SemesterName");
            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = _studentBusiness.GetById((int) id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(_departmentBusiness.GetAll(), "DepartmentId", "DeptName",
                student.DepartmentId);
            ViewBag.SemesterId = new SelectList(_semesterBusiness.GetAll(), "SemesterId", "SemesterName",
                student.SemesterId);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(
                Include =
                    "StudentId,StudentName,Registration,Email,Date,Seasson,Address,ContactNo,DepartmentId,SemesterId")] Student student)
        {
            if (ModelState.IsValid)
            {
                _studentBusiness.Update(student);
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(_departmentBusiness.GetAll(), "DepartmentId", "DeptName",
                student.DepartmentId);
            ViewBag.SemesterId = new SelectList(_semesterBusiness.GetAll(), "SemesterId", "SemesterName",
                student.SemesterId);
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = _studentBusiness.GetById((int) id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = _studentBusiness.GetById((int) id);
            _studentBusiness.Delete(student);
            return RedirectToAction("Index");
        }

        public ActionResult ShowStudentCourses()
        {
            ViewBag.StudentId = new SelectList(_studentBusiness.GetAll(), "StudentId", "Registration");
            ViewBag.DepartmentId = new SelectList(_departmentBusiness.GetAll(), "DepartmentId", "DeptName");
            return View();
        }

        public JsonResult GetCourseByStudentId(int studentId)
        {
            UniversityDbContext db = new UniversityDbContext();
            string dept =
                db.StudentDeptSemesters.Where(x => x.StudentId == studentId).Select(x => x.DeptName).FirstOrDefault();
            string studentName =
                db.StudentDeptSemesters.Where(x => x.StudentId == studentId).Select(x => x.StudentName).FirstOrDefault();
            string semester =
                db.StudentDeptSemesters.Where(x => x.StudentId == studentId)
                    .Select(x => x.SemesterName)
                    .FirstOrDefault();
            var courses =
                db.CourseDeptSemesters.Where(x => x.DeptName == dept && x.SemesterName == semester).Select(x => new
                {
                    courseName = x.CourseName,
                    courseCode = x.CourseCode,
                    description = x.Discription,
                    credit = x.Credit,
                    StudentName = studentName
                }).ToList();
            return Json(courses, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetSemesters()
        {
            var semesters = _semesterBusiness.GetAll();
            var sem = semesters.Select(x => new
            {
                SemesterId = x.SemesterId,
                SemesterName = x.SemesterName
            });
            return Json(sem, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetStudentBySemesterIdAndDeptId(int deptId, int semesterId )
        {
            var students = _studentBusiness.GetAll();
            var result = students.Where(x => x.DepartmentId == deptId && x.SemesterId == semesterId).Select(x => new
            {
                studentId = x.StudentId,
                registration = x.Registration
            });
            return Json(result, JsonRequestBehavior.AllowGet);
        }
}
}
