using Loan.Domain.Application;
using Loan.Domain.ReadModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Loan.Service.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoanApplicationController : ControllerBase
    {
        private readonly LoanApplicationSubmissionService loanApplicationSubmissionService;
        private readonly LoanApplicationEvaluationService loanApplicationEvaluationService;
        private readonly LoanApplicationDecisionService loanApplicationDecisionService;
        private readonly LoanApplicationFinder loanApplicationFinder;

        private static string fakeUser = "admin";
        public LoanApplicationController(LoanApplicationSubmissionService loanApplicationSubmissionService,
            LoanApplicationEvaluationService loanApplicationEvaluationService,
            LoanApplicationDecisionService loanApplicationDecisionService,
            LoanApplicationFinder loanApplicationFinder)
        {
            this.loanApplicationSubmissionService = loanApplicationSubmissionService;
            this.loanApplicationEvaluationService = loanApplicationEvaluationService;
            this.loanApplicationDecisionService = loanApplicationDecisionService;
            this.loanApplicationFinder = loanApplicationFinder;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
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

        [HttpGet]
        [Route("summary")]
        public IActionResult GetSummary()
        {
            var result = loanApplicationFinder.GetLoanApplicationSummary();
            return Ok(result);
        }
        [HttpGet]
        [Route("summary_details")]
        public IActionResult GetSummaryWithDetail()
        {
            var result = loanApplicationFinder.GetLoanApplicationSummaryWithDetails();
            return Ok(result);
        }
        
    }
}