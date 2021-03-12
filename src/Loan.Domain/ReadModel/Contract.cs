namespace Loan.Domain.ReadModel
{
    public static class Contract
    {
        public static class V1
        {
            public class LoanApplicationSummary
            {
                public string Status { get; set; }
                public int Count { get; set; }
            }
            public class LoanApplicationSummaryWithDetail
            {
                public string ApplicationId { get; set; }
                public string NameSurname { get; set; }
                public string Status { get; set; }
            }
        }
    }
}