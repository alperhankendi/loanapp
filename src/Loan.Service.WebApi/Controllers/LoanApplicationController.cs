using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Loan.Service.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoanApplicationController : ControllerBase
    {
        [Route("")]
        [HttpGet]
        public Task<List<string>> Get()
        {
            return Task.FromResult(new List<string>
            {
                "test1",
                "test2"
            });
        }
    }
}