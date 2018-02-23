using QuoteCalculator.Interfaces;
using QuoteCalculator.Models;
using System.Collections.Generic;
using System.Linq;

namespace QuoteCalculator
{
	public class LoanAllocationProvider : ILoanAllocationProvider
	{
		private readonly ILoanOfferRepository loanOfferRepository;

		public LoanAllocationProvider(ILoanOfferRepository loanOfferRepository)
		{
			this.loanOfferRepository = loanOfferRepository;
		}

		public IEnumerable<LoanAllocation> GetLoanAllocations()
		{
			var offers = this.loanOfferRepository.GetLoanOffers();

			return Enumerable.Empty<LoanAllocation>();
		}
	}
}