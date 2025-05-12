using FluentValidation;

namespace ATMApp.Domain
{
    public class EntityKindValidator : AbstractValidator<EntityKind>
    {
        public EntityKindValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Type name is required");
            RuleFor(x => x.Code).NotEmpty().WithMessage("Type code is required");
        }
    }
}
