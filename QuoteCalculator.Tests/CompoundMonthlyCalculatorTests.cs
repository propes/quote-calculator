using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuoteCalculator.Calculators;
using QuoteCalculator.Interfaces;
using System;

namespace QuoteCalculator.Tests
{
	[TestClass()]
	public class CompoundMonthlyCalculatorTests
	{
		private readonly ILoanCalculator calculator = new CompoundMonthlyLoanCalculator();

		[TestMethod()]
		public void CalculateMonthlyPaymentCorrectly_ZeroPrincipal()
		{
			var principal = 0;
			var rate = 5.0;
			var months = 12;

			var monthlyPayment = calculator.CalculateMonthlyPayment(principal, rate, months);

			Assert.AreEqual(0, monthlyPayment);
		}

		[TestMethod()]
		public void CalculateMonthlyPaymentCorrectly_12MonthLoan()
		{
			var principal = 1000;
			var rate = 0.05;
			var months = 12;

			var monthlyPayment = calculator.CalculateMonthlyPayment(principal, rate, months);

			Assert.AreEqual(85.61, Math.Round(monthlyPayment, 2));
		}

		[TestMethod()]
		public void CalculateMonthlyPaymentCorrectly_24MonthLoan()
		{
			var principal = 1000;
			var rate = 0.05;
			var months = 24;

			var monthlyPayment = calculator.CalculateMonthlyPayment(principal, rate, months);

			Assert.AreEqual(43.87, Math.Round(monthlyPayment, 2));
		}

		[TestMethod()]
		public void CalculateTotalPaymentCorrectly_ZeroTotalPayment()
		{
			var monthlyPayment = 0;
			var months = 12;

			var totalPayment = calculator.CalculateTotalPayment(monthlyPayment, months);

			Assert.AreEqual(0, totalPayment);
		}

		[TestMethod()]
		public void CalculateTotalPaymentCorrectly_PositiveTotalPayment12MonthLoan()
		{
			var monthlyPayment = 100;
			var months = 12;

			var totalPayment = calculator.CalculateTotalPayment(monthlyPayment, months);

			Assert.AreEqual(1200, totalPayment, 2);
		}
	}
}
