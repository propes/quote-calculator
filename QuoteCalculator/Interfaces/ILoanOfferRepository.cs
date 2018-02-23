using System.Collections.Generic;
using QuoteCalculator.Models;

namespace QuoteCalculator.Interfaces
{
	public interface ILoanOfferRepository
	{
		IEnumerable<LoanOffer> GetLoanOffers();
	}
}