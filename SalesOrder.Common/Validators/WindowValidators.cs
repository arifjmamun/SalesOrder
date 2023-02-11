using FluentValidation;
using SalesOrder.Common.DTO.Element;
using SalesOrder.Common.DTO.Order;
using SalesOrder.Common.DTO.Window;

namespace SalesOrder.Common.Validators;

public class WindowCreateDtoValidator : AbstractValidator<WindowCreateDto>
{
    public WindowCreateDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Window name is required");
        RuleFor(x => x.QuantityOfWindows).GreaterThan(0).WithMessage("Quantity of windows must be greater than 0");
        RuleForEach(x => x.SubElements).SetValidator(new SubElementCreateDtoValidator());
    }
}

public class WindowUpdateDtoValidator : AbstractValidator<WindowUpdateDto>
{
    public WindowUpdateDtoValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithName("Window Id is required");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Window name is required");
        RuleFor(x => x.QuantityOfWindows).GreaterThan(0).WithMessage("Quantity of windows must be greater than 0");
    }
}