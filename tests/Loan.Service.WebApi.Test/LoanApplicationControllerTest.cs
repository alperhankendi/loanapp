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
        public string LoanApplicationController_CreateValidLoanApplication_ToBeSuccessCode()
        {
            var response =  client.PostAsJsonAsync("/LoanApplication", applicationRequest).Result;
            var payload =  response.Content.ReadAsStringAsync().Result;
            testOutputHelper.WriteLine($"Response :{payload}");
            
            response.EnsureSuccessStatusCode();
            Assert.NotEmpty(payload);
            return payload;
        }
        [Fact]
        public void LoanApplicationController_CreateInValidLoanApplication_ToBeFailedCode()
        {
            applicationRequest.NationalIdentifier = "invalid_identity";

            var response = client.PostAsJsonAsync("/LoanApplication", applicationRequest).Result;
            var payload = response.Content.ReadAsStringAsync().Result;
            testOutputHelper.WriteLine($"Response :{payload}");
            
            Assert.Equal(HttpStatusCode.InternalServerError,response.StatusCode);
            Assert.Contains("National Identifier must be 11 chars long",payload);
        }
        [Fact]
        public async Task<string> LoanApplicationController_EvaluateValidLoanApplication_ToBeSuccessCode()
        {
            var validApplicationNumber = LoanApplicationController_CreateValidLoanApplication_ToBeSuccessCode();
            var response = await client.PutAsync($"/LoanApplication/evaluate/{validApplicationNumber}",null);
            testOutputHelper.WriteLine($"Response :{response}");
            response.EnsureSuccessStatusCode();
            return validApplicationNumber;
        }
        [Fact]
        public async Task<string> LoanApplicationController_EvaluateInValidLoanApplication_ToBeSuccessCode()
        {
            applicationRequest.MonthlyIncome = 2000;
            applicationRequest.PropertyValue = 20000;
            var validApplicationNumber = LoanApplicationController_CreateValidLoanApplication_ToBeSuccessCode();
            var response = await client.PutAsync($"/LoanApplication/evaluate/{validApplicationNumber}",null);
            testOutputHelper.WriteLine($"Response :{response}");
            response.EnsureSuccessStatusCode();
            return validApplicationNumber;
        }
        [Fact]
        public async Task LoanApplicationController_DecisionValidLoanApplication_ToBeAcceptedWithSuccessCode()
        {
            //Arrange
            var validApplicationNumber = await LoanApplicationController_EvaluateValidLoanApplication_ToBeSuccessCode();
            //Act
            var response = await client.PutAsync($"/LoanApplication/accept/{validApplicationNumber}",null);
            //Assert
            testOutputHelper.WriteLine($"Response :{response}");
            response.EnsureSuccessStatusCode();
        }
        [Fact]
        public async Task LoanApplicationController_DecisionValidLoanApplication_ToBeRejectedWithSuccessCode()
        {
            //Arrange
            var validApplicationNumber = await LoanApplicationController_EvaluateValidLoanApplication_ToBeSuccessCode();
            //Act
            var response = await client.PutAsync($"/LoanApplication/reject/{validApplicationNumber}",null);
            //Assert
            testOutputHelper.WriteLine($"Response :{response}");
            response.EnsureSuccessStatusCode();
        }
    }
}