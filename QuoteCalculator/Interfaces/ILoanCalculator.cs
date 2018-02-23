namespace QuoteCalculator.Interfaces
{
	public interface ILoanCalculator
	{
		double CalculateMonthlyPayment(double principal, double rate, double months);

		double CalculateTotalPayment(double monthlyPayment, double months);

		double CalculateInterestRate(double principal, double monthlyPayment, double months);
	}
}