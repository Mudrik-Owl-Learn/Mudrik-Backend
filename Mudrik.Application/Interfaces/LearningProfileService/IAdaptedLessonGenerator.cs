using Mudrik.Domain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Mudrik.Application.Interfaces
{
    /// <summary>
    /// Takes a StandardLesson and a LearnerAIProfile and returns an AdaptedContentJson string
    /// (JSON array of chunks). Infrastructure implementation uses Semantic Kernel + GPT-4o.
    /// Wrapped with a 30-second timeout to prevent hanging during integration testing.
    /// </summary>
    public interface IAdaptedLessonGenerator
    {
        Task<string> GenerateAsync(
            StandardLesson lesson,
            LearnerAIProfile profile,
            CancellationToken cancellationToken);
    }
}
