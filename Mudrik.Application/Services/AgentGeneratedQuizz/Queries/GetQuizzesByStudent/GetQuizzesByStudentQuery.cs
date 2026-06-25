using MediatR;
using Mudrik.Application.Services.AgentGeneratedQuizz.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.AgentGeneratedQuizz.Queries.GetQuizzesByStudent
{
    public record GetQuizzesByStudentQuery(Guid studentId) : IRequest<List<GenerateQuizDTO>>;
}
