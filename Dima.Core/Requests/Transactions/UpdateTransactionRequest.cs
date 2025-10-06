using Dima.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Dima.Core.Requests.Transactions
{
    public class UpdateTransactionRequest : Request
    {

        public long  Id{ get; set; }

        [Required(ErrorMessage = "TItulo invalido")]
        public string Title { get; set; } = string.Empty;


        [Required(ErrorMessage = "Tipo invalida")]
        public ETransactionType Type { get; set; }


        [Required(ErrorMessage = "Valor invalida")]
        public decimal Amount { get; set; }


        [Required(ErrorMessage = "Categoria invalida")]
        public long CategoryId { get; set; }


        [Required(ErrorMessage = "Data invalida")]
        public DateTime? PaidOrReceivedAt { get; set; }
    }
}
