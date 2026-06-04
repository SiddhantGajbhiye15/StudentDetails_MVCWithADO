using MVCWithADO.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCWithADO.Controllers
{
    public class StudentController : Controller
    {
        StudentDLA dal;
        public StudentController()
        {
            dal = new StudentDLA();
        }
        // GET: Student
        public ActionResult DisplayStudent()
        {
            List<Student> students = dal.SelectStudents(null, true);
            return View(students);
        }
        public ActionResult StudentDetails(int Sid)
        {
            Student student = dal.SelectStudents(Sid, true)[0];
            return View(student);
        }
        [HttpGet]
        public ActionResult AddStudent()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddStudent(Student student, HttpPostedFileBase selectedFile)
        {
            if(selectedFile != null)
            {
                string folderPath = Server.MapPath("~/Uploads/");
                if(!Directory.Exists(folderPath))                
                    Directory.CreateDirectory(folderPath);
                string fileExtension = Path.GetExtension(selectedFile.FileName);
                string newFileName = Guid.NewGuid().ToString() + fileExtension;
                selectedFile.SaveAs(folderPath + newFileName);
                student.Photo = newFileName;
            }
            if(dal.InsertStudent(student) > 0)
                return RedirectToAction("DisplayStudent");
            else
                return View(student);
         
        }
        public ActionResult EditStudent(int Sid)
        {
            Student student = dal.SelectStudents(Sid, true).Single();
            TempData["Photo"] = student.Photo;
            return View(student);
        }
        public ActionResult UpdateStudent(Student student, HttpPostedFileBase selectedFile)
        {

            if (selectedFile != null)
            {
                string folderPath = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);
                string fileExtension = Path.GetExtension(selectedFile.FileName);
                string newFileName = Guid.NewGuid().ToString() + fileExtension;
                selectedFile.SaveAs(folderPath + newFileName);
                student.Photo = newFileName;
            }
            else if(TempData["Photo"] != null && Convert.ToString(TempData["Photo"]) != "")
            {
                student.Photo = TempData["Photo"].ToString();
            }
            if (dal.UpdateStudent(student) > 0)
            {
                return RedirectToAction("DisplayStudent");
            }
            else
            {
                return View("EditStudent", student);
            }
        }
            
        public ActionResult DeleteStudent(int Sid)
        {
            if (dal.DeleteStudent(Sid) > 0)
            {
                return RedirectToAction("DisplayStudent");
            }
            else
            {
                return View("DeleteErrorView");
            }
        }
    }
}