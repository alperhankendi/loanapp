using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Loan.Domain.Repository
{
    public class LoanDbContext : DbContext
    {
        public DbSet<LoanApplication> LoanApplications { get; set; }
        
        public DbSet<Operator> Operators { get; set; }

        public LoanDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoanApplication>()
                .Property(l => l.Number)
                .HasConversion(x => x.Number, x => new LoanApplicationNumber(x));
            modelBuilder.Entity<LoanApplication>()
                .HasKey(l => l.Id);
            modelBuilder.Entity<LoanApplication>()
                .Property(l => l.Id)
                .HasConversion(x=>x.Id, x=> new LoanApplicationId(x));
            
            modelBuilder.Entity<LoanApplication>()
                .Property(l => l.Number);
            
            var converter = new EnumToStringConverter<LoanApplicationStatus>();
            var converterForScore = new EnumToStringConverter<ApplicationScore>();
            
            modelBuilder.Entity<LoanApplication>()
                .Property(l => l.Status).HasConversion(converter);
            
            modelBuilder.Entity<LoanApplication>()
                .OwnsOne(a => a.Score, s =>
                {
                    s.Property(x => x.Explanation);
                    s.Property(x => x.Score).HasConversion(converterForScore);
                });
            
            modelBuilder.Entity<LoanApplication>()
                .OwnsOne(a => a.Customer)
                .OwnsOne(c =>c.Address, ca =>
                {
                    ca.Property(x => x.Country);
                    ca.Property(x => x.City);
                    ca.Property(x => x.ZipCode);
                    ca.Property(x => x.Street);
                });
            modelBuilder.Entity<LoanApplication>()
                .OwnsOne(a => a.Customer)
                .OwnsOne(c =>c.NationalIdentifier, ni => ni.Property(x=>x.Value));
            modelBuilder.Entity<LoanApplication>()
                .OwnsOne(a => a.Customer)
                .OwnsOne(c =>c.Name, cn =>
                {
                    cn.Property(x => x.First);
                    cn.Property(x => x.Last);
                });
            modelBuilder.Entity<LoanApplication>()
                .OwnsOne(a => a.Customer, c => c.Property(x => x.Birthdate));
            modelBuilder.Entity<LoanApplication>()
                .OwnsOne(a => a.Customer)
                .OwnsOne(c =>c.MonthlyIncome, ma => ma.Property(x=>x.Amount));
            
            modelBuilder.Entity<LoanApplication>()
                .OwnsOne(a => a.Property)
                .OwnsOne(p =>p.Address, pa =>
                {
                    pa.Property(x => x.Country);
                    pa.Property(x => x.City);
                    pa.Property(x => x.ZipCode);
                    pa.Property(x => x.Street);
                });
            modelBuilder.Entity<LoanApplication>()
                .OwnsOne(a => a.Property)
                .OwnsOne(p =>p.Value, pv => pv.Property(x=>x.Amount));
            
            modelBuilder.Entity<LoanApplication>()
                .OwnsOne(a => a.Loan)
                .OwnsOne(l =>l.InterestRate, ir => ir.Property(x=>x.Value));
            modelBuilder.Entity<LoanApplication>()
                .OwnsOne(a => a.Loan)
                .OwnsOne(l =>l.LoanAmount, la => { la.Property(x => x.Amount); });
            modelBuilder.Entity<LoanApplication>()
                .OwnsOne(a => a.Loan, l => l.Property(x => x.LoanNumberOfYears));
                
            modelBuilder.Entity<LoanApplication>()
                .OwnsOne(a => a.Decision, d =>
                {
                    d.OwnsOne(x => x.DecisionBy, db => db.Property(y => y.Id));
                    d.Property(x => x.DecisionDate);
                });
            
            modelBuilder.Entity<LoanApplication>()
                .OwnsOne(a => a.Registration, r =>
                {
                    r.OwnsOne(x => x.RegisteredBy, db => db.Property(y => y.Id));
                    r.Property(x => x.RegistrationDate);
                });
            
            modelBuilder.Entity<Operator>()
                .HasKey(l => l.Id);
            modelBuilder.Entity<Operator>()
                .Property(l => l.Id)
                .HasConversion(x=>x.Id, x=> new OperatorId(x));
            modelBuilder.Entity<Operator>()
                .OwnsOne(o => o.CompetenceLevel, cl => cl.Property(c=>c.Amount));
            modelBuilder.Entity<Operator>()
                .Property(l => l.Login)
                .HasConversion(x => x.Value, x => new Login(x));
            modelBuilder.Entity<Operator>()
                .Property(l => l.Password)
                .HasConversion(x=>x.Value, x => new Password(x));
            modelBuilder.Entity<Operator>()
                .OwnsOne(
                    l => l.Name,
                    n =>
                    {
                        n.Property(o => o.First).HasColumnName("FirstName");
                        n.Property(o => o.Last).HasColumnName("LastName");
                    });
        }
    }
}