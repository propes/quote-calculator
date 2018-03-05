using QuoteCalculator.Models;

namespace QuoteCalculator.Interfaces
{
	public interface ILoanQuoteGenerator
	{
		LoanQuote GetQuote(decimal loanAmount, decimal loanMonths);
	}
}