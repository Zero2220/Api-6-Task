using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityApi.Dtos;

namespace Services.Services.Interfaces
{
    public interface IStudentService
    {
        int Create(CreateStudentDto createDto);
        List<GetStudentDto> GetAll();
        GetStudentDto GetById(int id);
        int Edit(int id, EditStudentDto editDto);
        int Delete(int id);
    }
}
