using System.Collections.Generic;
using System.Linq;
using QuoteCalculator.Models;

namespace QuoteCalculator
{
	public class LoanQuoteCalculator
	{
		private readonly IEnumerable<LoanOffer> _loanOffers;
		private readonly ILoanCalculator _calculator;

		public LoanQuoteCalculator(IEnumerable<LoanOffer> loanOffers, ILoanCalculator calculator)
		{
			_loanOffers = loanOffers;
			_calculator = calculator;
		}

		public LoanQuote GetQuote(int loanAmount, int loanMonths)
		{
			var monthlyPayment = _loanOffers.Sum(o => _calculator.CalculateMonthlyPayment(o.Amount, o.Rate, loanMonths));
			var totalPayment = _calculator.CalculateTotalPayment(monthlyPayment, loanMonths);
			var interestRate = _calculator.CalculateInterestRate(loanAmount, monthlyPayment, loanMonths);

			return new LoanQuote
			{
				LoanAmount = loanAmount,
				InterestRate = interestRate,
				MonthlyRepayment = monthlyPayment,
				TotalRepayment = totalPayment
			};
		}
	}
}