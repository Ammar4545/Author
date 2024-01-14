using ApiApp.Models.RequestDTO;
using ApiApp.Models.ResponseDTO;

namespace ApiApp.Repositories
{
    public interface IUserRepository
    {
        AuthResult Registeration(UserAddTDO userAddTDO);
        AuthResult UserLogin(UserLoginTDO userLoginTDO);
    }
}