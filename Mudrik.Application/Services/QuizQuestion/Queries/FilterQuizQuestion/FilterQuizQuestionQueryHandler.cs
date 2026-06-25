using MediatR;
using Mudrik.Application.Interfaces;
using Mudrik.Application.Services.QuizQuestion.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Mudrik.Application.Services.QuizQuestion.Queries.FilterQuizQuestion
{
    public class FilterQuizQuestionQueryHandler : IRequestHandler<FilterQuizQuestionQuery, List<GenerateQuizQuestionDTO>>
    {
        private readonly IAppDbContext _context;

        public FilterQuizQuestionQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<GenerateQuizQuestionDTO>> Handle(FilterQuizQuestionQuery request, CancellationToken cancellationToken)
        {
            var filteredQuestions = await _context.QuizQuestions
                 .Where(q => q.StandardLessonId == request.StandardLessonId &&
                             q.ConceptTag == request.ConceptTag &&
                             q.GradeLevel == request.GradeLevel)
                 .Select(q => new GenerateQuizQuestionDTO
                 {
                     Id = q.Id,
                     StandardLessonId = q.StandardLessonId,
                     SubjectId = q.SubjectId,
                     QuestionText = q.QuestionText,
                     Format = q.Format,
                     OptionsJson = q.OptionsJson,
                     CorrectOptionId = q.CorrectOptionId,
                     ConceptTag = q.ConceptTag,
                     GradeLevel = q.GradeLevel,
                     GeneratedAt = q.GeneratedAt
                 })
                 .ToListAsync(cancellationToken);

            if (filteredQuestions.Count == 0)
                return null;

            return filteredQuestions;
        }
    }
}
