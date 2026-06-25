using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Mudrik.Application.Interfaces;
using Mudrik.Infrastructure.AI;
using Mudrik.Infrastructure.Settings;

namespace Mudrik.Infrastructure.Extensions
{
    public static class SemanticKernelExtensions
    {
        /// <summary>
        /// Registers Semantic Kernel with Azure OpenAI GPT-4o and wires up
        /// IAdaptedLessonGenerator -> AdaptedLessonGenerator.
        /// </summary>
        public static IServiceCollection AddSemanticKernel(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var settings = configuration
                .GetSection("AzureOpenAI")
                .Get<AzureOpenAISettings>()
                ?? throw new InvalidOperationException(
                    "AzureOpenAI configuration section is missing from appsettings.json.");

            services.AddSingleton(settings);

            // Build and register the Semantic Kernel as a singleton.
            // Kernel is thread-safe and expensive to construct — singleton is correct.
            services.AddSingleton(sp =>
            {
                var builder = Kernel.CreateBuilder();

                builder.AddAzureOpenAIChatCompletion(
                    deploymentName: settings.DeploymentName,
                    endpoint: settings.Endpoint,
                    apiKey: settings.ApiKey);

                return builder.Build();
            });

            // AdaptedLessonGenerator is Scoped (not Singleton) because it
            // may take per-request cancellation tokens and request-scoped state.
            services.AddScoped<IAdaptedLessonGenerator, AdaptedLessonGenerator>();

            return services;
        }
    }
}
