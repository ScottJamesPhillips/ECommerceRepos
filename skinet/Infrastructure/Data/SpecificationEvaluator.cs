using System.Linq;
using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    //37. Constraining this class, only for use with 'Entity' classes...
    public class SpecificationEvaluator<TEntity> where TEntity:BaseEntity 
    {
        //37. method taking in query and specfication
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            //37. variable to store query in
            var query = inputQuery;

            //37. then evaluate what is in specficiation...
            if(spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            //59.
            if(spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }

            if(spec.OrderByDescending != null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }

            //37. Includes is an aggregate, sums all includes statements
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }
    }
}