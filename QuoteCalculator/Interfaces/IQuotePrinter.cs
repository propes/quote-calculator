using QuoteCalculator.Models;

namespace QuoteCalculator.Interfaces
{
	internal interface IQuotePrinter
	{
		void Print(LoanQuote quote);
	}
}