using Microsoft.EntityFrameworkCore;
using Mudrik.Application.Interfaces;
using Mudrik.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mudrik.Infrastructure.Services.GamificationService
{
    public class StudentDirectoryLookup(AppDbContext context) : IStudentDirectoryLookup
    {
        public async Task<IReadOnlyDictionary<Guid, StudentBasicInfo>> GetBasicInfoAsync(
            IEnumerable<Guid> studentProfileIds, CancellationToken cancellationToken)
        {
            var idList = studentProfileIds.Distinct().ToList();
            if (idList.Count == 0) return new Dictionary<Guid, StudentBasicInfo>();

            var results = await context.StudentProfiles
                .AsNoTracking()
                .Where(s => idList.Contains(s.Id))
                .Select(s => new StudentBasicInfo(s.Id, s.FirstName, s.AvatarId, s.GradeLevel))
                .ToListAsync(cancellationToken);

            return results.ToDictionary(s => s.StudentProfileId);
        }
    }
}
