using FluentValidation;
using MediatR;
using Mudrik.Application.Interfaces;
using Mudrik.Application.Services.Curriculum.DTOs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mudrik.Application.Services.Curriculum.Queries.GetLessonById
{
    public record GetLessonByIdQuery(Guid LessonId) : IRequest<LessonDetailDto>;

    
}
