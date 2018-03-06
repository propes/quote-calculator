using QuoteCalculator.Exceptions;
using System;

namespace QuoteCalculator.Models
{
	public class LoanOffer
	{
		public string LenderName { get; set; }
		public decimal Rate { get; set; }
		public decimal Amount { get; set; }
		public decimal ExposureLimit { get; set; }
		public decimal MaxLoanAmount => Math.Min(this.ExposureLimit, this.Amount);
		public decimal AmountLent { get; set; }

		public LoanAllocation GetNextAllocation(decimal requestedLoanAmount)
		{
			var amountToLend = Math.Min(requestedLoanAmount, this.MaxLoanAmount);

			if (amountToLend > this.Amount - this.AmountLent)
			{
				throw new FundsNotAvailableException();
			}

			var loanAllocation = new LoanAllocation
			{
				LenderName = this.LenderName,
				Rate = this.Rate,
				Amount = amountToLend
			};

			this.AmountLent += amountToLend;

			return loanAllocation;
		}
	}
}
