using System.Reflection.Metadata.Ecma335;

namespace Dima.Core.Requests.Transactions
{
    public class GetTransactionsByPeriodRequest : PagedRequest
    {
        public DateTime? StartDate{ get; set; }
        public DateTime? EndDate { get; set; }
    }
}
