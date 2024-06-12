using Data.Repositories.İnterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityApi.Data;
using UniversityApp.Data.Repositories.Implmentations;

namespace Data.Repositories.İmplementations
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        private readonly UniDatabase _context;

        public StudentRepository(UniDatabase context) : base(context)
        {
            _context = context;
        }



    }
}
