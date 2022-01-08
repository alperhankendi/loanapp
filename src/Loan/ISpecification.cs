namespace Loan.Core
{
    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T candidate);
    }
}