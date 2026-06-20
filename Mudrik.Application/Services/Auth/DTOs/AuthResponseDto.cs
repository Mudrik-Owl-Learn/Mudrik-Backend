using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.Auth.DTOs
{
    public record AuthResponseDto(
        string Token,
        string Role
    );
}
