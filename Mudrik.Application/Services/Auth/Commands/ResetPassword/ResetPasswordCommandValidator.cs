using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mudrik.Application.Services.Auth.Commands.ResetPassword
{
    public class ResetPasswordCommandValidator: AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("البريد الإلكتروني مطلوب.")
                .EmailAddress().WithMessage("صيغة البريد الإلكتروني غير صحيحة.");

            RuleFor(x => x.Token)
                .NotEmpty().WithMessage("رمز إعادة تعيين كلمة المرور مطلوب.");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("كلمة المرور الجديدة مطلوبة.")
                .MinimumLength(8).WithMessage("يجب أن تتكون كلمة المرور من 8 أحرف على الأقل.")
                .Matches("[A-Z]").WithMessage("يجب أن تحتوي كلمة المرور على حرف كبير واحد على الأقل.")
                .Matches("[0-9]").WithMessage("يجب أن تحتوي كلمة المرور على رقم واحد على الأقل.")
                .Matches("[^a-zA-Z0-9]").WithMessage("يجب أن تحتوي كلمة المرور على رمز خاص واحد على الأقل.");
        }
    }
}
