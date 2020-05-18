using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Loan.Domain.Application;
using Xunit;
using Xunit.Abstractions;

namespace Loan.Service.WebApi.Test
{
    public class LoanApplicationControllerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> factory;
        private readonly ITestOutputHelper testOutputHelper;
        private readonly HttpClient client;

        private readonly Contract.V1.SubmitApplication applicationRequest = null; 
            
        public LoanApplicationControllerTest(CustomWebApplicationFactory<Startup> factory,
            ITestOutputHelper testOutputHelper)
        {
            this.factory = factory;
            this.testOutputHelper = testOutputHelper;
            client = factory.CreateClient();
            
            applicationRequest = new Contract.V1.SubmitApplication
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
        }
        [Fact]
        public async Task LoanApplicationController_CreateValidLoanApplication_ToBeSuccessCode()
        {
            var response = await client.PostAsJsonAsync("/LoanApplication", applicationRequest);
            var payload = await response.Content.ReadAsStringAsync();
            testOutputHelper.WriteLine($"Response :{payload}");
            
            response.EnsureSuccessStatusCode();
            Assert.NotEmpty(payload);
        }
        
        [Fact]
        public async Task LoanApplicationController_CreateValidLoanApplication_ToBeFailedCode()
        {
            applicationRequest.NationalIdentifier = "invalid_identity";

            var response = await client.PostAsJsonAsync("/LoanApplication", applicationRequest);
            var payload = await response.Content.ReadAsStringAsync();
            testOutputHelper.WriteLine($"Response :{payload}");
            
            Assert.Equal(HttpStatusCode.InternalServerError,response.StatusCode);
            Assert.Contains("National Identifier must be 11 chars long",payload);
        }
    }
}