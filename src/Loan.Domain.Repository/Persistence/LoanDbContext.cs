using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using System.Reflection;
using System.IO;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;

namespace Loan.Domain.Repository.Persistence;
public class LoanDbContext : DbContext
{
    public DbSet<LoanApplication> LoanApplications { get; set; }

    public DbSet<Operator> Operators { get; set; }

    public LoanDbContext(){}
    public LoanDbContext(DbContextOptions options) : base(options){
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.ConfigureWarnings(warnings => warnings.Default(WarningBehavior.Ignore)
            .Log(CoreEventId.NavigationBaseIncludeIgnored, CoreEventId.RequiredAttributeOnCollection)
        .Throw(RelationalEventId.BoolWithDefaultWarning));

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        if (!options.IsConfigured)
        {
            options.UseSqlite($"Data Source={System.IO.Path.Join(@"C:\workspace\loanapp\", "loan_ddd.db")}");
        }
        base.OnConfiguring(options);
    }
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}