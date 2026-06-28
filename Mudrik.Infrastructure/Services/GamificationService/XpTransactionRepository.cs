using Microsoft.EntityFrameworkCore;
using Mudrik.Application.Interfaces;
using Mudrik.Domain.Models;
using Mudrik.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mudrik.Infrastructure.Services.GamificationService
{
    public class XpTransactionRepository(AppDbContext context) : IXpTransactionRepository
    {
        public async Task<XpTransaction> AddAsync(XpTransaction transaction, CancellationToken cancellationToken)
        {
            await context.XpTransactions.AddAsync(transaction, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return transaction;
        }

        public async Task<XpTransaction?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await context.XpTransactions
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
        }

        public async Task<(IReadOnlyList<XpTransaction> Items, int TotalCount)> GetPagedForStudentAsync(
            Guid studentProfileId, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var query = context.XpTransactions
                .AsNoTracking()
                .Where(t => t.StudentProfileId == studentProfileId)
                .OrderByDescending(t => t.AwardedAt);

            var totalCount = await query.CountAsync(cancellationToken);
            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (items, totalCount);
        }
    }
}
