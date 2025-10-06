using Dima.Api.Data;
using Dima.Core.Common.Extensions;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Handlers
{
    public class TransactionHandler(AppDbContext context) : ITransactionHandler
    {
       public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
        {
            try
            {
                var transaction = new Transaction
                {
                    UserId = request.UserId,
                    Title = request.Title,
                    Type = request.Type,
                    CreatedAt = DateTime.Now,
                    Amount = request.Amount,
                    CategoryId = request.CategoryId,
                    PaidOrReceivedAt = request.PaidOrReceivedAt,
                };

                await context.Transactions.AddAsync(transaction);
                await context.SaveChangesAsync();

                return new Response<Transaction?>(transaction, 201, "Uma nova Transacao foi criada com sucesso");

            }
            catch 
            {
                return new Response<Transaction?>(null, 500, "Nao foi possivel criar uma nova Transacao");

            }
            
        }

        public async Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
        {
            try
            {
                var transaction = await context.Transactions
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (transaction == null)
                   return new Response<Transaction?>(null, 404, "Transacao nao foi encontrada");

                context.Transactions.Remove(transaction);
                await context.SaveChangesAsync();
                   return new Response<Transaction?>(transaction, 200, "Transcao excluida com sucesso");
            }
            catch 
            {
                return new Response<Transaction?>(null, 500, "Nao foi possivel excluir a transacao");
            }
        }

        public async Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request)
        {
            try
            {
                var transaction = await context.Transactions
             .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                return transaction is null
                   ? new Response<Transaction?>(null, 404, "Transacao nao foi encontrada")
                   : new Response<Transaction?>(transaction, message: "Transacao encontrada com sucesso");

            }
            catch 
            {
                return new Response<Transaction?>(null, 500, "Nao foi possivel recuperar a transacao");
               
            }

           
        }

       public async Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(GetTransactionsByPeriodRequest request)
        {
            try
            {
                request.StartDate ??= DateTime.Now.GetFirstDay();
                request.EndDate ??= DateTime.Now.GetLastDay();
            }
            catch 
            {
                return new PagedResponse<List<Transaction>?>(null, 500, "Nao foi possivel determinar as datas de inicio e terminio");
               
            }

            try
            {
            var query = context
                .Transactions
                .AsNoTracking()
                .Where(x => x.CreatedAt >= request.StartDate 
                && x.CreatedAt <= request.EndDate 
                && x.UserId == request.UserId)
                .OrderBy(x => x.CreatedAt);

            var transaction = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var count = await query.CountAsync();

            return new PagedResponse<List<Transaction>?>(transaction,
                count,
                request.PageNumber,
                request.PageSize);

            }
            catch 
            {
                return new PagedResponse<List<Transaction>?>(null, 500, "Nao foi possivel obter as transacoes");

            }


        }


        public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
        {
            try
            {
                var transaction = await context.Transactions
               .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if (transaction == null)
                    return new Response<Transaction?>(null, 404, "Transacao nao encontrada");

                transaction.Title = request.Title;
                transaction.Type = request.Type;
                transaction.Amount = request.Amount;
                transaction.CategoryId = request.CategoryId;
                transaction.PaidOrReceivedAt = request.PaidOrReceivedAt;

                context.Transactions.Update(transaction);
                await context.SaveChangesAsync();

                return new Response<Transaction?>(transaction, 200, "Transacao atualizada com sucesso");

            }
            catch
            {
                return new Response<Transaction?>(null, 500, "Nao foi possivel atualizar a transacao");

            }
        }
    }
}
