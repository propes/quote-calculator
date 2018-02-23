using QuoteCalculator.Exceptions;
using QuoteCalculator.Interfaces;
using QuoteCalculator.Models;
using System.Collections.Generic;
using System.Linq;

namespace QuoteCalculator
{
	public class LoanAllocationProvider : ILoanAllocationProvider
	{
		private readonly IEnumerable<LoanOffer> loanOffers;

		public LoanAllocationProvider(ILoanOfferRepository loanOfferRepository)
		{
			this.loanOffers = loanOfferRepository.GetLoanOffers();
		}

		public bool FundsAreAvailable(double loanAmount)
		{
			var totalAmountOffered = loanOffers.Sum(l => l.Amount);
			return loanAmount <= totalAmountOffered;
		}

		public IEnumerable<LoanAllocation> GetLoanAllocationsForAmount(double loanAmount)
		{
			if (!FundsAreAvailable(loanAmount))
			{
				throw new FundsNotAvailableException();
			}

			var sortedLoanOffers = loanOffers.OrderBy(l => l.Rate);
			var loanAllocations = new List<LoanAllocation>();
			var amountRemaining = loanAmount;

			foreach (var loanOffer in sortedLoanOffers)
			{
				if (loanOffer.Amount <= amountRemaining)
				{
					loanAllocations.Add(new LoanAllocation { Amount = loanOffer.Amount, Rate = loanOffer.Rate });
					amountRemaining -= loanOffer.Amount;
				}
				else if (amountRemaining > 0)
				{
					loanAllocations.Add(new LoanAllocation { Amount = amountRemaining, Rate = loanOffer.Rate });
					break;
				}
				else
				{
					break;
				}
			}

			return loanAllocations;
		}
	}
}