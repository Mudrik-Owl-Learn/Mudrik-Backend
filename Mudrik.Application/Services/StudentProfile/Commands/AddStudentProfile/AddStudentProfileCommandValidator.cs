using FluentValidation;
using Mudrik.Domain.Enums;
using System.Text.Json;

namespace Mudrik.Application.Services.StudentProfile.Commands.AddStudentProfile;

public class AddStudentProfileCommandValidator : AbstractValidator<AddStudentProfileCommand>
{
    private static readonly string[] ValidLearningStyles = { "video", "audio", "play", "reading" };
    private static readonly string[] ValidPersonalityTags = { "curious", "creative", "adventurous", "calm", "competitive" };

    public AddStudentProfileCommandValidator()
    {
        RuleFor(x => x.ParentProfileId)
            .NotEmpty()
            .WithMessage("يجب ادخال رقم هوية الوالد صحيح");

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("الاسم الأول مطلوب.")
            .MaximumLength(100)
            .WithMessage("الاسم الأول لا يتجاوز 100 حرف.")
            .Matches(@"^[\p{L}\p{M}\s\-'\u0640]+$")
            .WithMessage("الاسم الأول يحتوي على أحرف غير صالحة.");

        RuleFor(x => x.Age)
            .InclusiveBetween(4, 12)
            .WithMessage("العمر يجب أن يكون بين 4 و 12 سنة.");

        RuleFor(x => x.Gender)
            .Must(g => Enum.IsDefined(typeof(Gender), g))
            .WithMessage($"الجنس يجب أن يكون أحد القيم التالية: {string.Join(", ", Enum.GetNames(typeof(Gender)))}.");

        RuleFor(x => x.GradeLevel)
            .InclusiveBetween(1, 4)
            .WithMessage("الصف الدراسي يجب أن يكون بين 1 و 4.");

        // TODO: change to GreaterThan(0) once AvatarId becomes int FK
        RuleFor(x => x.AvatarId)
            .NotEmpty()
            .WithMessage("يجب اختيار صورة رمزية.")
            .MaximumLength(50)
            .WithMessage("معرف الصورة الرمزية لا يتجاوز 50 حرفاً.");

        // TODO: replace with collection validation once InterestsJson becomes junction table
        RuleFor(x => x.InterestsJson)
            .NotEmpty()
            .WithMessage("يجب اختيار اهتمام واحد على الأقل.")
            .Must(BeValidJsonArray)
            .WithMessage("صيغة الاهتمامات غير صالحة.")
            .Must(HaveAtLeastOneInterest)
            .WithMessage("يجب اختيار اهتمام واحد على الأقل.")
            .Must(NotExceedMaxInterests)
            .WithMessage("لا يمكن اختيار أكثر من 10 اهتمامات.");

        RuleFor(x => x.LearningStylePref)
            .NotEmpty()
            .WithMessage("أسلوب التعلم المفضل مطلوب.")
            .Must(v => ValidLearningStyles.Contains(v?.ToLower()))
            .WithMessage($"أسلوب التعلم يجب أن يكون أحد: {string.Join(", ", ValidLearningStyles)}.");

        RuleFor(x => x.PersonalityTag)
            .NotEmpty()
            .WithMessage("وسم الشخصية مطلوب.")
            .Must(v => ValidPersonalityTags.Contains(v?.ToLower()))
            .WithMessage($"وسم الشخصية يجب أن يكون أحد: {string.Join(", ", ValidPersonalityTags)}.");
    }

    private static bool BeValidJsonArray(string json)
    {
        if (string.IsNullOrWhiteSpace(json)) return false;
        try
        {
            using var doc = JsonDocument.Parse(json);
            return doc.RootElement.ValueKind == JsonValueKind.Array;
        }
        catch { return false; }
    }

    private static bool HaveAtLeastOneInterest(string json)
    {
        try
        {
            using var doc = JsonDocument.Parse(json);
            return doc.RootElement.GetArrayLength() > 0;
        }
        catch { return false; }
    }

    private static bool NotExceedMaxInterests(string json)
    {
        try
        {
            using var doc = JsonDocument.Parse(json);
            return doc.RootElement.GetArrayLength() <= 10;
        }
        catch { return false; }
    }
}