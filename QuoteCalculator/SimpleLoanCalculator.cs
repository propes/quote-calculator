using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteCalculator
{
	public class SimpleLoanCalculator : ILoanCalculator
	{
		public decimal CalculateMonthlyPayment(decimal principal, decimal rate, int months)
		{
			return months == 0 ? 0 : principal * (1 + rate / 12 * months) / months;
		}

		public decimal CalculateTotalPayment(decimal monthlyPayment, int months)
		{
			return monthlyPayment * months;
		}

		public decimal CalculateInterestRate(decimal principal, decimal monthlyAmount, int months)
		{
			return months == 0 ? 0 : (monthlyAmount * months / principal - 1) * 12 / months;
		}
	}
}
