using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mudrik.Application.Interfaces
{
    /// <summary>Maps StudentProfiles.FirstName / AvatarId / GradeLevel — read-only projection.</summary>
    public record StudentBasicInfo(Guid StudentProfileId, string FirstName, string AvatarId, int GradeLevel);

    public interface IStudentDirectoryLookup
    {
        Task<IReadOnlyDictionary<Guid, StudentBasicInfo>> GetBasicInfoAsync(
            IEnumerable<Guid> studentProfileIds, CancellationToken cancellationToken);
    }
}
