using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuoteCalculator.Models;

namespace QuoteCalculator.Tests
{
	[TestClass]
	public class LoanQuoteCalculatorTests
	{
		[TestMethod]
		public void GetCorrectQuote_ZeroLoanAmountAndZeroLoanMonths()
		{
			var loanAmount = 100;
			var loanMonths = 0;
			var loanOffers = new[] {
				new LoanOffer
				{
					LenderName = "Test",
					Amount = 100,
					Rate = 0.075M,
				}
			};
			var calculator = new LoanQuoteCalculator(loanOffers);

			var quoteResult = calculator.GetQuote(loanAmount, loanMonths);

			Assert.AreEqual(100, quoteResult.LoanAmount, "Incorrect loan amount");
			Assert.AreEqual(0.075M, quoteResult.InterestRate, "Incorrect interest rate");
			Assert.AreEqual(0, quoteResult.MonthlyRepayment, "Incorrect monthly repayment");
			Assert.AreEqual(0, quoteResult.TotalRepayment, "Incorrect total amount");
		}

		[TestMethod]
		public void GetCorrectQuote_OneOfferAndOneMonth()
		{
			var loanAmount = 100;
			var loanMonths = 1;
			var loanOffers = new[] {
				new LoanOffer
				{
					LenderName = "Test",
					Amount = 100,
					Rate = 0.075M,
				}
			};
			var calculator = new LoanQuoteCalculator(loanOffers);

			var quoteResult = calculator.GetQuote(loanAmount, loanMonths);

			Assert.AreEqual(100, quoteResult.LoanAmount);
			Assert.AreEqual(0.075M, quoteResult.InterestRate);
			Assert.AreEqual(100, quoteResult.MonthlyRepayment);
			Assert.AreEqual(100, quoteResult.TotalRepayment);
		}
	}
}
