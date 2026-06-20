using Mudrik.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(ApplicationUser user, string role);
    }
}
