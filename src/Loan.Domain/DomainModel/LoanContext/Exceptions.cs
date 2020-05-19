using System;

namespace Loan.Domain
{
    public class InvalidLoanApplicationStatusException : Exception
    {
        public InvalidLoanApplicationStatusException(string message):base(message)
        {
            
        }
    }
    
    public class NeedToScoreApplicationException : Exception
    {
        public NeedToScoreApplicationException(string message):base(message)
        {
            
        }
    }
    public class OperatorDoesNotHaveRequiredCompetenceLevelException : Exception
    {
        public OperatorDoesNotHaveRequiredCompetenceLevelException(string message):base(message)
        {
        }
    }
    public class LoanApplicationNotFound : Exception
    {
        public LoanApplicationNotFound(string message):base(message)
        {
        }
    }
    public class OperatorNotFound : Exception
    {
        public OperatorNotFound(string message):base(message)
        {
        }
    }
    
}