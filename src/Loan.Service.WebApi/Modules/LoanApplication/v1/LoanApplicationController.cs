using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Loan.Domain.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Loan.Service.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoanApplicationController : ControllerBase
    {
        private readonly LoanApplicationSubmissionService loanApplicationSubmissionService;

        private static string fakeUser = "admin";
        public LoanApplicationController(LoanApplicationSubmissionService loanApplicationSubmissionService)
        {
            this.loanApplicationSubmissionService = loanApplicationSubmissionService;
        }

        [HttpPost]
        public string Create([FromBody] Contract.V1.SubmitApplication submitApplication)
        {
            var newLoanApplicationNumber = loanApplicationSubmissionService.SubmitLoanApplication(submitApplication, fakeUser);
            return newLoanApplicationNumber;
        }
    }
}