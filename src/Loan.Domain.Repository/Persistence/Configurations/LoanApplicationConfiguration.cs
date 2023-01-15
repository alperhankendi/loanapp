using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Linq;

namespace Loan.Domain.Repository.Persistence.Configurations;
public class LoanApplicationConfiguration : IEntityTypeConfiguration<LoanApplication>
{
    void IEntityTypeConfiguration<LoanApplication>.Configure(EntityTypeBuilder<LoanApplication> entity)
    {
        entity.Property(e => e.Id).HasConversion(typeof(Guid));
        entity.Property(l => l.Number).HasConversion(x => x.Number, x => new LoanApplicationNumber(x));
        entity.Property(e => e.Status).HasConversion(new EnumToStringConverter<LoanApplicationStatus>());
        entity.Property(p => p.Id).HasConversion(v => v.Id, v => new LoanApplicationId(v));
        entity.OwnsOne(o => o.Score,
            sa =>
            {
                sa.Property(p => p.Score).HasConversion(new EnumToStringConverter<ApplicationScore>());
                sa.Property(p => p.Explanation);
            });
        entity.OwnsOne(o => o.Customer, sa =>
        {
            sa.OwnsOne(o => o.Name, ss =>
            {
                ss.Property(p => p.First).IsRequired();
                ss.Property(p => p.Last).IsRequired();
            })
            .OwnsOne(o => o.NationalIdentifier, ss => ss.Property(p => p.Value))
            .OwnsOne(c => c.Address, ca =>
            {
                ca.Property(x => x.Country).IsRequired();
                ca.Property(x => x.City).IsRequired();
                ca.Property(x => x.ZipCode).IsRequired();
                ca.Property(x => x.Street).IsRequired();
            })
            .OwnsOne(e => e.Email, cn => cn.Property(x => x.MailValue));
            sa.Property(e => e.Birthdate)
                .HasConversion(
                    src => src.Kind == DateTimeKind.Utc ? src : DateTime.SpecifyKind(src, DateTimeKind.Utc),
                    dst => dst.Kind == DateTimeKind.Utc ? dst : DateTime.SpecifyKind(dst, DateTimeKind.Utc));

            sa.Property(e => e.MonthlyIncome).HasConversion(x => x.Amount, v => new Money(v));
        });
        
        entity.OwnsOne(o => o.Property, sa =>
        {
            sa.OwnsOne(p => p.Address, pa =>
            {
                pa.Property(x => x.Country).IsRequired();
                pa.Property(x => x.City).IsRequired();
                pa.Property(x => x.ZipCode).IsRequired();
                pa.Property(x => x.Street).IsRequired();
            });
            sa.Property(e => e.Value).HasConversion(x => x.Amount, v => new Money(v)).IsRequired();
        });

        entity.OwnsOne(o => o.Loan, sa =>
        {
            sa.Property(l => l.InterestRate).HasConversion(r => r.Value, x => new Percent(x));
            sa.Property(l => l.LoanAmount).HasConversion(r => r.Amount, x => new Money(x));
            sa.Property(l => l.LoanNumberOfYears);
        });
        entity.OwnsOne(o => o.Decision, sa =>
        {
            sa.Property(x => x.DecisionBy).HasConversion(db => db.Id, x => new OperatorId(x));
            sa.Property(l => l.DecisionDate);
        });
        entity.OwnsOne(o => o.Registration, sa =>
        {
            sa.Property(x => x.RegisteredBy).HasConversion(db => db.Id, x => new OperatorId(x));
            sa.Property(l => l.RegistrationDate);
        });
    }
}
