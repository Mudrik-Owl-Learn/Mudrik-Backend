using FluentValidation;
using Newtonsoft.Json.Linq;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Mudrik.Application.Services.QuizQuestion.Commands.AddQuizQuestion
{
    public class AddQuizQuestionCommandValidator : AbstractValidator<AddQuizQuestionCommand>
    {
        private static readonly string[] AllowedFormats =
        {
            "MultipleChoice",
            "TrueFalse",
            "FillInTheBlank"
        };

        public AddQuizQuestionCommandValidator()
        {
            RuleFor(x => x.StandardLessonId)
                .NotEmpty()
                .WithMessage("معرّف الدرس الأساسي مطلوب.");

            RuleFor(x => x.SubjectId)
                .NotEmpty()
                .WithMessage("معرّف المادة الدراسية مطلوب.");

            RuleFor(x => x.QuestionText)
                .NotEmpty()
                .WithMessage("نص السؤال مطلوب.")
                .MaximumLength(2000)
                .WithMessage("نص السؤال يجب ألا يتجاوز 2000 حرف.");

            RuleFor(x => x.Format)
                .NotEmpty()
                .WithMessage("صيغة السؤال مطلوبة.")
                .Must(format => !string.IsNullOrWhiteSpace(format) &&
                                 Array.Exists(AllowedFormats, f => f.Equals(format, StringComparison.OrdinalIgnoreCase)))
                .WithMessage($"صيغة السؤال غير صالحة. الصيغ المسموح بها هي: {string.Join(", ", AllowedFormats)}.");

            RuleFor(x => x.OptionsJson)
                .NotEmpty()
                .WithMessage("خيارات السؤال مطلوبة.")
                .Must(BeValidJson)
                .WithMessage("صيغة JSON الخاصة بالخيارات غير صالحة.")
                .Must(HaveAtLeastTwoOptions)
                .WithMessage("يجب أن يحتوي السؤال على خيارين على الأقل.")
                .When(x => BeValidJson(x.OptionsJson));

            RuleFor(x => x.CorrectOptionId)
                .NotEmpty()
                .WithMessage("معرّف الخيار الصحيح مطلوب.");

            RuleFor(x => x)
                .Must(x => OptionsContainCorrectOptionId(x.OptionsJson, x.CorrectOptionId))
                .WithMessage("معرّف الخيار الصحيح غير موجود ضمن خيارات السؤال.")
                .When(x => BeValidJson(x.OptionsJson) && !string.IsNullOrWhiteSpace(x.CorrectOptionId));

            RuleFor(x => x.ConceptTag)
                .NotEmpty()
                .WithMessage("وسم المفهوم (ConceptTag) مطلوب.")
                .MaximumLength(100)
                .WithMessage("وسم المفهوم يجب ألا يتجاوز 100 حرف.");

            RuleFor(x => x.GradeLevel)
                .InclusiveBetween(1, 12)
                .WithMessage("المستوى الدراسي يجب أن يكون بين 1 و 4.");
        }

        private bool BeValidJson(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return false;

            try
            {
                JToken.Parse(json);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool HaveAtLeastTwoOptions(string optionsJson)
        {
            try
            {
                var token = JToken.Parse(optionsJson);

                if (token is JArray array)
                    return array.Count >= 2;

                if (token is JObject obj)
                    return obj.Properties().Count() >= 2;

                return false;
            }
            catch
            {
                return false;
            }
        }

        private bool OptionsContainCorrectOptionId(string optionsJson, string correctOptionId)
        {
            try
            {
                var token = JToken.Parse(optionsJson);

                if (token is JArray array)
                {
                    foreach (var item in array)
                    {
                        // supports either: ["a","b","c"] or [{ "id": "a", "text": "..." }, ...]
                        var idValue = item.Type == JTokenType.Object
                            ? item["id"]?.ToString() ?? item["Id"]?.ToString()
                            : item.ToString();

                        if (string.Equals(idValue, correctOptionId, StringComparison.OrdinalIgnoreCase))
                            return true;
                    }
                    return false;
                }

                if (token is JObject obj)
                {
                    // supports: { "a": "...", "b": "..." } keyed by option id
                    return obj.Properties().Any(p =>
                        string.Equals(p.Name, correctOptionId, StringComparison.OrdinalIgnoreCase));
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}