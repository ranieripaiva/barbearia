using BarberBossI.Domain.Entities;
using BarberBossI.Domain.Enums;
using BarberBossI.Domain.Security.Cryptography;
using BarberBossI.Domain.Security.Tokens;
using BarberBossI.Infrastruture.DataAccess;
using CommonTestUtilities.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Test.Resources;

namespace WebApi.Test;
public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    public InvoiceIdentityManager Invoice_Admin { get; private set; } = default!;
    public InvoiceIdentityManager Invoice_MemberTeam { get; private set; } = default!;
    public UserIdentityManager User_Team_Member { get; private set; } = default!;
    public UserIdentityManager User_Admin { get; private set; } = default!;
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test")
            .ConfigureServices(services =>
            {
                var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                services.AddDbContext<BarberBossIDbContext>(config =>
                {
                    config.UseInMemoryDatabase("InMemoryDbForTesting");
                    config.UseInternalServiceProvider(provider);
                });

                var scope = services.BuildServiceProvider().CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<BarberBossIDbContext>();
                var passwordEncripter = scope.ServiceProvider.GetRequiredService<IPasswordEncripter>();
                var TokenGenerator = scope.ServiceProvider.GetRequiredService<IAccessTokenGenerator>();

                StartDatabase(dbContext, passwordEncripter, TokenGenerator);
            });
    }

    private void StartDatabase(
       BarberBossIDbContext dbContext,
       IPasswordEncripter passwordEncripter,
       IAccessTokenGenerator accessTokenGenerator)
    {
        var userTeamMember = AddUserTeamMember(dbContext, passwordEncripter, accessTokenGenerator);
        var invoiceTeamMember = AddInvoices(dbContext, userTeamMember, invoiceId: 1, tagId: 1);
        Invoice_MemberTeam = new InvoiceIdentityManager(invoiceTeamMember);

        var userAdmin = AddUserAdmin(dbContext, passwordEncripter, accessTokenGenerator);
        var invoiceAdmin = AddInvoices(dbContext, userAdmin, invoiceId: 2, tagId: 2);
        Invoice_Admin = new InvoiceIdentityManager(invoiceAdmin);

        dbContext.SaveChanges();
    }

    private User AddUserTeamMember(BarberBossIDbContext dbContext, IPasswordEncripter passwordEncripter, IAccessTokenGenerator accessTokenGenerator)
    {
        var user = UserBuilder.Build();
        user.Id = 1;

        var password = user.Password;
        user.Password = passwordEncripter.Encrypt(user.Password);

        dbContext.Users.Add(user);

        var token = accessTokenGenerator.Generate(user);

        User_Team_Member = new UserIdentityManager(user, password, token);

        return user;
    }

    private User AddUserAdmin( BarberBossIDbContext dbContext, IPasswordEncripter passwordEncripter, IAccessTokenGenerator accessTokenGenerator)
    {
        var user = UserBuilder.Build(Roles.ADMIN);
        user.Id = 2;

        var password = user.Password;
        user.Password = passwordEncripter.Encrypt(user.Password);

        dbContext.Users.Add(user);

        var token = accessTokenGenerator.Generate(user);
        User_Admin = new UserIdentityManager(user, password, token);

        return user;
    }

    private Invoice AddInvoices(BarberBossIDbContext dbContext, User user, long invoiceId, long tagId)
    {
        var invoice = InvoiceBuilder.Build(user);
        invoice.Id = invoiceId;

        foreach (var tag in invoice.Tags)
        {
            tag.Id = tagId;
            tag.InvoiceId = invoiceId;
        }

        dbContext.Invoices.Add(invoice);

        return invoice;
    }
}
