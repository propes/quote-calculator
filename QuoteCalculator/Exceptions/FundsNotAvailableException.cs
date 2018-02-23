using System;

namespace QuoteCalculator.Exceptions
{
	public class FundsNotAvailableException : Exception
	{
		public FundsNotAvailableException() :
			base("Sorry, the market does not have sufficient offers from lenders to satisfy the loan.")
		{
		}
	}
}
