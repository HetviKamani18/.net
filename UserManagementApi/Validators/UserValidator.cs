using FluentValidation;
using UserManagementApi.Models;

namespace UserManagementApi.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.Name)
                .NotEmpty().WithMessage("Name is required.");

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
        }
    }
} 