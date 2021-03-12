using System;
using Loan.Core;
using Newtonsoft.Json;

namespace Loan.Domain
{
    public static class Events
    {
        public static class V1
        {
            public class LoanApplicationSubmitted : DomainEvent
            {
                public Guid LoanApplicationId { get;}   
                public string   Email { get; set; }
                
                public LoanApplicationSubmitted(LoanApplication loanApplication):this(loanApplication.Id.Id)
                {
                    Email = loanApplication.Customer.Email.MailValue;
                }
                [JsonConstructor]
                protected LoanApplicationSubmitted(Guid id)
                {
                    LoanApplicationId = id;
                } 
            }
            public class LoanApplicationAccepted : DomainEvent
            {
                public Guid LoanApplicationId { get;}
                
                [JsonConstructor]
                public LoanApplicationAccepted(LoanApplication loanApplication):this(loanApplication.Id.Id)
                {
                }
                [JsonConstructor]
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
                [JsonConstructor]
                protected LoanApplicationRejected(Guid id)
                {
                    LoanApplicationId = id;
                }
            }
        }
    }
}