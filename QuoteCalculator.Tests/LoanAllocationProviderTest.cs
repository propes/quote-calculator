using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using QuoteCalculator.Exceptions;
using QuoteCalculator.Interfaces;
using QuoteCalculator.Models;
using System.Linq;

namespace QuoteCalculator.Tests
{
	[TestClass]
	public class LoanAllocationProviderTests
	{
		private readonly Mock<ILoanOfferRepository> mockLoanOfferRepository =
			new Mock<ILoanOfferRepository>();

		[TestMethod]
		public void AllocateAvailableFundsCorrectlyToOneAllocation_OneOfferPartiallyRequired()
		{
			mockLoanOfferRepository.Setup(m => m.GetLoanOffers())
				.Returns(new[]
				{
					new LoanOffer { Amount = 1000, Rate = 5M },
					new LoanOffer { Amount = 2000, Rate = 6M }
				});

			var loanAllocator = new LoanAllocationProvider(mockLoanOfferRepository.Object);

			var loanAllocations = loanAllocator.GetLoanAllocationsForAmount(500).ToList();

			Assert.AreEqual(1, loanAllocations.Count);
			Assert.AreEqual(500, loanAllocations[0].Amount);
			Assert.AreEqual(5, loanAllocations[0].Rate);
		}

		[TestMethod]
		public void AllocateAvailableFundsCorrectlyToOneAllocation_OneOfferWhollyRequired()
		{
			mockLoanOfferRepository.Setup(m => m.GetLoanOffers())
				.Returns(new[]
				{
					new LoanOffer { Amount = 1000, Rate = 5M },
					new LoanOffer { Amount = 2000, Rate = 6M }
				});

			var loanAllocator = new LoanAllocationProvider(mockLoanOfferRepository.Object);

			var loanAllocations = loanAllocator.GetLoanAllocationsForAmount(1000).ToList();

			Assert.AreEqual(1, loanAllocations.Count);
			Assert.AreEqual(1000, loanAllocations[0].Amount);
			Assert.AreEqual(5, loanAllocations[0].Rate);
		}

		[TestMethod]
		public void AllocateAvailableFundsCorrectlyToTwoAllocations_TwoOffersRequired()
		{
			mockLoanOfferRepository.Setup(m => m.GetLoanOffers())
				.Returns(new[]
				{
					new LoanOffer { Amount = 500, Rate = 5M },
					new LoanOffer { Amount = 500, Rate = 6M },
					new LoanOffer { Amount = 500, Rate = 7M },
				});

			var loanAllocator = new LoanAllocationProvider(mockLoanOfferRepository.Object);

			var loanAllocations = loanAllocator.GetLoanAllocationsForAmount(650).ToList();

			Assert.AreEqual(2, loanAllocations.Count);
			Assert.AreEqual(500, loanAllocations[0].Amount);
			Assert.AreEqual(5, loanAllocations[0].Rate);
			Assert.AreEqual(150, loanAllocations[1].Amount);
			Assert.AreEqual(6, loanAllocations[1].Rate);
		}

		[TestMethod]
		public void AllocateAvailableFundsCorrectlyToThreeAllocations_AllOffersRequired()
		{
			mockLoanOfferRepository.Setup(m => m.GetLoanOffers())
				.Returns(new[]
				{
					new LoanOffer { Amount = 500, Rate = 5M },
					new LoanOffer { Amount = 500, Rate = 6M },
					new LoanOffer { Amount = 500, Rate = 7M }
				});

			var loanAllocator = new LoanAllocationProvider(mockLoanOfferRepository.Object);

			var loanAllocations = loanAllocator.GetLoanAllocationsForAmount(1500).ToList();

			Assert.AreEqual(3, loanAllocations.Count);
			Assert.AreEqual(500, loanAllocations[2].Amount);
			Assert.AreEqual(7, loanAllocations[2].Rate);
		}

		[TestMethod]
		public void EnsureLoansAreOrderedByRateWhenAllocatingFunds_ThreeAllocationsAllRequired()
		{
			mockLoanOfferRepository.Setup(m => m.GetLoanOffers())
				.Returns(new[]
				{
					new LoanOffer { Amount = 500, Rate = 6M },
					new LoanOffer { Amount = 300, Rate = 7M },
					new LoanOffer { Amount = 200, Rate = 5M }
				});

			var loanAllocator = new LoanAllocationProvider(mockLoanOfferRepository.Object);

			var loanAllocations = loanAllocator.GetLoanAllocationsForAmount(1000).ToList();

			Assert.AreEqual(5, loanAllocations.First().Rate);
			Assert.AreEqual(200, loanAllocations.First().Amount);
			Assert.AreEqual(7, loanAllocations.Last().Rate);
			Assert.AreEqual(300, loanAllocations.Last().Amount);
		}

		[TestMethod]
		[ExpectedException(typeof(FundsNotAvailableException))]
		public void ThrowExceptionWhenAllocatingFunds_InsufficientFundsAvailable()
		{
			mockLoanOfferRepository.Setup(m => m.GetLoanOffers())
				.Returns(new[]
				{
					new LoanOffer { Amount = 500, Rate = 5M },
					new LoanOffer { Amount = 500, Rate = 6M },
					new LoanOffer { Amount = 500, Rate = 7M }
				});

			var loanAllocator = new LoanAllocationProvider(mockLoanOfferRepository.Object);

			var loanAllocations = loanAllocator.GetLoanAllocationsForAmount(1600);
		}
	}
}
