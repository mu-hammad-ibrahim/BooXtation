using Microsoft.EntityFrameworkCore;
using BookXtation.DAL.Models.Data;
using BooXtation.BLL.Repositories.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookXtation.DAL
{
    public static class SpecificationEvaluator<T> where T : class
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec) 
        {
            var query = inputQuery; 

            if (spec.Criteria is not null)
                query = query.Where(spec.Criteria);
            

            if (spec.OrderBy is not null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            if (spec.OrderByDescending is not null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }


            if (spec.IsPaginationEnabled)
                query = query.Skip(spec.Skip).Take(spec.Take);

            query = spec.Includes.Aggregate(query, (currentQuery, IncludeExpression) =>

            (currentQuery.Include(IncludeExpression)));

            return query;

        }


    }
}
