using MediatR;
using Mudrik.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.QuizQuestion.Commands.UpdateQuizQuestion
{
    public class UpdateQuizQuestionCommandHandler(IAppDbContext _context) : IRequestHandler<UpdateQuizQuestionCommand, bool>
    {
        public async Task<bool> Handle(UpdateQuizQuestionCommand request, CancellationToken cancellationToken)
        {
            var quizQuestion = await _context.QuizQuestions.FindAsync(new object[] { request.Id }, cancellationToken);
            if (quizQuestion == null)
                return false;

            quizQuestion.StandardLessonId = request.StandardLessonId;
            quizQuestion.SubjectId = request.SubjectId;
            quizQuestion.QuestionText = request.QuestionText;
            quizQuestion.Format = request.Format;
            quizQuestion.OptionsJson = request.OptionsJson;
            quizQuestion.CorrectOptionId = request.CorrectOptionId;
            quizQuestion.ConceptTag = request.ConceptTag;
            quizQuestion.GradeLevel = request.GradeLevel;

            await _context.SaveChangesAsync(cancellationToken);

            return true;

        }
    }
}
