using System.Collections.Generic;
using System.Linq;
using QuoteCalculator.Models;

namespace QuoteCalculator
{
	public class LoanQuoteCalculator
	{
		private readonly IEnumerable<LoanOffer> _loanOffers;

		public LoanQuoteCalculator(IEnumerable<LoanOffer> loanOffers)
		{
			_loanOffers = loanOffers;
		}

		public LoanQuote GetQuote(int loanAmount, int loanMonths)
		{
			var firstLoanOffer = _loanOffers.First();

			var interestRate = firstLoanOffer.Rate;
			var totalRepayment = loanMonths == 0 ? 0 : firstLoanOffer.Amount;
			var monthlyRepayment = loanMonths == 0 ? 0 : totalRepayment / loanMonths;

			return new LoanQuote
			{
				LoanAmount = loanAmount,
				InterestRate = interestRate,
				MonthlyRepayment = monthlyRepayment,
				TotalRepayment = totalRepayment
			};
		}
	}
}