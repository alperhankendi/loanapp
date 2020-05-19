using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Loan.Domain.Application;
using Npgsql;

namespace Loan.Domain.ReadModel
{
    public class LoanApplicationFinder
    {
        private readonly string connString;

        public LoanApplicationFinder(string connString)
        {
            this.connString = connString;
        }

        public List<Contract.V1.LoanApplicationSummary> GetLoanApplicationSummary()
        {
            using var cn = new NpgsqlConnection(connString);
            cn.Open();
            
            return cn.Query<Contract.V1.LoanApplicationSummary>(
                    "SELECT \"Status\" as Status,count(*) as Count FROM \"LoanApplications\" group by \"Status\"").ToList();
        }
    }


}