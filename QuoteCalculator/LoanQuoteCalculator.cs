using QuoteCalculator.Models;
using System.Collections.Generic;
using System.Linq;
using QuoteCalculator.Interfaces;

namespace QuoteCalculator
{
	public class LoanQuoteCalculator : ILoanQuoteCalculator
	{
		private readonly IEnumerable<LoanAllocation> loanAllocations;
		private readonly ILoanCalculator calculator;

		public LoanQuoteCalculator(ILoanAllocationProvider loanAllocationProvider, ILoanCalculator calculator)
		{
			this.loanAllocations = loanAllocationProvider.GetLoanAllocations();
			this.calculator = calculator;
		}

		public LoanQuote GetQuoteForMonths(int loanMonths)
		{
			var loanAmount = this.loanAllocations.Sum(o => o.Amount);
			var monthlyPayment = this.loanAllocations.Sum(o => calculator.CalculateMonthlyPayment(o.Amount, o.Rate, loanMonths));
			var totalPayment = calculator.CalculateTotalPayment(monthlyPayment, loanMonths);
			var interestRate = calculator.CalculateInterestRate(loanAmount, monthlyPayment, loanMonths);

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