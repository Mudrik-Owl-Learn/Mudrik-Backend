using MediatR;
using Microsoft.EntityFrameworkCore;
using Mudrik.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.StudentProfile.Commands.DeleteStudentProfile
{
    public class DeleteStudentProfileCommandHandler(IAppDbContext _context) : IRequestHandler<DeleteStudentProfileCommand, int>
    {

        async Task<int> IRequestHandler<DeleteStudentProfileCommand, int>.Handle(DeleteStudentProfileCommand request, CancellationToken cancellationToken)
        {
            var profile = _context.StudentProfiles.Find(request.id);
            if (profile == null)
                return 0;
            var result = _context.StudentProfiles.Remove(profile);
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
