using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Specifications
{
    //36. going to take type
    public interface ISpecification<T>
    {
        //36. create generic methods
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }

        //59. 
        Expression<Func<T, object>> OrderBy { get; }

        Expression<Func<T, object>> OrderByDescending { get; }

        int Take {get;}

        int Skip {get;}

        bool IsPagingEnabled {get;}

    }
}