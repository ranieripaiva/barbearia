using BarberBossI.Communication.Responses;

namespace BarberBossI.Application.UseCases.Users.Profile;
public interface IGetUserProfileUseCase
{
    Task<ResponseUserProfileJson> Execute();
}
