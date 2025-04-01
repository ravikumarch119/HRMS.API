using HRMS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Employees.Interfaces
{
    public interface IJobHistoryRepository
    {
        Task<JobHistory?> GetJobHistoryById(int id);
        Task UpdateJobHistory(JobHistory jobHistory);
    }

}
