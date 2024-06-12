using Data.Repositories.İnterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UniversityApi.Data;
using UniversityApp.Data.Repositories.Implmentations;

namespace Data.Repositories.Implementations
{
    public class GroupRepository : Repository<Group>, IGroupRepository
    {
        private readonly UniDatabase _context;

        public GroupRepository(UniDatabase context) : base(context)
        {
            _context = context;
        }

        
    }
}
