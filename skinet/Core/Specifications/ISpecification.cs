using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Specifications
{
    //36. going to take type
    public interface ISpecification<T>
    {
         //36. create generic methods
         Expression<Func<T, bool>> Criteria {get;}
         List<Expression<Func<T, object>>> Includes {get;}
    }
}