namespace SKAutoNew.Services.Tests
{
    using Moq;
    using SKAutoNew.Data.Models;
    using SKAutoNew.Data.Repositories;
    using SKAutoNew.Services.Data;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class UserServiceTests
    {
        private Mock<IRepository<ApplicationUser>> userRepo = new Mock<IRepository<ApplicationUser>>();
        
        private List<ApplicationUser> data = new List<ApplicationUser>
        {
            new ApplicationUser
            {
                Id = "pesho1",
                UserName = "pesho",
                Email = "pesho@pesho.bg",
                IsAuthorize = true
            },
            new ApplicationUser
            {
                Id = "gosho1",
                UserName = "gosho",
                Email = "gosho@gosho.bg",
                IsAuthorize = false
            }
        }; 

        [Fact]
        public async Task GetAuthorizeStatusByUserNameAsyncShouldReturnTrue()
        {
            userRepo.Setup(x => x.All()).Returns(data.AsQueryable());
            var userService = new UserService(userRepo.Object);

            bool isAuthorized = (bool)await userService.GetAuthorizeStatusByUserNameAsync("pesho");

            Assert.True(isAuthorized);
        }

        [Fact]
        public async Task GetAuthorizeStatusByUserNameAsyncShouldReturnFalse()
        {
            userRepo.Setup(x => x.All()).Returns(data.AsQueryable());
            var userService = new UserService(userRepo.Object);

            bool isAuthorized = (bool)await userService.GetAuthorizeStatusByUserNameAsync("gosho");

            Assert.False(isAuthorized);
        }

        [Fact]
        public async Task GetCountOfUsersShouldReturnTwo()
        {
            userRepo.Setup(x => x.All()).Returns(data.AsQueryable());
            var userService = new UserService(userRepo.Object);

            var count = await userService.GetCountOfUsers();

            var expected = 2;
            var actual = count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task ChangeUserIsAuthorizeSatusByUserIdAsyncShouldReturnTrue()
        {
            userRepo.Setup(x => x.All()).Returns(data.AsQueryable());
            var userService = new UserService(userRepo.Object);

            var isAuthorized = await userService.ChangeUserIsAuthorizeSatusByUserIdAsync("pesho1", "true");

            Assert.True(isAuthorized);
        }
    }
}
