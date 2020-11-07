using FluentValidation;
using Server.Exceptions;
using Server.Models.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Server.Validators
{
    public class ChangePasswordRequestModelValidator:AbstractValidator<ChangePasswordRequestModel>
    {
        public ChangePasswordRequestModelValidator()
        {
            RuleFor(customer => customer.NewPassword).Matches("[.]{6,}").OnFailure(x=>throw new PasswordNotMatchException("the entered password does not match the partern when changing password."));
            RuleFor(x => x.NewPassword).NotEqual(x=>x.OldPassword).OnFailure(x=>throw new NewPasswordEqualToOldException("The new password is equal to the older one when changing password."));
        }
    }
}
