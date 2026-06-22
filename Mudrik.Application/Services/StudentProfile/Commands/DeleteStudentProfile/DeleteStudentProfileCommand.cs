using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.StudentProfile.Commands.DeleteStudentProfile
{
    public record DeleteStudentProfileCommand(Guid id) : IRequest<int>;
}
