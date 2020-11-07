using FluentValidation;
using Server.Exceptions;
using Server.Models.VO;
using Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Validators
{
    public class RegisterRequestModelValidator : AbstractValidator<RegisterRequestModel>
    {
        private IDatabaseService _databaseService;
        public RegisterRequestModelValidator()
        {
            RuleFor(customer => customer.Password).Matches("[.]{6,}").OnFailure(x => throw new PasswordNotMatchException("the entered password does not match the partern when registering."));
            RuleFor(x => _databaseService.Users.FirstOrDefault(t => t.Username == x.Username) == null).Equal(true).OnFailure(x => throw new UsernameDuplicatedException("Username duplicated."));
        }
    }
}
