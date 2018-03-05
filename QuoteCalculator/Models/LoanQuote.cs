namespace QuoteCalculator.Models
{
	public class LoanQuote
	{
		public decimal LoanAmount { get; set; }
		public decimal InterestRate { get; set; }
		public decimal MonthlyRepayment { get; set; }
		public decimal TotalRepayment { get; set; }
	}
}