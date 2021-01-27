namespace Pokeshop.Api.Infrastructure.Services
{
    public interface ICurrentUserService
    {
        string GetId();
        string GetUserName();
    }
}