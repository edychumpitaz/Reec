using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Reec.Inspection
{
    public class InspectionDbContext : DbContext
    {
        public InspectionDbContext([NotNull] DbContextOptions options) : base(options)
        {
        }

        protected InspectionDbContext()
        {
        }
    }


}
