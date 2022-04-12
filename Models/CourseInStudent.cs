using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCEntityFrameAppCS.Models
{
    public class CourseInStudent
    {
        [Display(Name = "Student Id")]
        public int StudentId { get; set; }

        [Display(Name = "Student Name")]
        public string StudentName { get; set; }

        [Display(Name = "Course Name")]
        public string Coursename { get; set; }

    }
}