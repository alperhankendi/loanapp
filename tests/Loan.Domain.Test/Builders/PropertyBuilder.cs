namespace Loan.Domain.Test.Builders
{
    internal class PropertyBuilder
    {
        private Money value = new Money(300_000M);
        private Address address = new Address("Turkey","34840","İstanbul","Cumhuriyet Cad.");

        public PropertyBuilder WithValue(decimal propertyValue)
        {
            value = new Money(propertyValue);
            return this;
        }

        public PropertyBuilder WithAddress(string country,string zip,string city,string street)
        {
            this.address = new Address(country,zip,city,street);
            return this;
        }

        public Property Build()
        {
            return new Property
            (
                value,
                address
            );
        }
    }
}