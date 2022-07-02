using FluentValidation;
using BlogAPI.Entities;

namespace BlogAPI.Models.Validators
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserValidator(BlogDbContext dbContext)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password).MinimumLength(6);

            RuleFor(x => x.ConfirmPassword).Equal(e => e.Password);

            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    // context odpowiada za wyswietlenie bledu walidacji
                    // value pobiera wartosc z linii 18
                    var emailInUse = dbContext.Users.Any(u => u.Email == value);
                    if(emailInUse)
                    {
                        context.AddFailure("Email", "That email is already taken.");
                    }
                });
        }

    }
}
