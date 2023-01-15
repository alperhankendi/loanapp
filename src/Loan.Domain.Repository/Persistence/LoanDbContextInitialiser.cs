using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Loan.Domain.Repository.Persistence;

public class LoanDbContextInitialiser
{
    private readonly ILogger<LoanDbContextInitialiser> _logger;
    private readonly LoanDbContext _context;
    public LoanDbContextInitialiser(ILogger<LoanDbContextInitialiser> logger, LoanDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsNpgsql())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }
    public async Task SeedAsync(CancellationToken cancellationToken)
    {
        try
        {
            await TrySeedAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }
    private async Task TrySeedAsync(CancellationToken cancellationToken)
    {
        if (!_context.Operators.Any(o => o.Login == new Login("admin")))
        {
            await _context.Operators.AddAsync(new Operator(
                new Login("admin"), new Password("admin"),
                new Name("Admin", "Admin"),
                new Money(1_000_000M)
            ), cancellationToken);
        }

        await _context.SaveChangesAsync(cancellationToken);
    }

}
