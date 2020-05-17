using System;

namespace Loan.Domain.Test.Builders
{
    internal class LoanApplicationBuilder
    {
        private Operator user = new Operator(new Login("admin"), new Password("admin"), new Name("admin", "admin"), new MonetaryAmount(1_000_000));
        private Customer customer = new CustomerBuilder().Build();
        private Property property = new PropertyBuilder().Build();
        private Loan loan = new LoanBuilder().Build();
        private LoanApplicationNumber applicationNumber = new LoanApplicationNumber(Guid.NewGuid().ToString());
        private LoanApplicationStatus targetStatus = LoanApplicationStatus.New;

        public LoanApplicationBuilder WithNumber(string number)
        {
            applicationNumber = new LoanApplicationNumber(number);
            return this;
        }
        public LoanApplicationBuilder WithOperator(string login)
        {
            user = new Operator(new Login(login), new Password(login),new Name(login,login),new MonetaryAmount(1_000_000));
            return this;
        }
        public LoanApplicationBuilder WithCustomer(Action<CustomerBuilder> customizeCustomer)
        {
            var customerBuilder = new CustomerBuilder();
            customizeCustomer(customerBuilder);
            customer = customerBuilder.Build();
            return this;
        }
        public LoanApplicationBuilder WithProperty(Action<PropertyBuilder> propertyCustomizer)
        {
            var propertyBuilder = new PropertyBuilder();
            propertyCustomizer(propertyBuilder);
            property = propertyBuilder.Build();
            return this;
        }
        public LoanApplicationBuilder WithLoan(Action<LoanBuilder> loanCustomizer)
        {
            var loanBuilder = new LoanBuilder();
            loanCustomizer(loanBuilder);
            loan = loanBuilder.Build();
            return this;
        }
        public LoanApplication Build()
        {
            var application = new LoanApplication
            (
                applicationNumber,
                customer,
                property,
                loan,
                user.Id
            );

            return application;
        }
        
    }
}