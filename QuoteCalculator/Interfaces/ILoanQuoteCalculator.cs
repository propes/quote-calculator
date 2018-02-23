using QuoteCalculator.Models;

namespace QuoteCalculator.Interfaces
{
	public interface ILoanQuoteCalculator
	{
		LoanQuote GetQuoteForMonths(int loanMonths);
	}
}