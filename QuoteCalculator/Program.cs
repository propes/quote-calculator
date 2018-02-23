using System;
using System.Configuration;
using Autofac;
using QuoteCalculator.Composition;
using QuoteCalculator.Interfaces;
using QuoteCalculator.Models;

namespace QuoteCalculator
{
	class Program
	{
		private static readonly int LoanMonths = int.Parse(ConfigurationManager.AppSettings["LoanMonths"]);

		static void Main(string[] args)
		{
			ParseArgs(args, out var filename, out var loanAmount);

			var iocContainer = IocComposition.Compose();

			LoanQuote quote;
			try
			{
				var quoteCalculator = iocContainer.Resolve<ILoanQuoteCalculator>();
				quote = quoteCalculator.GetQuote(loanAmount, LoanMonths);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return;
			}

			iocContainer.Resolve<IQuotePrinter>().Print(quote);
		}

		static void ParseArgs(string[] args, out string filename, out int loanAmount)
		{
			loanAmount = 0;
			filename = string.Empty;
		}
	}
}
