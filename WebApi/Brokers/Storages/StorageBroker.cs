using Microsoft.EntityFrameworkCore;

namespace WebApi.Brokers.Storages
{
    public partial class StorageBroker:DbContext,IStorageBroker
    {
        private readonly IConfiguration configuration;

        public StorageBroker(IConfiguration configuration)
        {
            this.configuration = configuration;
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
         optionsBuilder.UseSqlite("Data Source=storage.db");
        }
        private async ValueTask<T> InsertAsync<T>(T @object)
        {
            Entry(@object).State = EntityState.Added;
            await SaveChangesAsync();
            DetachEntity(@object);

            return @object;
        }

        private async ValueTask<IQueryable<T>> SelectAllAsync<T>() where T : class =>
            Set<T>();

        private async ValueTask<T> SelectAsync<T>(params object[] @objectIds) where T : class =>
            await FindAsync<T>(@objectIds);

        private async ValueTask<T> UpdateAsync<T>(T @object)
        {
            Entry(@object).State = EntityState.Modified;
            await SaveChangesAsync();
            DetachEntity(@object);

            return @object;
        }

        private async ValueTask<T> DeleteAsync<T>(T @object)
        {
            Entry(@object).State = EntityState.Deleted;
            await SaveChangesAsync();
            DetachEntity(@object);

            return @object;
        }

        private void DetachEntity<T>(T @object)
        {
            Entry(@object).State = EntityState.Detached;
        }
    }
}
