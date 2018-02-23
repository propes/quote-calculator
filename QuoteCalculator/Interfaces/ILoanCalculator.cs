namespace QuoteCalculator.Interfaces
{
	public interface ILoanCalculator
	{
		decimal CalculateMonthlyPayment(decimal principal, decimal rate, int months);

		decimal CalculateTotalPayment(decimal monthlyPayment, int months);

		decimal CalculateInterestRate(decimal principal, decimal monthlyPayment, int months);
	}
}