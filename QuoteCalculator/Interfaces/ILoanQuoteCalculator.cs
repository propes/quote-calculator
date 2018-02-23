using QuoteCalculator.Models;

namespace QuoteCalculator.Interfaces
{
	public interface ILoanQuoteGenerator
	{
		LoanQuote GetQuote(double loanAmount, double loanMonths);
	}
}