using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Transactions
{
    public class GetTransactionByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
  
        =>app.MapGet("/{id}",HandleAsync)
            .WithName("Transactions: Get By Id")
            .WithSummary("Recupera uma transacao")
            .WithDescription("Recupera uma transacao")
            .WithOrder(4)
            .Produces<Response<Transaction?>>();

        public static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            long id,
            ITransactionHandler handler)
        {
            var request = new GetTransactionByIdRequest
            {
                Id = id,
                UserId = user.Identity?.Name ?? string.Empty
            };

            var result = await handler.GetByIdAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }
    }
}
