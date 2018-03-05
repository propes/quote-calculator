namespace QuoteCalculator.Interfaces
{
	public interface ILoanCalculator
	{
		decimal CalculateMonthlyPayment(decimal principal, decimal rate, decimal months);

		decimal CalculateTotalPayment(decimal monthlyPayment, decimal months);

		decimal CalculateInterestRate(decimal principal, decimal monthlyPayment, decimal months);
	}
}