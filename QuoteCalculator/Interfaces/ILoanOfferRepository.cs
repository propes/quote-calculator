using QuoteCalculator.Models;
using System.Collections.Generic;

namespace QuoteCalculator.Interfaces
{
	public interface ILoanOfferRepository
	{
		IEnumerable<LoanOffer> GetLoanOffers();
	}
}