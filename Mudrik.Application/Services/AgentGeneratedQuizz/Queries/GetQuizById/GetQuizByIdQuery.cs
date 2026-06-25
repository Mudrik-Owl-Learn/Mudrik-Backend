using MediatR;
using Mudrik.Application.Services.AgentGeneratedQuizz.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.AgentGeneratedQuizz.Queries.GetQuizById
{
    public record GetQuizByIdQuery(Guid id) : IRequest<GenerateQuizDTO>;
}
