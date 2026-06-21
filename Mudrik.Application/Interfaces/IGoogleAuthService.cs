using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Interfaces
{
    public record GoogleUserInfo(string Email, string FullName);

    public interface IGoogleAuthService
    {
        Task<GoogleUserInfo?> ValidateGoogleTokenAsync(string idToken);
    }
}
