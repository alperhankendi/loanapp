using Loan.Domain.Repository.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan.Service.WebApi.Test;
public class MockDb : IDbContextFactory<LoanDbContext>
{
    public LoanDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<LoanDbContext>()
            .UseInMemoryDatabase($"InMemoryTestDb-{DateTime.Now.ToFileTimeUtc()}")
            .Options;

        return new LoanDbContext(options);
    }
}