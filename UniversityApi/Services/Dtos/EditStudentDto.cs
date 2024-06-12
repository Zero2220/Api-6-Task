using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace UniversityApi.Dtos
{
    public class EditStudentDto
    {
        public int GroupId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }

        public IFormFile FormFile { get; set; }
    }

    public class EditStudentDtoValidation : AbstractValidator<EditStudentDto>
    {
        public EditStudentDtoValidation()
        {
            RuleFor(x => x.GroupId).NotEmpty();
            RuleFor(x => x.FullName).NotEmpty().MinimumLength(5).MaximumLength(25);
            RuleFor(x => x.Email).NotEmpty().MinimumLength(6).MaximumLength(25);
            RuleFor(x => x.BirthDate).NotEmpty().LessThan(p => DateTime.Now);


            RuleFor(x => x).Custom((f, c) =>
            {
                if (f.FormFile != null && f.FormFile.Length > 2 * 1024 * 1024)
                {
                    c.AddFailure("File", "File must be less or equal than 2MB");
                }


            });
        }
    }
}
