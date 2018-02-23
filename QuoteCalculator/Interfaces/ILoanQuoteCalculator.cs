using QuoteCalculator.Models;

namespace QuoteCalculator.Interfaces
{
	public interface ILoanQuoteCalculator
	{
		LoanQuote GetQuote(double loanAmount, double loanMonths);
	}
}