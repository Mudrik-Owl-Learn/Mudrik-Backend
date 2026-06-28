using MediatR;
using Mudrik.Application.Interfaces;
using Mudrik.Application.Services.Curriculum.DTOs;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mudrik.Application.Services.Curriculum.Queries.GetCurriculumStats
{
    public record GetCurriculumStatsQuery : IRequest<CurriculumStatsDto>;

    
}
