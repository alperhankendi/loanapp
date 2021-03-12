using System;
using Loan.Domain;

namespace Loan.Service.WebApi.Modules.LoanApplication
{
    public static class Mapper
    {
        public static Domain.LoanApplication ToModel(Contract.V1.SubmitApplication applicationDto)
        {
            var application = new Domain.LoanApplication(
                LoanApplicationNumber.NewNumber, 
                new Customer(
                    new NationalIdentifier(applicationDto.NationalIdentifier),
                    new Name(applicationDto.FirstName,applicationDto.LastName),
                    applicationDto.BirthDate,
                    new Money(applicationDto.MonthlyIncome),
                    new Address(applicationDto.CustomerAddress.Country,
                        applicationDto.CustomerAddress.ZipCode,
                        applicationDto.CustomerAddress.City,
                        applicationDto.CustomerAddress.Street),
                    new Email(applicationDto.Email)
                ),
                new Property(new Money(applicationDto.PropertyValue),
                    new Address(applicationDto.ProperyAddress.Country,
                        applicationDto.ProperyAddress.ZipCode,
                        applicationDto.ProperyAddress.City,
                        applicationDto.ProperyAddress.Street)),
                new Domain.Loan(new Money(applicationDto.LoanAmount),
                    applicationDto.LoanNumberOfYears,
                    new Percent(applicationDto.LoanInterestRate))     
            );
            return application;
        }
    }
    
    public static class Contract
    {
        public static class V1
        {
            public class SubmitApplication
            {
                //About customer
                public string NationalIdentifier { get; set; }
                public string FirstName { get; set; }
                public string LastName { get; set; }
                public DateTime BirthDate { get; set; }
                public decimal MonthlyIncome { get; set; }
                public Address CustomerAddress { get; set; }

                public string Email { get; set; }
                //About Property
                public decimal PropertyValue { get; set; }
                public Address ProperyAddress { get; set; }
                //About Loan
                public decimal LoanAmount { get; set; }
                public int LoanNumberOfYears { get; set; }
                public decimal LoanInterestRate { get; set; }
                
                
            }
            public class Address
            {
                public string Country { get; set; }
                public string ZipCode { get; set; }
                public string City { get; set; }
                public string Street { get; set; }
            }
        }
    }
}