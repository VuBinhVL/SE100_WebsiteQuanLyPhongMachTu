﻿using PhongMachTu.DataAccess.Infrastructure;
using PhongMachTu.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.DataAccess.Repositories
{

    public interface IThamSoRepository : IRepository<ThamSo>
    {
    }
    public class ThamSoRepository : RepositoryBase<ThamSo>, IThamSoRepository
    {
        public ThamSoRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}