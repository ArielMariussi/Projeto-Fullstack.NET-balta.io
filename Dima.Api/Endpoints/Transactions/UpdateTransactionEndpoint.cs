using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Dima.Api.Endpoints.Transactions
{
    public class UpdateTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)

         => app.MapPut("/{id}", HandleAsync)
            .WithName("Transactions: Update")
            .WithSummary("Atualiza uma nova transacao")
            .WithDescription("Atualiza uma nova transacao")
            .WithOrder(3)
            .Produces<Response<Transaction?>>();

        public static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            long id,
            ITransactionHandler handler,
            UpdateTransactionRequest request)
        {
            request.Id = id;
            request.UserId = user.Identity?.Name ?? string.Empty;

            var result = await handler.UpdateAsync(request);
            return result.IsSuccess
                ? TypedResults.Ok(result)
                : TypedResults.BadRequest(result);
        }

    }
}
