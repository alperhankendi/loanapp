using System;
using System.Collections.Generic;
using Loan.Core;

namespace Loan.Domain
{
    public class ScoreResult : ValueObject<ScoreResult>
    {
        protected ScoreResult()
        {
        }
        private ScoreResult(ApplicationScore? score, string explanation)
        {
            Score = score;
            Explanation = explanation;
        }
        private ApplicationScore? Score { get; }
        private string Explanation { get; }
        public ScoreResult Green()
        {
            return new ScoreResult(ApplicationScore.Green,null);
        }
        public ScoreResult Red(string[] messages)
        {
            return new ScoreResult(ApplicationScore.Red,string.Join(Environment.NewLine,messages));
        }
        public bool IsRed()
        {
            return Score == ApplicationScore.Red;
        }
        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            yield return Score;
            yield return Explanation;
        }
    }
}