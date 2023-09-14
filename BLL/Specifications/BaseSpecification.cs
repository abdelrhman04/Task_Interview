﻿using System.Linq.Expressions;

namespace BLL;

public class BaseSpecification<T> : ISpecification<T>
{
    public BaseSpecification()
    {

    }
    public BaseSpecification(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }
    public Expression<Func<T, bool>> Criteria { get; set; }

    public List<Expression<Func<T, object>>> Includes { get; } = new();


    public Expression<Func<T, object>> OrderBy { get; private set; }

    public Expression<Func<T, object>> OrderByDescending { get; private set; }

    public int Skip { get; private set; }

    public int Take { get; private set; }

    public bool IsPagingEnabled { get; private set; } = false;

    protected void AddCriteria(Expression<Func<T, bool>> criteriaExpression)
    {
        Criteria = criteriaExpression;
    }
    protected void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }

    protected void AddOrderBy(Expression<Func<T, object>> expression=null)
    {
        if(expression!=null)
        {
            OrderBy = expression;

        }
       
    }

    protected void AddOrderByDescending(Expression<Func<T, object>> expression)
    {
        OrderByDescending = expression;
    }

    protected void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPagingEnabled = true;
    }
}

