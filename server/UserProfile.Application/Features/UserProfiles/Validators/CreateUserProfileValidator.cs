using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProfile.Application.Features.UserProfiles.Commands.CreateUserProfile;

namespace UserProfile.Application.Features.UserProfiles.Validators;

public class CreateUserProfileValidator : AbstractValidator<CreateUserProfileCommand>
{
    public CreateUserProfileValidator()
    {
        RuleFor(x => x.FullName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Age).InclusiveBetween(0, 120);
        RuleFor(x => x.Nationality).NotEmpty();
        RuleFor(x => x.FavoriteCountries).NotEmpty();
        RuleFor(x => x.VisitedCountries).NotEmpty();
        RuleFor(x => x.Bio).MaximumLength(1000);
        // აქ შეგიძლია დაამატო სურათის ზომის და გაფართოების ვალიდაციაც
    }
}
