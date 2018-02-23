using System.Collections.Generic;
using QuoteCalculator.Models;

namespace QuoteCalculator.Interfaces
{
	public interface ILoanAllocationProvider
	{
		IEnumerable<LoanAllocation> GetLoanAllocations();
	}
}