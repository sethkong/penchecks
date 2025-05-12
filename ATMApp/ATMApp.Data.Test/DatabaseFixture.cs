using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ATMApp.Data.Test
{
    public class DatabaseFixture
    {
        public DbContextOptions<DatabaseContext> DbOptions;
        public DatabaseFixture()
        {
            DbOptions = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "atmapp_dev")
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;
        }
    }
}
