using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    //33. Repository 'GenericRepository' to implement IGenericRepository
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _context;
        //34. build constructor, make user of context
        public GenericRepository(StoreContext context)
        {
            _context = context;
        }

        //34.Command for generic repos replaces type with Set<T>(), sets to whatever type makes call...
        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        //34.Use of Set<T>, set type through runtime, but in instances of product list (ProductRepos file), there were 'Include()' statements
        //34.When generic repos, 'include('navigation properties)' change depending calling types, need to take this into account...cannot use 'Include()' here...
        //34.Entities with specific requirements can be stalled by generic repos, can fix this however...
        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        
         public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            //38. 'ApplyOurSpecification' method below, returns built up query including spec, then executed against db using 'FirstOrDefaultAsync()'
            return await ApplyOurSpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
             //38. 'ApplyOurSpecification' method below, returns built up query including spec, then executed against db using 'ToListAsync()'
            return await ApplyOurSpecification(spec).ToListAsync();
        }

        //65.
        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplyOurSpecification(spec).CountAsync();
        }

        //38.Build another method to allow us to apply our specifications
        private IQueryable<T> ApplyOurSpecification(ISpecification<T> spec)
        {
            //38. 'T' gets replaced with 'product' (or brand/type depending on caller type), it is then converted into a queryable
            //so passes into GetQuery method in SpecificationEvaluator, and inputQuery parameter will be of type 'product' as that is passed in...
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec); 
        }

        
    }
}