﻿using PhongMachTu.DataAccess.Infrastructure;
using PhongMachTu.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.DataAccess.Repositories
{
	public interface IBenhLyRepository:IRepository<BenhLy>
	{
	}
	public class BenhLyRepository : RepositoryBase<BenhLy>, IBenhLyRepository
	{
		public BenhLyRepository(IDbFactory dbFactory) : base(dbFactory)
		{
		}
	}
}
