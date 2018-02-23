namespace QuoteCalculator.Models
{
	public class LoanQuote
	{
		public double LoanAmount { get; set; }
		public double InterestRate { get; set; }
		public double MonthlyRepayment { get; set; }
		public double TotalRepayment { get; set; }
	}
}