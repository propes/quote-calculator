using Autofac;

namespace QuoteCalculator
{
	public class IocComposition
	{
		public static IContainer Compose()
		{
			var builder = new ContainerBuilder();

			builder.RegisterType<LoanOfferCsvRepository>();
			builder.RegisterType<LoanAllocationProvider>();
			builder.RegisterType<CompoundMonthlyLoanCalculator>();
			builder.RegisterType<LoanQuoteCalculator>();
			builder.RegisterType<ConsoleQuotePrinter>();

			return builder.Build();
		}
	}
}