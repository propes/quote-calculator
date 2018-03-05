using Autofac;
using QuoteCalculator.Composition;
using QuoteCalculator.Interfaces;
using QuoteCalculator.Models;
using System;
using System.Configuration;
using System.IO;

namespace QuoteCalculator
{
	// *************
	// ASSUMPTIONS:
	// - A basic implementation of a csv reader is sufficient for this exercise. Normally I would use a third party library for this.
	// - Rates in the input file are annual.
	// - Compounding is monthly.
	// - Payments due at the end of the period.
	//
	// *************

	class Program
	{
		private static readonly int LoanMonths = int.Parse(ConfigurationManager.AppSettings["LoanMonths"]);

		static void Main(string[] args)
		{
			string filename;
			decimal loanAmount;
			try
			{
				ParseArgs(args, out filename, out loanAmount);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return;
			}

			var iocContainer = IocComposition.Compose(filename);

			LoanQuote quote;
			try
			{
				var quoteCalculator = iocContainer.Resolve<ILoanQuoteGenerator>();
				quote = quoteCalculator.GetQuote(loanAmount, LoanMonths);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return;
			}

			iocContainer.Resolve<IQuotePrinter>().Print(quote);
		}

		static void ParseArgs(string[] args, out string filename, out decimal loanAmount)
		{
			if (args.Length != 2)
			{
				throw new Exception("Please provide a filename and a loan amount.");
			}

			filename = ParseFilename(args[0]);
			loanAmount = ParseLoanAmount(args[1]);
		}

		static string ParseFilename(string value)
		{
			if (!File.Exists(value))
			{
				throw new Exception("Error: The specified file does not exist.");
			}

			return value;
		}

		static decimal ParseLoanAmount(string value)
		{
			if (!decimal.TryParse(value, out var loanAmount))
			{
				throw new Exception("Error: Loan amount must be a valid number greater than or equal to zero.");
			}

			if (loanAmount < 0)
			{
				throw new Exception("Error: The loan amount must be a number greater than zero.");
			}

			return loanAmount;
		}
	}
}
