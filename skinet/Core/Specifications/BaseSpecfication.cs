using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Specifications
{
    //36. OVERALL - the addition of this class and the ISpecification interface is to effectively replace the 'Include' statements in ProductRepository class, used to get product list or item 

    public class BaseSpecfication<T> : ISpecification<T>
    {
        //36. generate constructor
        public BaseSpecfication(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public BaseSpecfication()
        {

        }

        //36. Will implement ISpecification Interface
        public Expression<Func<T, bool>> Criteria {get;}

        //36. Includes will contain list of 'includes()' statements we can use to list async methods, default as an empty list
        public List<Expression<Func<T, object>>> Includes {get;} = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy {get; private set;}

        public Expression<Func<T, object>> OrderByDescending {get; private set;}

        public int Take {get; private set;}

        public int Skip {get; private set;}

        public bool IsPagingEnabled {get; private set;}

        //36. Create a method to add include statements to our include list
        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        //59.
        public void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        public void AddOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
        {
            OrderByDescending = orderByDescendingExpression;
        }

        public void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }
    }
}