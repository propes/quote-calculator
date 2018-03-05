using System;
using QuoteCalculator.Exceptions;
using QuoteCalculator.Interfaces;
using QuoteCalculator.Models;
using System.Collections.Generic;
using System.Linq;

namespace QuoteCalculator
{
	public class LoanAllocationProvider : ILoanAllocationProvider
	{
		private readonly Lazy<IEnumerable<LoanOffer>> loanOffers;

		public LoanAllocationProvider(ILoanOfferRepository loanOfferRepository)
		{
			loanOffers = new Lazy<IEnumerable<LoanOffer>>(loanOfferRepository.GetLoanOffers);
		}

		public IEnumerable<LoanAllocation> GetLoanAllocationsForAmount(decimal loanAmount)
		{
			if (loanAmount > loanOffers.Value.Sum(o => o.Amount))
			{
				throw new FundsNotAvailableException();
			}

			var sortedLoanOffers = loanOffers.Value.OrderBy(l => l.Rate);
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