using System.Linq.Expressions;
using IMS.CoreBusiness.Exceptions;
using IMS.CoreBusiness.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IMS.Plugins.EFCoreSqlServer.Base
{
    public abstract class BaseEFCoreRepository<T> where T : class, ICoreBusinessEntity
    {
        protected readonly IDbContextFactory<IMSContext> contextFactory;
        public delegate void EntityUpdaterDelegate(ref T existingEntity, T changedEntity);

        private readonly EntityUpdaterDelegate entityUpdater;
        private readonly Expression<Func<T, string, bool>> searchPredicate;

        public BaseEFCoreRepository(IDbContextFactory<IMSContext> contextFactory,
            EntityUpdaterDelegate entityUpdater,
            Expression<Func<T, string, bool>> searchPredicate)
        {
            this.contextFactory = contextFactory;
            this.entityUpdater = entityUpdater;
            this.searchPredicate = searchPredicate;
        }

        public async Task AddAsync(T entity)
        {
            using var context = await contextFactory.CreateDbContextAsync();
            DoAdd(entity, context);
            await context.SaveChangesAsync();
        }

        protected virtual void DoAdd(T entity, IMSContext context)
        {
            context.Set<T>().Add(entity);
        }

        public async Task DeleteAsync(int Id)
        {
            var context = await contextFactory.CreateDbContextAsync();
            var entity = await DoGetByIdAsync(context, Id);
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetByNameAsync(string searchString)
        {
            var context = await contextFactory.CreateDbContextAsync();
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                Expression<Func<T, bool>> newPredicate = e => searchPredicate.Compile()(e, searchString);
                return await context.Set<T>().Where(newPredicate).ToListAsync();
            }
            else
            {
                return await context.Set<T>().ToListAsync();
            }
        }

        public Task<T> GetByIdAsync(int Id)
        {
            using var context = contextFactory.CreateDbContext();
            return DoGetByIdAsync(context, Id);
        }

        public async Task UpdateAsync(T entity)
        {
            using var context = await contextFactory.CreateDbContextAsync();
            var existingEntity = await DoGetByIdAsync(context, entity.Id);
            DoUpdate(existingEntity, entity, context);
            await context.SaveChangesAsync();
        }

        protected virtual void DoUpdate(T existingEntity, T changedEntity, IMSContext context)
        {
            entityUpdater.Invoke(ref existingEntity, changedEntity);
        }

        protected virtual async Task<T> DoGetByIdAsync(IMSContext context, int Id)
        {
            var inventory = await context.Set<T>().FirstOrDefaultAsync(x => x.Id == Id)
                ?? throw new NotFoundException(typeof(T), Id.ToString());
            return inventory;
        }
    }
}