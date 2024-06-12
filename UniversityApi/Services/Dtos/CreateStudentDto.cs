using FluentValidation;
using Microsoft.AspNetCore.Http;
using UniversityApi.Data;

namespace UniversityApi.Dtos
{
    public class CreateStudentDto
    {
        public int GroupId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public IFormFile FormFile { get; set; }
    }

    public class CreateStudentDtoValidation : AbstractValidator<CreateStudentDto>
    {
        public CreateStudentDtoValidation()
        {
            RuleFor(x => x.GroupId).NotEmpty();
            RuleFor(x => x.FullName).NotEmpty().MinimumLength(5).MaximumLength(25);
            RuleFor(x=>x.Email).NotEmpty().MinimumLength(6).MaximumLength(25);
            RuleFor(x => x.BirthDate).NotEmpty().LessThan(p => DateTime.Now);
        }
    }
}

