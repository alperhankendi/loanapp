using System;
using Loan.Core;

namespace Loan.Domain
{
    public class OperatorId : IdentityBase<Guid>
    {
        public OperatorId(Guid id) : base(id)
        {
        }
    }
}