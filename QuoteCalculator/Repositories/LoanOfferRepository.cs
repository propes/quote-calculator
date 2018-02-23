using QuoteCalculator.Interfaces;
using QuoteCalculator.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace QuoteCalculator.Repositories
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
			var loanOffers = new List<LoanOffer>();

			if (!File.Exists(filename))
			{
				throw new Exception("The specified file does not exist");
			}

			using (var reader = File.OpenText(filename))
			{
				var line = reader.ReadLine(); // Read and ignore first line as it contains the headers.
				while ((line = reader.ReadLine()) != null)
				{
					var columns = line.Split(',');
					var loanOffer = new LoanOffer
					{
						LenderName = columns[0],
						Rate = double.Parse(columns[1]),
						Amount = double.Parse(columns[2])
					};
					loanOffers.Add(loanOffer);
				}
			}

			return loanOffers;
		}
	}
}