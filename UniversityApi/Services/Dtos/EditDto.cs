using FluentValidation;

namespace UniversityApi.Dtos
{
    public class EditDto
    {
        public string No { get; set; }
        public byte Limit { get; set; }
    }

    public class GroupEditDtoValidator : AbstractValidator<EditDto>
    {
        public GroupEditDtoValidator()
        {
            RuleFor(x => x.No).NotEmpty().MaximumLength(5).MinimumLength(4);
            RuleFor(x => (int)x.Limit).NotNull().InclusiveBetween(5, 18);
        }
    }
}
