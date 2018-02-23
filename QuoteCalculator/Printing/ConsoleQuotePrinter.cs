using QuoteCalculator.Interfaces;
using QuoteCalculator.Models;
using System;

namespace QuoteCalculator.Printing
{
	public class ConsoleQuotePrinter : IQuotePrinter
	{
		public void Print(LoanQuote quote)
		{
			Console.WriteLine("Requested amount: {0:c0}", quote.LoanAmount);
			Console.WriteLine("Rate: {0:p1}", quote.InterestRate);
			Console.WriteLine("Monthly repayment: {0:c2}", quote.MonthlyRepayment);
			Console.WriteLine("Total repayment: {0:c2}", quote.TotalRepayment);
		}
	}
}
