using BarberBossI.Communication.Requests;

namespace BarberBossI.Application.UseCases.Users.ChangePassword;
public interface IChangePasswordUseCase
{
    Task Execute(RequestChangePasswordJson request);
}
