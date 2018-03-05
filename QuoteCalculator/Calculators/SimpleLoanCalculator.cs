using QuoteCalculator.Interfaces;

namespace QuoteCalculator.Calculators
{
	public class SimpleLoanCalculator : ILoanCalculator
	{
		public decimal CalculateMonthlyPayment(decimal principal, decimal rate, decimal months)
		{
			return months == 0 ? 0 : principal * (1 + rate / 12 * months) / months;
		}

		public decimal CalculateTotalPayment(decimal monthlyPayment, decimal months)
		{
			return monthlyPayment * months;
		}

		public decimal CalculateInterestRate(decimal principal, decimal monthlyAmount, decimal months)
		{
			return months == 0 ? 0 : (monthlyAmount * months / principal - 1) * 12 / months;
		}
	}
}
