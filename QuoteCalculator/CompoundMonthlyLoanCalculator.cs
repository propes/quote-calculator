using QuoteCalculator.Interfaces;

namespace QuoteCalculator
{
	public class CompoundMonthlyLoanCalculator : ILoanCalculator
	{
		public decimal CalculateMonthlyPayment(decimal principal, decimal rate, int months)
		{
			return 0;
		}

		public decimal CalculateTotalPayment(decimal monthlyPayment, int months)
		{
			return 0;
		}

		public decimal CalculateInterestRate(decimal principal, decimal monthlyPayment, int months)
		{
			return 0;
		}
	}
}
