using System;
using Autofac;
using QuoteCalculator.Interfaces;
using QuoteCalculator.Models;

namespace QuoteCalculator
{
	class Program
	{
		static void Main(string[] args)
		{
			ParseArgs(args, out var filename, out var loanMonths);

			var iocContainer = IocComposition.Compose();

			var quoteCalculator = iocContainer.Resolve<ILoanQuoteCalculator>();

			LoanQuote quote = null;
			try
			{
				quote = quoteCalculator.GetQuoteForMonths(loanMonths);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return;
			}

			iocContainer.Resolve<IQuotePrinter>().Print(quote);
		}

		static void ParseArgs(string[] args, out string filename, out int loanMonths)
		{
			loanMonths = 36;
			filename = string.Empty;
		}
	}
}
