using System;
using System.Collections.Generic;
using Loan.Domain.Application;
using Loan.Domain.Test.Builders;
using Loan.Service.Api.Modules.LoanApplication;
using Xunit;
using Xunit.Abstractions;

namespace Loan.Domain.Test
{
    public class LoanApplicationSubmissionServiceTests
    {
        private readonly ITestOutputHelper outputHelper;

        public LoanApplicationSubmissionServiceTests(ITestOutputHelper outputHelper)
        {
            this.outputHelper = outputHelper;
        }
        [Fact]
        public void LoadApplicationSubmissionService_ValidApplication_GetsSubmitted()
        {

            var operators = new OperatorRepositoryMock(new List<Operator>
            {
                new OperatorBuilder().WithLogin("admin").Build(),
            });
            var loanapplications = new LoanApplicationRepositoryMock(new List<LoanApplication>());
            
            var service = new LoanApplicationSubmissionService(operators,
                loanapplications,
                new UnitOfWorkMock(),
                new EventPublisherMock()
            );

            var applicationRequest = new Contract.V1.SubmitApplication
            {
                FirstName = "Alper",
                LastName = "Hankendi",
                BirthDate = new DateTime(1980, 7, 20),
                NationalIdentifier = "11111111111",
                CustomerAddress = new Contract.V1.Address
                {
                    City = "Istanbul",
                    Country = "Turkey",
                    Street = "Cumhuriyet Cad.",
                    ZipCode = "34840"
                },
                Email = "ahankendi@gmail.com",
                MonthlyIncome = 20_000M,
                LoanNumberOfYears = 10,
                LoanInterestRate = 1.1M,
                LoanAmount =  200_000M,
                PropertyValue = 250_000M,
                ProperyAddress = new Contract.V1.Address
                {
                    City = "Istanbul",
                    Country = "Turkey",
                    Street = "Cumhuriyet Cad.",
                    ZipCode = "34840"
                }
            };

            var appNumber = service.SubmitLoanApplication(Mapper.ToModel(applicationRequest));
            outputHelper.WriteLine($"Func:ValidApplication_GetsSubmitted, Result:{appNumber}");
            Assert.False(string.IsNullOrWhiteSpace(appNumber));
            Assert.NotNull(loanapplications.WithNumber(LoanApplicationNumber.Of(appNumber)));
        }

        [Fact]
        public void LoadApplicationSubmissionService_InvalidApplication_IsNotSaved()
        {
            var operators = new OperatorRepositoryMock(new List<Operator>
            {
                new OperatorBuilder().WithLogin("admin").Build(),
            });
            var loanapplications = new LoanApplicationRepositoryMock(new List<LoanApplication>());
            
            var eventPublisher = new EventPublisherMock();
            var service = new LoanApplicationSubmissionService(operators,
                loanapplications,
                new UnitOfWorkMock(),
                eventPublisher
            );

            var applicationRequest = new Contract.V1.SubmitApplication
            {
                FirstName = "Alper",
                LastName = "Hankendi",
                BirthDate = new DateTime(1980, 7, 20),
                NationalIdentifier = "1111111111",
                CustomerAddress = new Contract.V1.Address
                {
                    City = "Istanbul",
                    Country = "Turkey",
                    Street = "Cumhuriyet Cad.",
                    ZipCode = "34840"
                },
                MonthlyIncome = 20_000M,
                LoanNumberOfYears = 10,
                LoanInterestRate = 1.1M,
                LoanAmount =  200_000M,
                PropertyValue = 250_000M,
                ProperyAddress = new Contract.V1.Address
                {
                    City = "Istanbul",
                    Country = "Turkey",
                    Street = "Cumhuriyet Cad.",
                    ZipCode = "34840"
                }

            };

            var ex = Assert.Throws<ArgumentException>(() => service.SubmitLoanApplication(Mapper.ToModel(applicationRequest)));
            outputHelper.WriteLine(ex.Message);
            Assert.Equal("National Identifier must be 11 chars long",ex.Message);
        }
    }
}