using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Domain
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Status { get; set; } = "Active";

        [ForeignKey("Department")]
        public int DepartmentID { get; set; }  // Ensure this matches DB column name

        public Department Department { get; set; }

       // public List<JobHistory> JobHistories { get; set; } = new();

        public byte[] RowVersion { get; set; }

        public DateTime InsertedDate { get; set; } // New field
        public DateTime UpdatedDate { get; set; } // New field
    }
}
