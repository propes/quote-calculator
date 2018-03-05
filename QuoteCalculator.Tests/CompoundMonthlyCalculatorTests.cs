using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuoteCalculator.Calculators;
using QuoteCalculator.Interfaces;
using System;

namespace QuoteCalculator.Tests
{
	[TestClass]
	public class CompoundMonthlyCalculatorTests
	{
		private readonly ILoanCalculator calculator = new CompoundMonthlyLoanCalculator();

		[TestMethod]
		public void CalculateMonthlyPaymentCorrectly_ZeroPrincipal()
		{
			var principal = 0M;
			var rate = 5M;
			var months = 12M;

			var monthlyPayment = calculator.CalculateMonthlyPayment(principal, rate, months);

			Assert.AreEqual(0, monthlyPayment);
		}

		[TestMethod]
		public void CalculateMonthlyPaymentCorrectly_12MonthLoan()
		{
			var principal = 1000M;
			var rate = 0.05M;
			var months = 12M;

			var monthlyPayment = calculator.CalculateMonthlyPayment(principal, rate, months);

			Assert.AreEqual(85.61M, Math.Round(monthlyPayment, 2));
		}

		[TestMethod]
		public void CalculateMonthlyPaymentCorrectly_24MonthLoan()
		{
			var principal = 1000M;
			var rate = 0.05M;
			var months = 24M;

			var monthlyPayment = calculator.CalculateMonthlyPayment(principal, rate, months);

			Assert.AreEqual(43.87M, Math.Round(monthlyPayment, 2));
		}

		[TestMethod]
		public void CalculateTotalPaymentCorrectly_ZeroTotalPayment()
		{
			var monthlyPayment = 0M;
			var months = 12M;

			var totalPayment = calculator.CalculateTotalPayment(monthlyPayment, months);

			Assert.AreEqual(0, totalPayment);
		}

		[TestMethod]
		public void CalculateTotalPaymentCorrectly_PositiveTotalPayment12MonthLoan()
		{
			var monthlyPayment = 100M;
			var months = 12M;

			var totalPayment = calculator.CalculateTotalPayment(monthlyPayment, months);

			Assert.AreEqual(1200, totalPayment);
		}
	}
}
