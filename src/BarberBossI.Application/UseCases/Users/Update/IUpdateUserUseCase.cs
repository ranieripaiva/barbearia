using BarberBossI.Communication.Requests;

namespace BarberBossI.Application.UseCases.Users.Update;
public interface IUpdateUserUseCase
{
    Task Execute(RequestUpdateUserJson request);
}
