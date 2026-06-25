using MediatR;
using Mudrik.Application.Interfaces;
using Mudrik.Application.Services.QuizQuestion.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.QuizQuestion.Queries.GetQuizQuestionById
{
    public class GetQuizQuestionByIdQueryHandler(IAppDbContext _context) : IRequestHandler<GetQuizQuestionByIdQuery, GenerateQuizQuestionDTO>
    {
        public async Task<GenerateQuizQuestionDTO> Handle(GetQuizQuestionByIdQuery request, CancellationToken cancellationToken)
        {
            var question = await _context.QuizQuestions.FindAsync(request.id);
            if (question == null)
                return null;

            GenerateQuizQuestionDTO questionDto = new GenerateQuizQuestionDTO()
            {
                Id = question.Id,
                StandardLessonId = question.StandardLessonId,
                SubjectId = question.SubjectId,
                QuestionText = question.QuestionText,
                Format = question.Format,
                OptionsJson = question.OptionsJson,
                CorrectOptionId = question.CorrectOptionId,
                ConceptTag = question.ConceptTag,
                GradeLevel = question.GradeLevel,
                GeneratedAt = question.GeneratedAt
            };

            return questionDto;
        }
    }
}
