using HRMS.Application.Employees.Interfaces;
using HRMS.Domain;
using HRMS.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Infrastructure.Repositories
{
    public class JobHistoryRepository : IJobHistoryRepository
    {
        private readonly HRMSDbContext _context;

        public JobHistoryRepository(HRMSDbContext context)
        {
            _context = context;
        }

        public async Task<JobHistory?> GetJobHistoryById(int id) =>
            await _context.JobHistories.FindAsync(id);

        public async Task UpdateJobHistory(JobHistory jobHistory)
        {
            _context.JobHistories.Update(jobHistory);
            await _context.SaveChangesAsync();
        }
    }
}
