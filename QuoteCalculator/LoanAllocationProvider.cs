using QuoteCalculator.Exceptions;
using QuoteCalculator.Interfaces;
using QuoteCalculator.Models;
using System;
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

			var sortedLoanOffers = loanOffers.Value.OrderBy(l => l.Rate).ToList();
			var loanAllocations = new List<LoanAllocation>();
			var amountRemaining = loanAmount;

			using (var loanOfferEnumerator = sortedLoanOffers.GetEnumerator())
			{
				while (amountRemaining > 0)
				{
					loanOfferEnumerator.MoveNext();
					var loanOffer = loanOfferEnumerator.Current;
					var loanAllocation = loanOffer.GetNextAllocation(amountRemaining);

					loanAllocations.Add(loanAllocation);
					amountRemaining -= loanAllocation.Amount;
				}
			}

			return loanAllocations;
		}
	}
}