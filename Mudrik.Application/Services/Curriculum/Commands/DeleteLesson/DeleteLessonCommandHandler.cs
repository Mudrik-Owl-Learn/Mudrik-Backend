using MediatR;
using Mudrik.Application.Interfaces.CurriculumService;
using Mudrik.Application.Services.Curriculum.Commands.CreateLesson;
using Mudrik.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.Curriculum.Commands.DeleteLesson;

public class DeleteLessonCommandHandler(ICurriculumRepository repository)
        : IRequestHandler<DeleteLessonCommand>
{
    public async Task Handle(DeleteLessonCommand request, CancellationToken cancellationToken)
    {
        var lesson = await repository.GetByIdAsync(request.LessonId, cancellationToken)
            ?? throw new KeyNotFoundException("الدرس غير موجود.");

        lesson.Deactivate();

        await repository.UpdateAsync(lesson, cancellationToken);
    }
}