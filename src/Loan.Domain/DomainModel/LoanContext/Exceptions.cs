using System;

namespace Loan.Domain
{
    public class BusinessException : Exception
    {
        public BusinessException(string message):base(message)
        {}
    }
    public class InvalidLoanApplicationStatusException : BusinessException
    {
        public InvalidLoanApplicationStatusException(string message):base(message)
        {
            
        }
    }
    
    public class NeedToScoreApplicationException : BusinessException
    {
        public NeedToScoreApplicationException(string message):base(message)
        {
            
        }
    }
    public class OperatorDoesNotHaveRequiredCompetenceLevelException : BusinessException
    {
        public OperatorDoesNotHaveRequiredCompetenceLevelException(string message):base(message)
        {
        }
    }
    public class LoanApplicationNotFound : BusinessException
    {
        public LoanApplicationNotFound(string message):base(message)
        {
        }
    }
    public class OperatorNotFound : BusinessException
    {
        public OperatorNotFound(string message):base(message)
        {
        }
    }
}