using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuoteCalculator.Repositories;
using System;
using System.Linq;

namespace QuoteCalculator.Tests
{
	[TestClass()]
	public class LoanOfferCsvRepositoryTests
	{
		private const string Testfilepath = "TestFiles\\testfile.csv";

		[TestMethod()]
		public void GetLoanOffers_FileInCorrectFormat()
		{
			var repository = new LoanOfferCsvRepository(Testfilepath);

			var loanOffers = repository.GetLoanOffers().ToList();

			// Test the total count.
			Assert.AreEqual(7, loanOffers.Count);

			// Test the first record.
			Assert.AreEqual("Bob", loanOffers.First().LenderName);
			Assert.AreEqual(0.075, loanOffers.First().Rate);
			Assert.AreEqual(640, loanOffers.First().Amount);

			// Test the last record.
			Assert.AreEqual("Angela", loanOffers.Last().LenderName);
			Assert.AreEqual(0.071, loanOffers.Last().Rate);
			Assert.AreEqual(60, loanOffers.Last().Amount);
		}

		[TestMethod()]
		[ExpectedException(typeof(Exception))]
		public void ThrowException_FileDoesNotExist()
		{
			var repository = new LoanOfferCsvRepository("foo");

			var loanOffers = repository.GetLoanOffers();
		}
	}
}
