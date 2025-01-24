using BarberBossI.Domain.Repositories.Invoices;
using Moq;

namespace CommonTestUtilities.Repositories;
public class InvoicesWriteOnlyRepositoryBuilder
{
    public static IInvoicesWriteOnlyRepository Build()
    {
        var mock = new Mock<IInvoicesWriteOnlyRepository>();

        return mock.Object;
    }
}
