using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using QuoteCalculator.Calculators;
using QuoteCalculator.Interfaces;
using QuoteCalculator.Models;
using System;

namespace QuoteCalculator.Tests
{
	[TestClass]
	public class LoanQuoteCalculatorTests
	{
		private readonly ILoanCalculator calculator = new SimpleLoanCalculator();
		private readonly Mock<ILoanAllocationProvider> mockAllocationProvider = new Mock<ILoanAllocationProvider>();

		[TestMethod]
		public void GetCorrectQuote_ZeroLoanAmountAndZeroLoanMonths()
		{
			mockAllocationProvider.Setup(m => m.GetLoanAllocationsForAmount(It.IsAny<double>()))
				.Returns(new[] {
					new LoanAllocation
					{
						Amount = 100,
						Rate = 0.12,
					}
				});

			var quoteCalculator = new LoanQuoteGenerator(mockAllocationProvider.Object, calculator);

			var result = quoteCalculator.GetQuote(100, 0);

			Assert.AreEqual(100, result.LoanAmount, "Incorrect loan amount");
			Assert.AreEqual(0, result.InterestRate, "Incorrect interest rate");
			Assert.AreEqual(0, result.MonthlyRepayment, "Incorrect monthly repayment");
			Assert.AreEqual(0, result.TotalRepayment, "Incorrect total amount");
		}

		[TestMethod]
		public void GetCorrectQuote_OneOfferAndOneMonth()
		{
			mockAllocationProvider.Setup(m => m.GetLoanAllocationsForAmount(It.IsAny<double>()))
				.Returns(new[] {
					new LoanAllocation
					{
						Amount = 100,
						Rate = 0.12,
					}
				});

			var quoteCalculator = new LoanQuoteGenerator(mockAllocationProvider.Object, calculator);

			var result = quoteCalculator.GetQuote(100, 1);

			Assert.AreEqual(100, result.LoanAmount, "Incorrect loan amount");
			Assert.AreEqual(0.12, Math.Round(result.InterestRate, 2), "Incorrect interest rate");
			Assert.AreEqual(101, result.MonthlyRepayment, "Incorrect monthly repayment");
			Assert.AreEqual(101, result.TotalRepayment, "Incorrect total amount");
		}

		[TestMethod]
		public void GetCorrectQuote_TwoIdenticalOffersAndOneMonth()
		{
			mockAllocationProvider.Setup(m => m.GetLoanAllocationsForAmount(It.IsAny<double>()))
				.Returns(new[] {
					new LoanAllocation
					{
						Amount = 100,
						Rate = 0.12,
					},
					new LoanAllocation
					{
						Amount = 100,
						Rate = 0.12
					}
				});

			var quoteCalculator = new LoanQuoteGenerator(mockAllocationProvider.Object, calculator);

			var result = quoteCalculator.GetQuote(200, 1);

			Assert.AreEqual(200, result.LoanAmount, "Incorrect loan amount");
			Assert.AreEqual(0.12, Math.Round(result.InterestRate, 2), "Incorrect interest rate");
			Assert.AreEqual(202, result.MonthlyRepayment, "Incorrect monthly repayment");
			Assert.AreEqual(202, result.TotalRepayment, "Incorrect total amount");
		}

		[TestMethod]
		public void GetCorrectQuote_TwoDifferentOffersAndOneMonth()
		{
			mockAllocationProvider.Setup(m => m.GetLoanAllocationsForAmount(It.IsAny<double>()))
				.Returns(new[] {
					new LoanAllocation
					{
						Amount = 100,
						Rate = 0.12,
					},
					new LoanAllocation
					{
						Amount = 100,
						Rate = 0.24
					}
				});

			var quoteCalculator = new LoanQuoteGenerator(mockAllocationProvider.Object, calculator);

			var result = quoteCalculator.GetQuote(200, 1);

			Assert.AreEqual(200, result.LoanAmount, "Incorrect loan amount");
			Assert.AreEqual(0.18, Math.Round(result.InterestRate, 2), "Incorrect interest rate");
			Assert.AreEqual(203, result.MonthlyRepayment, "Incorrect monthly repayment");
			Assert.AreEqual(203, result.TotalRepayment, "Incorrect total amount");
		}

		[TestMethod]
		public void GetCorrectQuote_TwoDifferentOffersAndTwelveMonths()
		{
			mockAllocationProvider.Setup(m => m.GetLoanAllocationsForAmount(It.IsAny<double>()))
				.Returns(new[] {
					new LoanAllocation
					{
						Amount = 100,
						Rate = 0.12,
					},
					new LoanAllocation
					{
						Amount = 100,
						Rate = 0.24
					}
				});

			var quoteCalculator = new LoanQuoteGenerator(mockAllocationProvider.Object, calculator);

			var result = quoteCalculator.GetQuote(200, 12);

			Assert.AreEqual(200, result.LoanAmount, "Incorrect loan amount");
			Assert.AreEqual(0.18, Math.Round(result.InterestRate, 2), "Incorrect interest rate");
			Assert.AreEqual(19.67, Math.Round(result.MonthlyRepayment, 2), "Incorrect monthly repayment");
			Assert.AreEqual(236, result.TotalRepayment, "Incorrect total amount");
		}
	}
}
