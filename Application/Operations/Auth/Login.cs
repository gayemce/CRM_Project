using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application;
public sealed record LoginCommand(
    string UserNameOrEmail,
    string Password) :
    IRequest<ApplicationUser>;

public sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(c => c.UserNameOrEmail).NotEmpty();
        RuleFor(c => c.Password).NotEmpty();
    }
}

public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, ApplicationUser>
{
    private readonly IValidator<LoginCommand> _validator;
    private readonly UserManager<ApplicationUser> _userManager;

    public LoginCommandHandler(
        IValidator<LoginCommand> validator,
        UserManager<ApplicationUser> userManager)
    {
        _validator = validator;
        _userManager = userManager;
    }

    public async Task<ApplicationUser> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(command, cancellationToken);

        var applicationUser = await _userManager.Users
            .FirstOrDefaultAsync(p => p.UserName == command.UserNameOrEmail || p.Email == command.UserNameOrEmail, cancellationToken);

        if (applicationUser == null)
        {
            throw new Exception("Kullanıcı bulunamadı.");
        }

        var checkPassword = await _userManager.CheckPasswordAsync(applicationUser, command.Password);

        if (!checkPassword)
        {
            throw new Exception("Yanlış şifre.");
        }

        return applicationUser;
    }
}

