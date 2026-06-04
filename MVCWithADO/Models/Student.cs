using System.ComponentModel.DataAnnotations;

namespace MVCWithADO.Models
{
    public class Student
    {
        [Display(Name = "Student ID")]
        public int Sid { get; set; }
        [Display(Name = "Student Name")]
        public string Name { get; set; }
        public int Class { get; set; }
        public int Fees { get; set; }
        public string Photo { get; set; }

    }
}