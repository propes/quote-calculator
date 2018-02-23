using QuoteCalculator.Interfaces;
using QuoteCalculator.Models;
using System.Linq;

namespace QuoteCalculator
{
	public class LoanQuoteGenerator : ILoanQuoteGenerator
	{
		private readonly ILoanAllocationProvider loanAllocationProvider;
		private readonly ILoanCalculator calculator;

		public LoanQuoteGenerator(ILoanAllocationProvider loanAllocationProvider, ILoanCalculator calculator)
		{
			this.loanAllocationProvider = loanAllocationProvider;
			this.calculator = calculator;
		}

		public LoanQuote GetQuote(double loanAmount, double loanMonths)
		{
			var loanAllocations = loanAllocationProvider.GetLoanAllocationsForAmount(loanAmount);
			var monthlyPayment = loanAllocations.Sum(o => calculator.CalculateMonthlyPayment(o.Amount, o.Rate, loanMonths));
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