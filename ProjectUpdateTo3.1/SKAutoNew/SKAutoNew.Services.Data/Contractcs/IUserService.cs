namespace SKAutoNew.Services.Data.Contractcs
{
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<bool> ChangeUserIsAuthorizeSatusByUserIdAsync(string userId, string value);

        Task<bool?> GetAuthorizeStatusByUserNameAsync(string userName);

        Task<int> GetCountOfUsers();
    }
}
