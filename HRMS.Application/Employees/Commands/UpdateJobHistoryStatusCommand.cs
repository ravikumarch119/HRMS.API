using HRMS.Application.Common.Exceptions;
using HRMS.Application.Employees.Interfaces;
using HRMS.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Employees.Commands
{
    public class UpdateJobHistoryStatusCommand : IRequest<Unit>
    {
        public int JobHistoryId { get; set; }
        public string Status { get; set; }
    }
    public class UpdateJobHistoryStatusCommandHandler : IRequestHandler<UpdateJobHistoryStatusCommand,Unit>
    {
        private readonly IJobHistoryRepository _repository;

        public UpdateJobHistoryStatusCommandHandler(IJobHistoryRepository jobHistoryRepository)
        {
            _repository = jobHistoryRepository;
        }

        public async Task<Unit> Handle(UpdateJobHistoryStatusCommand request, CancellationToken cancellationToken)
        {
            var jobHistory = await _repository.GetJobHistoryById(request.JobHistoryId);
            if (jobHistory == null)
            {
                throw new NotFoundException(nameof(JobHistory), request.JobHistoryId);
            }

            jobHistory.Status = request.Status;
            jobHistory.UpdatedDate = DateTime.UtcNow;

            await _repository.UpdateJobHistory(jobHistory);

            return Unit.Value;
        }
    }

}