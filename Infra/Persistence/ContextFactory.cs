using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class ContextFactory : DesignTimeDbContextFactoryBase<Context>
    {
        protected override Context CreateNewInstance(DbContextOptions<Context> options)
        {
            return new Context(options);
        }
    }
}
