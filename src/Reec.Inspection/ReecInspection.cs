using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Reec.Inspection
{
    public class ReecInspection<TDbContext> where TDbContext : DbContext
    {
        private readonly TDbContext _dbContext;

        public ReecInspection(TDbContext dbContext)
        {
            this._dbContext = dbContext;
        }


        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

    }

}
