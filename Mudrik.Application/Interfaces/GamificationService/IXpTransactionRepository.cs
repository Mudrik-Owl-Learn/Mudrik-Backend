using Mudrik.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mudrik.Application.Interfaces
{
    public interface IXpTransactionRepository
    {
        Task<XpTransaction> AddAsync(XpTransaction transaction, CancellationToken cancellationToken);
        Task<XpTransaction?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<(IReadOnlyList<XpTransaction> Items, int TotalCount)> GetPagedForStudentAsync(
            Guid studentProfileId, int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}
