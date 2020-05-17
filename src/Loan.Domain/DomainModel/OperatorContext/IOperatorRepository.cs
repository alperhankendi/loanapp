namespace Loan.Domain
{
    public interface IOperatorRepository
    {
        void Add(Operator @operator);
        Operator WithLogin(Login login);
    }
}