﻿using BlogDemo.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogDemo.DB.DataBase
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyContext _myContext;

        public UnitOfWork(MyContext myContext)
        {
            _myContext = myContext;
        }
         
        public async Task<bool> SaveAsync()
        {
            return await _myContext.SaveChangesAsync() > 0;
        }
    }
}
