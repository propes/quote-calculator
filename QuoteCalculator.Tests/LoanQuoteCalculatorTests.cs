using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuoteCalculator.Models;

namespace QuoteCalculator.Tests
{
	[TestClass]
	public class LoanQuoteCalculatorTests
	{
		ILoanCalculator _calculator = new SimpleLoanCalculator();

		[TestInitialize]
		public void Initialize()
		{
		}

		[TestMethod]
		public void GetCorrectQuote_ZeroLoanAmountAndZeroLoanMonths()
		{
			var loanAmount = 100;
			var loanMonths = 0;
			var loanOffers = new[] {
				new LoanOffer
				{
					Amount = 200,
					Rate = 0.12M,
				}
			};
			var calculator = new LoanQuoteCalculator(loanOffers, _calculator);

			var result = calculator.GetQuote(loanAmount, loanMonths);

			Assert.AreEqual(100, result.LoanAmount, "Incorrect loan amount");
			Assert.AreEqual(0, result.InterestRate, "Incorrect interest rate");
			Assert.AreEqual(0, result.MonthlyRepayment, "Incorrect monthly repayment");
			Assert.AreEqual(0, result.TotalRepayment, "Incorrect total amount");
		}

		[TestMethod]
		public void GetCorrectQuote_OneOfferAndOneMonth()
		{
			var loanAmount = 100;
			var loanMonths = 1;
			var loanOffers = new[] {
				new LoanOffer
				{
					Amount = 200,
					Rate = 0.12M,
				}
			};
			var calculator = new LoanQuoteCalculator(loanOffers, _calculator);

			var result = calculator.GetQuote(loanAmount, loanMonths);

			Assert.AreEqual(100, result.LoanAmount, "Incorrect loan amount");
			Assert.AreEqual(0.12M, result.InterestRate, "Incorrect interest rate");
			Assert.AreEqual(101, result.MonthlyRepayment, "Incorrect monthly repayment");
			Assert.AreEqual(101, result.TotalRepayment, "Incorrect total amount");
		}

		[TestMethod]
		public void GetCorrectQuote_TwoIdenticalOffersAndOneMonth()
		{
			var loanAmount = 200;
			var loanMonths = 1;
			var loanOffers = new[] {
				new LoanOffer
				{
					Amount = 100,
					Rate = 0.12M,
				},
				new LoanOffer
				{
					Amount = 100,
					Rate = 0.12M
				}
			};
			var calculator = new LoanQuoteCalculator(loanOffers, _calculator);

			var result = calculator.GetQuote(loanAmount, loanMonths);

			Assert.AreEqual(200, result.LoanAmount, "Incorrect loan amount");
			Assert.AreEqual(0.12M, result.InterestRate, "Incorrect interest rate");
			Assert.AreEqual(202, result.MonthlyRepayment, "Incorrect monthly repayment");
			Assert.AreEqual(202, result.TotalRepayment, "Incorrect total amount");
		}

		[TestMethod]
		public void GetCorrectQuote_TwoDifferentOffersAndOneMonth()
		{
			var loanAmount = 200;
			var loanMonths = 1;
			var loanOffers = new[] {
				new LoanOffer
				{
					Amount = 100,
					Rate = 0.12M,
				},
				new LoanOffer
				{
					Amount = 100,
					Rate = 0.24M
				}
			};
			var calculator = new LoanQuoteCalculator(loanOffers, _calculator);

			var result = calculator.GetQuote(loanAmount, loanMonths);

			Assert.AreEqual(200, result.LoanAmount, "Incorrect loan amount");
			Assert.AreEqual(0.18M, result.InterestRate, "Incorrect interest rate");
			Assert.AreEqual(203, result.MonthlyRepayment, "Incorrect monthly repayment");
			Assert.AreEqual(203, result.TotalRepayment, "Incorrect total amount");
		}

		[TestMethod]
		public void GetCorrectQuote_TwoDifferentOffersAndTwelveMonths()
		{
			var loanAmount = 200;
			var loanMonths = 12;
			var loanOffers = new[] {
				new LoanOffer
				{
					Amount = 100,
					Rate = 0.12M,
				},
				new LoanOffer
				{
					Amount = 100,
					Rate = 0.24M
				}
			};
			var calculator = new LoanQuoteCalculator(loanOffers, _calculator);

			var result = calculator.GetQuote(loanAmount, loanMonths);

			Assert.AreEqual(200, result.LoanAmount, "Incorrect loan amount");
			Assert.AreEqual(0.18M, result.InterestRate, "Incorrect interest rate");
			Assert.AreEqual(19.67M, Math.Round(result.MonthlyRepayment, 2), "Incorrect monthly repayment");
			Assert.AreEqual(236M, Math.Round(result.TotalRepayment), "Incorrect total amount");
		}

		[TestMethod]
		public void GetCorrectQuote_TwoOffersOneIncomplete()
		{
			var loanAmount = 200;
			var loanMonths = 12;
			var loanOffers = new[] {
				new LoanOffer
				{
					Amount = 100,
					Rate = 0.12M,
				},
				new LoanOffer
				{
					Amount = 200,
					Rate = 0.24M
				}
			};
			var calculator = new LoanQuoteCalculator(loanOffers, _calculator);

			var result = calculator.GetQuote(loanAmount, loanMonths);

			Assert.AreEqual(200, result.LoanAmount, "Incorrect loan amount");
			Assert.AreEqual(0.18M, result.InterestRate, "Incorrect interest rate");
			Assert.AreEqual(19.67M, Math.Round(result.MonthlyRepayment, 2), "Incorrect monthly repayment");
			Assert.AreEqual(236M, Math.Round(result.TotalRepayment), "Incorrect total amount");
		}
	}
}
