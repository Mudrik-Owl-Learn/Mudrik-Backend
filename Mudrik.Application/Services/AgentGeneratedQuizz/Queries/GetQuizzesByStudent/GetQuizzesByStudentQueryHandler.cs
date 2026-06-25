using MediatR;
using Microsoft.EntityFrameworkCore;
using Mudrik.Application.Interfaces;
using Mudrik.Application.Services.AgentGeneratedQuizz.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.AgentGeneratedQuizz.Queries.GetQuizzesByStudent
{
    public class GetQuizzesByStudentQueryHandler(IAppDbContext _context) : IRequestHandler<GetQuizzesByStudentQuery, List<GenerateQuizDTO>>
    {
        public async Task<List<GenerateQuizDTO>> Handle(GetQuizzesByStudentQuery request, CancellationToken cancellationToken)
        {
            var quizzes = await _context.AgentGeneratedQuizzes.Where(a => a.StudentProfileId == request.studentId).ToListAsync(cancellationToken);

            if (!quizzes.Any())
                return null;

            List<GenerateQuizDTO> quizDTOs = new List<GenerateQuizDTO>();

            foreach (var quiz in quizzes)
            {
                var quizDTO = new GenerateQuizDTO()
                {
                    id = quiz.Id,
                    questions = await _context.QuizQuestions.Where(a => a.AgentGeneratedQuizQuestions.Any(q => q.AgentGeneratedQuizId == quiz.Id)).Select(s => new QuizQuestionWithoutCorrectOptionId()
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
                quizDTOs.Add(quizDTO);
            }


            //var quizDTO = new GenerateQuizDTO()
            //{
            //    id = request.studentId,

            //    questions = await _context.QuizQuestions.Where(a => a.AgentGeneratedQuizQuestions.Any(q => q.AgentGeneratedQuizId == request.studentId)).Select(s => new QuizQuestionWithoutCorrectOptionId()
            //    {
            //        Id = s.Id,
            //        StandardLessonId = s.StandardLessonId,
            //        SubjectId = s.SubjectId,
            //        QuestionText = s.QuestionText,
            //        Format = s.Format,
            //        OptionsJson = s.OptionsJson,
            //        ConceptTag = s.ConceptTag,
            //        GradeLevel = s.GradeLevel,
            //        GeneratedAt = s.GeneratedAt
            //    }).ToListAsync(cancellationToken)
            //};

            return quizDTOs;
        }
    }
}
