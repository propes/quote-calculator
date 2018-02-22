using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteCalculator.Models
{
	public class LoanOffer
	{
		public string LenderName { get; set; }
		public decimal Rate { get; set; }
		public int Amount { get; set; }
	}
}
