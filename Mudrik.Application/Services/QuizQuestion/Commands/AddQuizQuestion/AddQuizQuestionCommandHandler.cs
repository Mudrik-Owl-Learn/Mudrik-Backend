using MediatR;
using Mudrik.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.QuizQuestion.Commands.AddQuizQuestion
{
    public class AddQuizQuestionCommandHandler(IAppDbContext _context) : IRequestHandler<AddQuizQuestionCommand, Guid>
    {
        public async Task<Guid> Handle(AddQuizQuestionCommand request, CancellationToken cancellationToken)
        {

            var question = new Domain.Models.QuizQuestion()
            {
                Id = new Guid(),
                StandardLessonId = request.StandardLessonId,
                SubjectId = request.SubjectId,
                QuestionText = request.QuestionText,
                Format = request.Format,
                OptionsJson = request.OptionsJson,
                CorrectOptionId = request.CorrectOptionId,
                ConceptTag = request.ConceptTag,
                GradeLevel = request.GradeLevel,
                GeneratedAt = DateTime.UtcNow

            };

            await _context.QuizQuestions.AddAsync(question);
            await _context.SaveChangesAsync(cancellationToken);

            return question.Id;
        }
    }
}
