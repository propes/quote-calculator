using QuoteCalculator.Interfaces;
using System;

namespace QuoteCalculator.Calculators
{
	public class CompoundMonthlyLoanCalculator : ILoanCalculator
	{
		private const double MonthsInYear = 12.0;

		public double CalculateMonthlyPayment(double principal, double rate, double months)
		{
			var monthlyRate = rate / MonthsInYear;
			return principal * monthlyRate * Math.Pow(1 + monthlyRate, months) / (Math.Pow(1 + monthlyRate, months) - 1);
		}

		public double CalculateTotalPayment(double monthlyPayment, double months)
		{
			return monthlyPayment * months;
		}

		public double CalculateInterestRate(double principal, double monthlyPayment, double months)
		{
			// Solve for rate interatively.
			// Assume that rate will never be higher than 100 to avoid infinite looping.
			// Assume that an accuracy of 2 DP is sufficient.
			for (var rate = 0.0; rate < 100.0; rate += 0.0001)
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
