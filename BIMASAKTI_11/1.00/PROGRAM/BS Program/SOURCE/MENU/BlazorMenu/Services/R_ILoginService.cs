using BlazorMenu.Models;
using BlazorMenuCommon;

namespace BlazorMenu.Services
{
    public interface R_ILoginService
    {
        Task<LoginDTO> Login(LoginModel poParam);
    }
}