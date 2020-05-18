using System;
using Loan.Core;

namespace Loan.Domain
{
    public static class Events
    {
        public static class V1
        {
            public class LoanApplicationAccepted : DomainEvent
            {
                public Guid LoanApplicationId { get;}
                public LoanApplicationAccepted(LoanApplication loanApplication):this(loanApplication.Id.Id)
                {
                }
                protected LoanApplicationAccepted(Guid id)
                {
                    LoanApplicationId = id;
                }
            }
            public class LoanApplicationRejected : DomainEvent
            {
                public Guid LoanApplicationId { get;}
                public LoanApplicationRejected(LoanApplication loanApplication):this(loanApplication.Id.Id)
                {
                }
                protected LoanApplicationRejected(Guid id)
                {
                    LoanApplicationId = id;
                }
            }
        }
    }
}