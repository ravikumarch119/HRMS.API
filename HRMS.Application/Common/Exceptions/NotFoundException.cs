namespace HRMS.Application.Common.Exceptions
{
    [Serializable]
    public class NotFoundException : Exception
    {
        private string v;
        private int jobHistoryId;

        public NotFoundException()
        {
        }

        public NotFoundException(string? message) : base(message)
        {
        }

        public NotFoundException(string v, int jobHistoryId)
        {
            this.v = v;
            this.jobHistoryId = jobHistoryId;
        }

        public NotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}