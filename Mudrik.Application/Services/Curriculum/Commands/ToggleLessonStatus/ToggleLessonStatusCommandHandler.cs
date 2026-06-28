using MediatR;
using Mudrik.Application.Interfaces.CurriculumService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.Curriculum.Commands.ToggleLessonStatus;
public class ToggleLessonStatusCommandHandler(ICurriculumRepository repository)
       : IRequestHandler<ToggleLessonStatusCommand, bool>
{
    public async Task<bool> Handle(ToggleLessonStatusCommand request, CancellationToken cancellationToken)
    {
        var lesson = await repository.GetByIdAsync(request.LessonId, cancellationToken)
            ?? throw new KeyNotFoundException("الدرس غير موجود.");

        lesson.ToggleStatus();

        await repository.UpdateAsync(lesson, cancellationToken);

        return lesson.IsActive;
    }
}