using BarberBossI.Application.UseCases.Invoices;
using BarberBossI.Application.UseCases.Invoices.Register;
using BarberBossI.Communication.Requests;
using CommonTestUtilities.Requests;

namespace Validators.Test.Invoices.Register;
public class RegisterInvoicesValidatorTests
{
    [Fact]
    public void success()
    {
        //Arrange - configurar instacias.
        var validator = new InvoiceValidator();
        var request = RequestRegisterInvoiceJsonBuilder.Build();
        
        //Act - Ação
        var result = validator.Validate(request);

        //Assert - Resultado
        Assert.True(result.IsValid);

    }
}
