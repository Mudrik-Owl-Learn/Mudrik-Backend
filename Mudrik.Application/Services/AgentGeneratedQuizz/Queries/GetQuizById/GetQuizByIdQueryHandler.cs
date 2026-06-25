using MediatR;
using Mudrik.Application.Interfaces;
using Mudrik.Application.Services.AgentGeneratedQuizz.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.AgentGeneratedQuizz.Queries.GetQuizById
{
    public class GetQuizByIdQueryHandler(IAppDbContext _context) : IRequestHandler<GetQuizByIdQuery, GenerateQuizDTO>
    {
        public async Task<GenerateQuizDTO> Handle(GetQuizByIdQuery request, CancellationToken cancellationToken)
        {
            var quizExists = await _context.AgentGeneratedQuizzes.AnyAsync(a => a.Id == request.id, cancellationToken);

            if (!quizExists)
                return null;


            var quizDTO = new GenerateQuizDTO()
            {
                id = request.id,

                questions = await _context.QuizQuestions.Where(a => a.AgentGeneratedQuizQuestions.Any(q => q.AgentGeneratedQuizId == request.id)).Select(s => new QuizQuestionWithoutCorrectOptionId()
                {
                    Id = s.Id,
                    StandardLessonId = s.StandardLessonId,
                    SubjectId = s.SubjectId,
                    QuestionText = s.QuestionText,
                    Format = s.Format,
                    OptionsJson = s.OptionsJson,
                    ConceptTag = s.ConceptTag,
                    GradeLevel = s.GradeLevel,
                    GeneratedAt = s.GeneratedAt
                }).ToListAsync(cancellationToken)
            };

            return quizDTO;
        }
    }
}
