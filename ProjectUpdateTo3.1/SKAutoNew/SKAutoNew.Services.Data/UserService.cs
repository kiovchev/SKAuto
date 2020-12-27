namespace SKAutoNew.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using SKAutoNew.Data.Models;
    using SKAutoNew.Data.Repositories;
    using SKAutoNew.Services.Data.Contractcs;
    using SKAutoNew.Services.Data.EfExtensionsHelper;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// extend business logic for User
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IRepository<ApplicationUser> repository;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="repository"></param>
        public UserService(IRepository<ApplicationUser> repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// change user authorize status in database
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="value"></param>
        /// <returns></returns>
         public async Task<bool> ChangeUserIsAuthorizeSatusByUserIdAsync(string userId, string value)
        {
            var users = this.repository.All();
            var usersAll = await EfExtensions.ToListAsyncSafe<ApplicationUser>(users);
            var user = usersAll.SingleOrDefault(x => x.Id == userId);

            bool.TryParse(value, out bool result);

            user.IsAuthorize = result;
            await this.repository.SaveAsync();

            return true;
        }

        /// <summary>
        /// get user authorize status by username from database
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<bool?> GetAuthorizeStatusByUserNameAsync(string userName)
        {
            var users = this.repository.All();
            var usersAll = await EfExtensions.ToListAsyncSafe<ApplicationUser>(users);
            var user = usersAll.SingleOrDefault(x => x.UserName == userName);

            if (user == null)
            {
                return null;
            }

            return user.IsAuthorize;
        }

        /// <summary>
        /// get all users from database and return users count
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetCountOfUsers()
        {
            var users = this.repository.All();
            var usersAll = await EfExtensions.ToListAsyncSafe<ApplicationUser>(users);
            int count = usersAll.Count();

            return count;
        }
    }
}
