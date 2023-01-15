using Loan.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loan.Domain.Repository.Persistence.Configurations;

public class OperatorConfiguration : IEntityTypeConfiguration<Operator>
{
    void IEntityTypeConfiguration<Operator>.Configure(EntityTypeBuilder<Operator> entity)
    {
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnType("uuid").HasConversion(x => x.Id, x => new OperatorId(x));
        entity.OwnsOne(o => o.CompetenceLevel, cl => cl.Property(c => c.Amount));
        entity.Property(l => l.Login).HasConversion(x => x.Value, x => new Login(x));
        entity.Property(l => l.Password).HasConversion(x => x.Value, x => new Password(x));
        entity.OwnsOne(l => l.Name,
            n =>
            {
                n.Property(o => o.First).HasColumnName("FirstName");
                n.Property(o => o.Last).HasColumnName("LastName");
            });
    }
}

