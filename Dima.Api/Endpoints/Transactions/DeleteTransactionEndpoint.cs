using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Transactions
{
    public class DeleteTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
       
        =>app.MapDelete("/{id}",HandleAsync)
            .WithName("Transactions: Delete")
            .WithSummary("Exclui uma nova transacao")
            .WithDescription("Exclui uma nova transacao")
            .WithOrder(2)
            .Produces<Response<Transaction?>>();

        public static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            long id,
            ITransactionHandler handler)
        {
            var request = new DeleteTransactionRequest
            {
                UserId = user.Identity?.Name ?? string.Empty,
                Id = id
            };

            var result = await handler.DeleteAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }

    }
}
