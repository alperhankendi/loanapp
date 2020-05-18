using System;

namespace Loan.Domain.Application
{
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