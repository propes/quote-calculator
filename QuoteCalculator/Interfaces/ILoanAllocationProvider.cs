using QuoteCalculator.Models;
using System.Collections.Generic;

namespace QuoteCalculator.Interfaces
{
	public interface ILoanAllocationProvider
	{
		IEnumerable<LoanAllocation> GetLoanAllocationsForAmount(decimal loanAmount);
	}
}