using BigOn.Infrastructure.Localize.Colors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.ColorsModule.Commands.ColorAddCommand
{
    internal class ColorAddRequestValidator : AbstractValidator<ColorAddRequest>
    {
        public ColorAddRequestValidator()
        {
            RuleFor(m => m.Name)
                .NotNull().WithMessage(ColorsResource.COLOR_NAME_CANT_BE_NULL)
                .NotEmpty().WithMessage(ColorsResource.COLOR_NAME_CANT_BE_EMPTY);
            RuleFor(m => m.HexCode)
                .NotNull().WithMessage(ColorsResource.COLOR_HEXCODE_CANT_BE_NULL)
                .NotEmpty().WithMessage(ColorsResource.COLOR_HEXCODE_CANT_BE_EMPTY)
                .MaximumLength(7).WithMessage(ColorsResource.COLOR_HEXCODE_MAX_CHARACTERS_7)
                .MinimumLength(7).WithMessage(ColorsResource.COLOR_HEXCODE_MIN_CHARACTERS_7);
        }
    }

}
