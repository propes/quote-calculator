using QuoteCalculator.Interfaces;
using QuoteCalculator.Models;
using System.Collections.Generic;
using System.Linq;

namespace QuoteCalculator
{
	public class LoanOfferCsvRepository : ILoanOfferRepository
	{
		private readonly string filename;

		public LoanOfferCsvRepository(string filename)
		{
			this.filename = filename;
		}

		public IEnumerable<LoanOffer> GetLoanOffers()
		{
			return Enumerable.Empty<LoanOffer>();
		}
	}
}