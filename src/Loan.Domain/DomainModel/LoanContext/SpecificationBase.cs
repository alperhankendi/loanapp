using Loan.Core;

namespace Loan.Domain
{
    public abstract class SpecificationBase : ISpecification<LoanApplication>
    {
        public abstract bool IsSatisfiedBy(LoanApplication candidate);
        public abstract string Message { get; }
    }
}