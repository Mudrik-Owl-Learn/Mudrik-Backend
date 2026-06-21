using Google.Apis.Auth;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.SqlServer.Server;
using Mudrik.Application.Interfaces;
using Mudrik.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Infrastructure.Services
{
    public class GoogleAuthService(IOptions<GoogleAuthSettings> googleOptions,ILogger<GoogleAuthService> logger)
        : IGoogleAuthService
    {
        private readonly GoogleAuthSettings _settings = googleOptions.Value;

        public async Task<GoogleUserInfo?> ValidateGoogleTokenAsync(string idToken)
        {
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = [_settings.ClientId]
                };

                var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);

                return new GoogleUserInfo(payload.Email, payload.Name ?? payload.Email);
            }
            catch (InvalidJwtException ex)
            {
                logger.LogWarning(ex, "Invalid Google ID token received.");
                return null;
            }
        }
    }
}
