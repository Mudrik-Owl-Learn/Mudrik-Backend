using FluentValidation;
using MediatR;
using Mudrik.Application.Interfaces;
using Mudrik.Application.Services.Curriculum.DTOs;
using Mudrik.Domain.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mudrik.Application.Services.Curriculum.Queries.GetLessons
{
    public record GetLessonsQuery(
        Guid?   SubjectId,
        int?    GradeLevel,
        bool?   IsActive,
        string? SearchTerm,
        int     Page     = 1,
        int     PageSize = 20
    ) : IRequest<LessonListResultDto>;

}
