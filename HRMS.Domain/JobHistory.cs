using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Domain
{
    public class JobHistory
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int ManagerId { get; set; }
        public Employee Manager { get; set; }
        public string JobTitle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; } = "Active";

        public DateTime InsertedDate { get; set; } // New field
        public DateTime UpdatedDate { get; set; } // New field


    }
}
