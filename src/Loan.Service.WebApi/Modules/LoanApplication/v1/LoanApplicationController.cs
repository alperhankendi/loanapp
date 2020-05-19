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
        private readonly LoanApplicationEvaluationService loanApplicationEvaluationService;
        private readonly LoanApplicationDecisionService loanApplicationDecisionService;

        private static string fakeUser = "admin";
        public LoanApplicationController(LoanApplicationSubmissionService loanApplicationSubmissionService,
            LoanApplicationEvaluationService loanApplicationEvaluationService,
            LoanApplicationDecisionService loanApplicationDecisionService)
        {
            this.loanApplicationSubmissionService = loanApplicationSubmissionService;
            this.loanApplicationEvaluationService = loanApplicationEvaluationService;
            this.loanApplicationDecisionService = loanApplicationDecisionService;
        }

        [HttpPost]
        public string Create([FromBody] Contract.V1.SubmitApplication submitApplication)=>
            loanApplicationSubmissionService.SubmitLoanApplication(submitApplication, fakeUser);

        [HttpPut]
        [Route("evaluate/{applicationNumber}")]
        public IActionResult Evaluate(string applicationNumber)
        {
            loanApplicationEvaluationService.EvaluateLoanApplication(applicationNumber);
            return Ok();
        }
        [HttpPut]
        [Route("accept/{applicationNumber}")]
        public IActionResult Accept(string applicationNumber)
        {
            loanApplicationDecisionService.AcceptApplication(applicationNumber,fakeUser);
            return Ok();
        }
        [HttpPut]
        [Route("reject/{applicationNumber}")]
        public IActionResult Reject(string applicationNumber)
        {
            loanApplicationDecisionService.RejectApplication(applicationNumber,fakeUser);
            return Ok();
        }            
    }
}