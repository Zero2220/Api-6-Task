using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityApi.Dtos;

namespace Services.Services.Interfaces
{
    public interface IGroupService
    {
        int Create(CreateDto createDto);
        List<GroupGetDto> GetAll(string? search);
        GroupGetDto GetById(int id);
        int Edit(int id ,EditDto editDto);
        int Delete(int id);


    }
}
