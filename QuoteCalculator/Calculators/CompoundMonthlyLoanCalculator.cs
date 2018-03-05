using QuoteCalculator.Interfaces;
using System;

namespace QuoteCalculator.Calculators
{
	public class CompoundMonthlyLoanCalculator : ILoanCalculator
	{
		private const decimal MonthsInYear = 12M;

		public decimal CalculateMonthlyPayment(decimal principal, decimal rate, decimal months)
		{
			var monthlyRate = rate / MonthsInYear;
			return principal * monthlyRate * (decimal)Math.Pow(1 + (double)monthlyRate, (double)months) / (decimal)(Math.Pow(1 + (double)monthlyRate, (double)months) - 1);
		}

		public decimal CalculateTotalPayment(decimal monthlyPayment, decimal months)
		{
			return monthlyPayment * months;
		}

		public decimal CalculateInterestRate(decimal principal, decimal monthlyPayment, decimal months)
		{
			// Solve for rate interatively.
			// Assume that rate will never be higher than 100 to avoid infinite looping.
			// Assume that an accuracy of 2 DP is sufficient.
			for (var rate = 0M; rate < 100M; rate += 0.0001M)
			{
				var testMonthlyPayment = CalculateMonthlyPayment(principal, rate, months);
				if (Math.Round(monthlyPayment, 2) == Math.Round(testMonthlyPayment, 2))
				{
					return rate;
				}
			}

			throw new InvalidOperationException("Rate could not be determined from these inputs");
		}
	}
}
