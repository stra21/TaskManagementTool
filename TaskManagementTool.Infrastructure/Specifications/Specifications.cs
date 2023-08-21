using System.Linq.Expressions;
using TaskManagementTool.Core.Markers;
namespace TaskManagementTool.Infrastructure.Specifications;

public abstract class Specification<T> : ISpecification<T>
{
    public int PageSize { get; set; } = 10;
    public int PageIndex { get; set; } = 1;
    public abstract Expression<Func<T, bool>> ToExpression();
    public ISpecification<T> And(ISpecification<T> specification)
    {
        return new AndSpecification<T>(this, specification);
    }

    public ISpecification<T> Or(ISpecification<T> specification)
    {
        return new OrSpecification<T>(this, specification);
    }

    public bool ValidateSpec(T entity)
    {
        Func<T, bool> predicate = ToExpression().Compile();
        return predicate(entity);
    }
}
public class AndSpecification<T> : Specification<T>
{
    private readonly ISpecification<T> _left;
    private readonly ISpecification<T> _right;

    public AndSpecification(ISpecification<T> left, ISpecification<T> right)
    {
        _right = right;
        _left = left;
    }

    public override Expression<Func<T, bool>> ToExpression()
    {
        Expression<Func<T, bool>> leftExpression = _left.ToExpression();
        Expression<Func<T, bool>> rightExpression = _right.ToExpression();

        BinaryExpression andExpression = Expression.AndAlso(
            leftExpression.Body, rightExpression.Body);

        return Expression.Lambda<Func<T, bool>>(
            andExpression, leftExpression.Parameters.Single());
    }
}
public class OrSpecification<T> : Specification<T>
{
    private readonly ISpecification<T> _left;
    private readonly ISpecification<T> _right;

    public OrSpecification(ISpecification<T> left, ISpecification<T> right)
    {
        _right = right;
        _left = left;
    }

    public override Expression<Func<T, bool>> ToExpression()
    {
        Expression<Func<T, bool>> leftExpression = _left.ToExpression();
        Expression<Func<T, bool>> rightExpression = _right.ToExpression();

        BinaryExpression orExpression = Expression.Or(
            leftExpression.Body, rightExpression.Body);

        return Expression.Lambda<Func<T, bool>>(
            orExpression, leftExpression.Parameters.Single());
    }
}