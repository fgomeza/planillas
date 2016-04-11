﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Entities;
using Repository.Context;

namespace Repository.Repositories.Classes
{
    public class ExtraRepository: Repository<ExtraEntity>
    {
        public ExtraRepository(AppContext context) : base(context){}

        public IEnumerable<ExtraEntity> selectExtrasByEmployee(long employeeId)
        {
            var extras = _context.Set<ExtraEntity>().Where((e)=>e.employeeId==employeeId);
            return extras.ToList();
        }
    }
}