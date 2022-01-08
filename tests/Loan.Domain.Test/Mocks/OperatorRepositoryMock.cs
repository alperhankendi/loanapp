using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Loan.Domain.Test
{
    public class OperatorRepositoryMock : IOperatorRepository
    {
        private readonly ConcurrentDictionary<OperatorId,Operator> operators = new ConcurrentDictionary<OperatorId, Operator>();

        public OperatorRepositoryMock(IEnumerable<Operator> initalData)
        {
            foreach (var @operator in initalData)
            {
                this.operators[@operator.Id] = @operator;
            }
        }
        public void Add(Operator @operator)
        {
            operators[@operator.Id] = @operator;
        }

        public Operator WithLogin(Login login)
        {
            return this.operators.Values.FirstOrDefault(o=>o.Login == login);
        }
    }
}