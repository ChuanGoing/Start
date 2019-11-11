using Microsoft.EntityFrameworkCore;

namespace ChuanGoing.Storage.EFCore
{
    public class EFCoreDbContext : DbContext
    {
        public EFCoreDbContext(DbContextOptions<EFCoreDbContext> options)
            : base(options)
        {
        }
    }
}
