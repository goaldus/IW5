using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWManager.BL.Models
{
    public class CourseListModel
    {
        public Guid Id { get; set; }
        public string Abbreviation { get; set; }
        public string CourseName { get; set; }
        public int AcademicYear { get; set; }
    }
}
