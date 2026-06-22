using MediatR;
using Mudrik.Application.Services.StudentProfile.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.StudentProfile.Queries.GetAllStudentsProfiles
{
    public record GetAllStudentsProfilesQuery : IRequest<List<GetStudentDTO>>;
}
