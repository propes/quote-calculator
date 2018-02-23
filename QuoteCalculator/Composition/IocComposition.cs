using Autofac;
using QuoteCalculator.Calculators;
using QuoteCalculator.Interfaces;
using QuoteCalculator.Printing;
using QuoteCalculator.Repositories;

namespace QuoteCalculator.Composition
{
	public class IocComposition
	{
		public static IContainer Compose(string filename)
		{
			var builder = new ContainerBuilder();

			builder.Register(c => new LoanOfferCsvRepository(filename)).As<ILoanOfferRepository>();
			builder.RegisterType<LoanAllocationProvider>().As<ILoanAllocationProvider>();
			builder.RegisterType<CompoundMonthlyLoanCalculator>().As<ILoanCalculator>();
			builder.RegisterType<LoanQuoteGenerator>().As<ILoanQuoteGenerator>();
			builder.RegisterType<ConsoleQuotePrinter>().As<IQuotePrinter>();

			return builder.Build();
		}
	}
}