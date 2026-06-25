using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.AzureOpenAI;
using Mudrik.Application.Interfaces;
using Mudrik.Domain.Models;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mudrik.Infrastructure.AI
{
    /// <summary>
    /// Semantic Kernel + GPT-4o implementation of IAdaptedLessonGenerator.
    /// Produces a JSON array of chunks whose format is driven by the LearnerAIProfile.
    /// Wrapped with a 30-second CancellationToken timeout to prevent hangs in dev.
    /// </summary>
    public class AdaptedLessonGenerator(Kernel kernel) : IAdaptedLessonGenerator
    {
        private const int TimeoutSeconds = 30;

        public async Task<string> GenerateAsync(
            StandardLesson lesson,
            LearnerAIProfile profile,
            CancellationToken cancellationToken)
        {
            using var timeoutCts = new CancellationTokenSource(TimeSpan.FromSeconds(TimeoutSeconds));
            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(
                cancellationToken, timeoutCts.Token);

            try
            {
                var systemPrompt = BuildSystemPrompt(profile);
                var userPrompt = BuildUserPrompt(lesson, profile);

                var chatFunction = kernel.CreateFunctionFromPrompt(
                    promptTemplate: userPrompt,
                    executionSettings: new AzureOpenAIPromptExecutionSettings
                    {
                        MaxTokens = 4000,
                        Temperature = 0.4,
                        TopP = 0.9
                    });

                var kernelArgs = new KernelArguments();
                kernelArgs["system"] = systemPrompt;

                var result = await kernel.InvokeAsync(
                    chatFunction, kernelArgs, linkedCts.Token);

                var rawOutput = result.GetValue<string>()
                    ?? throw new InvalidOperationException(
                        "GPT-4o returned an empty response.");

                return ExtractJsonArray(rawOutput);
            }
            catch (OperationCanceledException) when (timeoutCts.IsCancellationRequested)
            {
                throw new TimeoutException(
                    $"Adapted lesson generation timed out after {TimeoutSeconds} seconds. " +
                    "The GPT-4o call did not complete in time. Retry or check Azure OpenAI latency.");
            }
        }

        // -------------------------------------------------------------------
        // Prompt builders
        // -------------------------------------------------------------------

        private static string BuildSystemPrompt(LearnerAIProfile profile)
        {
            var sb = new StringBuilder();

            sb.AppendLine("You are an expert Egyptian primary school curriculum adapter.");
            sb.AppendLine("You convert raw lesson content into structured, child-friendly micro-chunks.");
            sb.AppendLine();
            sb.AppendLine("LEARNER PROFILE:");
            sb.AppendLine($"- Dyslexia Severity: {profile.DyslexiaSeverity}/100");
            sb.AppendLine($"- ADHD Severity: {profile.ADHDSeverity}/100");
            sb.AppendLine($"- Reading Level: {profile.ReadingLevel}");
            sb.AppendLine($"- Comprehension Score: {profile.ComprehensionScore}/100");
            sb.AppendLine($"- Attention Span: {profile.AttentionSpanMinutes} minutes");
            sb.AppendLine($"- Preferred Format: {profile.PreferredFormat}");
            sb.AppendLine($"- Audio Support Required: {profile.AudioSupportRequired}");
            sb.AppendLine($"- Chunk Size Preference: {profile.ChunkSizePref}");
            sb.AppendLine();
            sb.AppendLine("FORMAT SELECTION RULES (apply strictly):");

            // Audio takes highest priority
            if (profile.AudioSupportRequired)
            {
                sb.AppendLine("- Use 'audio-chunk' format for ALL chunks.");
                sb.AppendLine("- Populate audioScriptUrl with a placeholder path: '/audio/{lessonId}/{chunkOrder}.mp3'");
                sb.AppendLine("- Keep contentText short — it will be read aloud.");
            }
            // High dyslexia → visual
            else if (profile.DyslexiaSeverity >= 60)
            {
                sb.AppendLine("- Use 'visual-explainer' format for ALL chunks.");
                sb.AppendLine("- Populate illustrationUrl with: '/illustrations/{lessonId}/{chunkOrder}.png'");
                sb.AppendLine("- Use very short sentences. No more than 8 words per sentence.");
                sb.AppendLine("- Use large concept labels. Avoid dense text blocks.");
            }
            // High ADHD → story-based, short chunks
            else if (profile.ADHDSeverity >= 60)
            {
                sb.AppendLine("- Use 'story-chunk' format. Wrap every concept in a short narrative.");
                sb.AppendLine("- Each chunk must be engaging and end with a cliffhanger or question.");
                sb.AppendLine($"- estimatedDurationSeconds must NOT exceed {profile.AttentionSpanMinutes * 45}.");
            }
            // Baseline — standard
            else
            {
                sb.AppendLine("- Use 'standard-chunk' format.");
                sb.AppendLine("- Content should be clear, structured, and grade-appropriate.");
            }

            sb.AppendLine();
            sb.AppendLine("CHUNK SIZE RULES:");
            sb.AppendLine(profile.ChunkSizePref switch
            {
                "Short" => "- Each chunk: 60–90 seconds. Produce 5–7 chunks.",
                "Medium" => "- Each chunk: 90–150 seconds. Produce 4–5 chunks.",
                "Long" => "- Each chunk: 150–240 seconds. Produce 3–4 chunks.",
                _ => "- Each chunk: 90–150 seconds. Produce 4–5 chunks."
            });

            sb.AppendLine();
            sb.AppendLine("OUTPUT FORMAT (return ONLY valid JSON — no markdown, no explanation):");
            sb.AppendLine("""
[
  {
    "chunkOrder": 1,
    "format": "story-chunk",
    "title": "...",
    "contentText": "...",
    "audioScriptUrl": null,
    "illustrationUrl": null,
    "estimatedDurationSeconds": 90
  }
]
""");
            sb.AppendLine("The output must be a valid JSON array. No extra text before or after.");

            return sb.ToString();
        }

        private static string BuildUserPrompt(StandardLesson lesson, LearnerAIProfile profile)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"LESSON TITLE: {lesson.Title}");
            sb.AppendLine($"GRADE LEVEL: {lesson.GradeLevel}");
            sb.AppendLine($"LEARNING OBJECTIVES: {lesson.LearningObjectivesJson}");
            sb.AppendLine();
            sb.AppendLine("RAW CONTENT:");
            sb.AppendLine(lesson.RawContentText);
            sb.AppendLine();
            sb.AppendLine("Now adapt this lesson into micro-chunks following the system instructions exactly.");
            sb.AppendLine("Return ONLY the JSON array. No markdown. No preamble.");
            return sb.ToString();
        }

        /// <summary>
        /// Strips any accidental markdown fences GPT-4o might wrap around the JSON.
        /// </summary>
        private static string ExtractJsonArray(string raw)
        {
            var trimmed = raw.Trim();

            // Strip ```json ... ``` fences if present
            if (trimmed.StartsWith("```"))
            {
                var firstNewline = trimmed.IndexOf('\n');
                var lastFence = trimmed.LastIndexOf("```");
                if (firstNewline > 0 && lastFence > firstNewline)
                {
                    trimmed = trimmed[(firstNewline + 1)..lastFence].Trim();
                }
            }

            // Must start with '[' for a valid chunk array
            if (!trimmed.StartsWith("["))
                throw new InvalidOperationException(
                    "GPT-4o response did not contain a valid JSON array. " +
                    $"Raw response prefix: {trimmed[..Math.Min(200, trimmed.Length)]}");

            return trimmed;
        }
    }
}
