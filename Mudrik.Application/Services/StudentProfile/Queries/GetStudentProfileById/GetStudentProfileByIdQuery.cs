using MediatR;
using Mudrik.Application.Services.StudentProfile.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.StudentProfile.Queries.GetStudentProfileById
{
    public record GetStudentProfileByIdQuery(Guid id) : IRequest<GetStudentDTO>;
}
