﻿using System.Linq.Expressions;

namespace BLL;
public interface ISpecification<T>
{
    Expression<Func<T, bool>> Criteria { get; }
    Expression<Func<T, object>> OrderBy { get; }
    List<Expression<Func<T, object>>> Includes { get; }

    Expression<Func<T, object>> OrderByDescending { get; }
    int Skip { get; }
    int Take { get; }
    bool IsPagingEnabled { get; }

}
