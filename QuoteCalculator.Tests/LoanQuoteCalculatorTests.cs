using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using QuoteCalculator.Models;
using System;
using QuoteCalculator.Interfaces;

namespace QuoteCalculator.Tests
{
	[TestClass]
	public class LoanQuoteCalculatorTests
	{
		private readonly ILoanCalculator calculator = new SimpleLoanCalculator();
		private readonly Mock<ILoanAllocationProvider> mockAllocationProvider = new Mock<ILoanAllocationProvider>();

		[TestInitialize]
		public void Initialize()
		{
			
		}

		[TestMethod]
		public void GetCorrectQuote_ZeroLoanAmountAndZeroLoanMonths()
		{
			mockAllocationProvider.Setup(m => m.GetLoanAllocations())
					.Returns(new[] {
					new LoanAllocation
					{
						Amount = 100,
						Rate = 0.12M,
					}
				});

			var quoteCalculator = new LoanQuoteCalculator(mockAllocationProvider.Object, calculator);

			var result = quoteCalculator.GetQuoteForMonths(0);

			Assert.AreEqual(100, result.LoanAmount, "Incorrect loan amount");
			Assert.AreEqual(0, result.InterestRate, "Incorrect interest rate");
			Assert.AreEqual(0, result.MonthlyRepayment, "Incorrect monthly repayment");
			Assert.AreEqual(0, result.TotalRepayment, "Incorrect total amount");
		}

		[TestMethod]
		public void GetCorrectQuote_OneOfferAndOneMonth()
		{
			mockAllocationProvider.Setup(m => m.GetLoanAllocations())
				.Returns(new[] {
					new LoanAllocation
					{
						Amount = 100,
						Rate = 0.12M,
					}
				});

			var quoteCalculator = new LoanQuoteCalculator(mockAllocationProvider.Object, calculator);

			var result = quoteCalculator.GetQuoteForMonths(1);

			Assert.AreEqual(100, result.LoanAmount, "Incorrect loan amount");
			Assert.AreEqual(0.12M, result.InterestRate, "Incorrect interest rate");
			Assert.AreEqual(101, result.MonthlyRepayment, "Incorrect monthly repayment");
			Assert.AreEqual(101, result.TotalRepayment, "Incorrect total amount");
		}

		[TestMethod]
		public void GetCorrectQuote_TwoIdenticalOffersAndOneMonth()
		{
			mockAllocationProvider.Setup(m => m.GetLoanAllocations())
				.Returns(new[] {
					new LoanAllocation
					{
						Amount = 100,
						Rate = 0.12M,
					},
					new LoanAllocation
					{
						Amount = 100,
						Rate = 0.12M
					}
				});

			var quoteCalculator = new LoanQuoteCalculator(mockAllocationProvider.Object, calculator);

			var result = quoteCalculator.GetQuoteForMonths(1);

			Assert.AreEqual(200, result.LoanAmount, "Incorrect loan amount");
			Assert.AreEqual(0.12M, result.InterestRate, "Incorrect interest rate");
			Assert.AreEqual(202, result.MonthlyRepayment, "Incorrect monthly repayment");
			Assert.AreEqual(202, result.TotalRepayment, "Incorrect total amount");
		}

		[TestMethod]
		public void GetCorrectQuote_TwoDifferentOffersAndOneMonth()
		{
			mockAllocationProvider.Setup(m => m.GetLoanAllocations())
				.Returns(new[] {
					new LoanAllocation
					{
						Amount = 100,
						Rate = 0.12M,
					},
					new LoanAllocation
					{
						Amount = 100,
						Rate = 0.24M
					}
				});

			var quoteCalculator = new LoanQuoteCalculator(mockAllocationProvider.Object, calculator);

			var result = quoteCalculator.GetQuoteForMonths(1);

			Assert.AreEqual(200, result.LoanAmount, "Incorrect loan amount");
			Assert.AreEqual(0.18M, result.InterestRate, "Incorrect interest rate");
			Assert.AreEqual(203, result.MonthlyRepayment, "Incorrect monthly repayment");
			Assert.AreEqual(203, result.TotalRepayment, "Incorrect total amount");
		}

		[TestMethod]
		public void GetCorrectQuote_TwoDifferentOffersAndTwelveMonths()
		{
			mockAllocationProvider.Setup(m => m.GetLoanAllocations())
				.Returns(new[] {
					new LoanAllocation
					{
						Amount = 100,
						Rate = 0.12M,
					},
					new LoanAllocation
					{
						Amount = 100,
						Rate = 0.24M
					}
				});

			var quoteCalculator = new LoanQuoteCalculator(mockAllocationProvider.Object, calculator);

			var result = quoteCalculator.GetQuoteForMonths(12);

			Assert.AreEqual(200, result.LoanAmount, "Incorrect loan amount");
			Assert.AreEqual(0.18M, result.InterestRate, "Incorrect interest rate");
			Assert.AreEqual(19.67M, Math.Round(result.MonthlyRepayment, 2), "Incorrect monthly repayment");
			Assert.AreEqual(236M, Math.Round(result.TotalRepayment), "Incorrect total amount");
		}
	}
}
