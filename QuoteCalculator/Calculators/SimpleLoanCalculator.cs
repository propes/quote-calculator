using QuoteCalculator.Interfaces;

namespace QuoteCalculator.Calculators
{
	public class SimpleLoanCalculator : ILoanCalculator
	{
		public double CalculateMonthlyPayment(double principal, double rate, double months)
		{
			return months == 0 ? 0 : principal * (1 + rate / 12 * months) / months;
		}

		public double CalculateTotalPayment(double monthlyPayment, double months)
		{
			return monthlyPayment * months;
		}

		public double CalculateInterestRate(double principal, double monthlyAmount, double months)
		{
			return months == 0 ? 0 : (monthlyAmount * months / principal - 1) * 12 / months;
		}
	}
}
